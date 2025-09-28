export type CreateContractDto = {
  payerPartyId: number;
  manufacturerPartyId: number;
  brandedProductId: number;
  osMonths: number;
  pfsMonths: number;
  osAfterMonthsRate: number;
  osBeforeMonthsRate: number;
  pfsAfterMonthsRate: number;
  pfsBeforeMonthsRate: number;
  minStage: number;
  maxStage: number;
  maxAgeExclusive: number;
};

export type ContractDto = {
  id: number;
  payerPartyId: number;
  manufacturerPartyId: number;
  brandedProductId: number;
  osMonths: number;
  pfsMonths: number;
  osAfterMonthsRate: number;
  osBeforeMonthsRate: number;
  pfsAfterMonthsRate: number;
  pfsBeforeMonthsRate: number;
  minStage: number;
  maxStage: number;
  maxAgeExclusive: number;
};

export type MatchContractsRequestDto = {
  productPackId: number;
  cancerStage: number;
  patientAge: number;
};

export type ContractMatchDto = {
  id: number;
};

//TODO: move to a common file:

export type ProblemDetails = {
  type?: string;
  title?: string;
  status?: number;
  detail?: string | null;
  instance?: string;
  traceId?: string;
  timestamp?: string;
  errors?: Record<string, string[]>;
};
