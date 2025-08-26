import { VisitType } from "./visitType";

export interface AppointmentFilterInDTO {
  patientId?: number;
  doctorId?: number;
  visitType?: VisitType;
  fromDate?: string;
  toDate?: string; 
  diagnosis?: string;

  page?: number
  pageSize?: number
}
