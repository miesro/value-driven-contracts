export type CreateTreatmentDto = {
  patientId: number;
  productPackId: number;
  contractId: number;
  startDateUtc: string;
  durationMonths: number;
};

export type SetTreatmentOutcomeDto = {
  progressionDateUtc?: string | null;
  deathDateUtc?: string | null;
};
