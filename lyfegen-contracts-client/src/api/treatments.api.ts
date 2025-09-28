import { postJson } from "./http";
import type { CreateTreatmentDto, SetTreatmentOutcomeDto } from "./treatments.types";
import type { TreatmentDto, TreatmentOutcomeDto } from "./patients.types";

const base = "/api/treatments";

export function createTreatment(dto: CreateTreatmentDto): Promise<TreatmentDto> {
  return postJson<CreateTreatmentDto, TreatmentDto>(base, dto);
}

export function setTreatmentOutcome(id: number, dto: SetTreatmentOutcomeDto): Promise<TreatmentOutcomeDto> {
  return postJson<SetTreatmentOutcomeDto, TreatmentOutcomeDto>(`${base}/${id}/outcome`, dto);
}
