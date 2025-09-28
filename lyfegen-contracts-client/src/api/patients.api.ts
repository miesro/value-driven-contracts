import { getJson, postJson } from "./http";
import type { CreatePatientDto, PatientListItemDto, PatientDetailsDto } from "./patients.types";

const base = "/api/patients";

export function getPatients(): Promise<PatientListItemDto[]> {
  return getJson<PatientListItemDto[]>(base);
}

export function createPatient(dto: CreatePatientDto): Promise<PatientDetailsDto> {
  return postJson<CreatePatientDto, PatientDetailsDto>(base, dto);
}

export function getPatientById(id: number): Promise<PatientDetailsDto> {
  return getJson<PatientDetailsDto>(`${base}/${id}`);
}
