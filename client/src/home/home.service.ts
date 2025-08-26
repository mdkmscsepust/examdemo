import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DropdownListModel } from '../models/dropdownListModel';
import { Observable } from 'rxjs';
import { AppointmentInDTO } from '../models/appointmentInDTO';
import { AppointmentOutDTO } from '../models/appointmentOutDTO';
import { AppointmentFilterInDTO } from '../models/appointmentFilterInDTO';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  baseUrl: string = 'http://localhost:5274'
  constructor(private httpClient: HttpClient) { }

  getDoctorDropdownList(): Observable<DropdownListModel[]>  {
     return this.httpClient.get<DropdownListModel[]>(`${this.baseUrl}/api/doctors/getdropdownlist`);
  }

  getPatientDropdownList() : Observable<DropdownListModel[]>  {
     return this.httpClient.get<DropdownListModel[]>(`${this.baseUrl}/api/patients/getdropdownlist`);
  }

  createPatient(appointmentInDTO :AppointmentInDTO) {
     return this.httpClient.post(`${this.baseUrl}/api/appointment/Post`, appointmentInDTO);
  }

  updatePatient(id: number, appointmentInDTO: AppointmentInDTO)  {
     return this.httpClient.put(`${this.baseUrl}/api/appointment/${id}`, appointmentInDTO);
  }

  deletePatient(id: number) {
     return this.httpClient.delete(`${this.baseUrl}/api/appointment/${id}`);
  }

  getPatient(id: number) : Observable<AppointmentOutDTO>  {
     return this.httpClient.get<AppointmentOutDTO>(`${this.baseUrl}/api/appointment/${id}`);
  }

  getAllPatient(appointmentFilterInDTO: AppointmentFilterInDTO) : Observable<AppointmentOutDTO[]>  {
     return this.httpClient.post<AppointmentOutDTO[]>(`${this.baseUrl}/api/appointment/getall`, appointmentFilterInDTO);
  }

  getDownloadPdf(id: number): Observable<Blob> {
  return this.httpClient.get(`${this.baseUrl}/api/prescription/download/${id}`, {
    responseType: 'blob' // Important: specify blob response type
  });
}

}
