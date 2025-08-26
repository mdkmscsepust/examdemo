import { Component, OnInit } from '@angular/core';
import { HomeService } from './home.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AppointmentFilterInDTO } from '../models/appointmentFilterInDTO';
import { AppointmentOutDTO } from '../models/appointmentOutDTO';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DropdownListModel } from '../models/dropdownListModel';
import { VisitType } from '../models/visitType';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  appointmentFilterInDTO: AppointmentFilterInDTO = {
  page: 1,
  pageSize: 10
};

appointmentOutDTO: AppointmentOutDTO[] = [];

appointmentForm!: FormGroup;
  doctors: DropdownListModel[] = [];
  patients: DropdownListModel[] = [];
  visitTypes = VisitType;
  showForm = false;

  constructor(private fb: FormBuilder, private homeService: HomeService) {}
  ngOnInit(): void {
    this.getAppointment();
    this.loadDropdowns();
    this.buildForm();
  }
  getAppointment() {
    this.homeService.getAllPatient(this.appointmentFilterInDTO).subscribe({
      next: res => this.appointmentOutDTO = res,
      error: err => console.error(err)
    });
  }
 
downloadAppointment(id: number): void {
  this.homeService.getDownloadPdf(id).subscribe({
    next: (blob: Blob) => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `Prescription_${id}.pdf`;
      document.body.appendChild(a);
      a.click();
      
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    },
    error: (err) => {
      console.error('Download failed:', err);
      alert('Failed to download prescription.');
    }
  });
}

  loadDropdowns(): void {
    this.homeService.getDoctorDropdownList().subscribe(d => this.doctors = d);
    this.homeService.getPatientDropdownList().subscribe(p => this.patients = p);
  }

  buildForm(): void {
    this.appointmentForm = this.fb.group({
      patientId: [null, Validators.required],
      doctorId: [null, Validators.required],
      appointmentDate: [new Date().toISOString(), Validators.required],
      diagnosis: ['', Validators.required],
      notes: [''],
       visitType: [VisitType.FirstVisit, Validators.required],
      prescriptionDetails: this.fb.array([])
    });
  }

  get prescriptionDetails(): FormArray {
    return this.appointmentForm.get('prescriptionDetails') as FormArray;
  }

  addPrescriptionDetail(): void {
    const detail = this.fb.group({
      medicineId: [null, Validators.required],
      dosage: ['2x', Validators.required],
      notes: [''],
      startDate: [new Date().toISOString(), Validators.required],
      endDate: [new Date().toISOString(), Validators.required]
    });
    this.prescriptionDetails.push(detail);
  }

  removePrescriptionDetail(index: number): void {
    this.prescriptionDetails.removeAt(index);
  }

  saveAppointment(): void {
    if (this.appointmentForm.invalid) {
      alert('Please fill all required fields.');
      return;
    }

    this.homeService.createPatient(this.appointmentForm.value).subscribe({
      next: res => console.log('Saved!', res),
      error: err => console.error(err)
    });
  }

  

  resetForm(): void {
    this.appointmentForm.reset({
      appointmentDate: '',
      diagnosis: '',
      notes: '',
      visitType: VisitType.FirstVisit
    });
    this.prescriptionDetails.clear();
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    if (!this.showForm) {
      this.resetForm();
    }
  }
}
