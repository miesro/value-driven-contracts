import { getJson, postJson } from "./http";
import type { ContractDto, ContractMatchDto, CreateContractDto, MatchContractsRequestDto } from "./contracts.types";

export function getContracts(): Promise<ContractDto[]> {
  return getJson<ContractDto[]>("/api/contracts");
}

export function createContract(dto: CreateContractDto): Promise<ContractDto> {
  return postJson<CreateContractDto, ContractDto>("/api/contracts", dto);
}

export function getMatchingContracts(q: MatchContractsRequestDto): Promise<ContractMatchDto[]> {
  const params = new URLSearchParams({
    productPackId: String(q.productPackId),
    cancerStage: String(q.cancerStage),
    patientAge: String(q.patientAge)
  });
  return getJson<ContractMatchDto[]>(`/api/contracts/matching?${params.toString()}`);
}