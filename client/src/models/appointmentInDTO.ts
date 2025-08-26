import { PrescriptionDetailInDTO } from "./prescriptionDetailInDTO";
import { VisitType } from "./visitType";

export interface AppointmentInDTO {
  patientId: number;
  doctorId: number;
  appointmentDate: string;
  diagnosis: string;
  notes: string;
  visitType: VisitType;
  prescriptionDetails: PrescriptionDetailInDTO[];
}