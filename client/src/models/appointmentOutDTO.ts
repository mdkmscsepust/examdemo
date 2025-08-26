import { PrescriptionDetailOutDTO } from "./prescriptionDetailOutDTO";
import { VisitType } from "./visitType";

export interface AppointmentOutDTO {
  id: number;
  patientId: number;
  patientName: string;
  doctorId: number;
  doctorName: string;
  appointmentDate: string; 
  diagnosis: string;
  notes: string;
  visitType: VisitType;
  prescriptionDetails: PrescriptionDetailOutDTO[];
}