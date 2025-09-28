export type CreatePatientDto = {
  firstName: string;
  lastName: string;
  birthDate: string;
  cancerStage: number;
};

export type PatientListItemDto = {
  id: number;
  name: string;
};

export type TreatmentOutcomeDto = {
  treatmentId: number;
  progressionDateUtc?: string | null;
  deathDateUtc?: string | null;
  paymentRate: number;
  payableAmountChf: number;
  refundAmountChf: number;
  effectiveDate?: string | null;
};

export type TreatmentDto = {
  id: number;
  patientId: number;
  contractId: number;
  productPackId: number;
  startDateUtc: string;
  durationMonths: number;
  outcome?: TreatmentOutcomeDto | null;
};

export type PatientDetailsDto = {
  id: number;
  firstName: string;
  lastName: string;
  birthDate: string;
  cancerStage: number;
  treatments: TreatmentDto[];
};
