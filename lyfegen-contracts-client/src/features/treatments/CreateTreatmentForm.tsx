import { useMemo, useState } from "react";
import type { PatientDetailsDto } from "../../api/patients.types";
import type { ContractMatchDto } from "../../api/contracts.types";
import type { CreateTreatmentDto } from "../../api/treatments.types";
import { getMatchingContracts } from "../../api/contracts.api";
import { createTreatment } from "../../api/treatments.api";

function calcAge(dateOnlyIso: string): number {
  const today = new Date();
  const bd = new Date(dateOnlyIso + "T00:00:00Z");
  let age = today.getUTCFullYear() - bd.getUTCFullYear();
  const m = today.getUTCMonth() - bd.getUTCMonth();
  if (m < 0 || (m === 0 && today.getUTCDate() < bd.getUTCDate())) age--;
  return age;
}

type Props = {
  patient: PatientDetailsDto;
  onCreated: () => Promise<void> | void; // parent will reload
};

export default function CreateTreatmentForm({ patient, onCreated }: Props) {
  const [productPackId, setProductPackId] = useState<number>(0);
  const [startDateUtc, setStartDateUtc] = useState<string>("");
  const [durationMonths, setDurationMonths] = useState<number>(1);
  const [matches, setMatches] = useState<ContractMatchDto[] | null>(null);
  const [contractId, setContractId] = useState<number | null>(null);

  const patientAge = useMemo(() => calcAge(patient.birthDate), [patient.birthDate]);

  async function queryMatches() {
    if (!productPackId) return;
    try {
      const res = await getMatchingContracts({
        productPackId,
        cancerStage: patient.cancerStage,
        patientAge
      });
      setMatches(res);
      setContractId(res[0]?.id ?? null);
    } catch (e) {
      console.error(e);
    }
  }

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    if (!contractId) return;

    const dto: CreateTreatmentDto = {
      patientId: patient.id,
      productPackId,
      contractId,
      startDateUtc,
      durationMonths
    };

    try {
      await createTreatment(dto);
      setProductPackId(0);
      setStartDateUtc("");
      setDurationMonths(1);
      setMatches(null);
      setContractId(null);
      await onCreated();
    } catch (e) {
      console.error(e);
    }
  }

  return (
    <>
      <h3 style={{ marginTop: 16 }}>Create treatment</h3>
      <form onSubmit={submit} style={{ display: "flex", gap: 12, alignItems: "center", flexWrap: "wrap" }}>
        <label htmlFor="productPackId" style={{ display: "flex", flexDirection: "column" }}>
          <span>Product pack id</span>
          <input
            id="productPackId"
            type="number"
            placeholder="Product pack id"
            value={productPackId || ""}
            onChange={e => setProductPackId(parseInt(e.target.value || "0", 10))}
            style={{ width: 160 }}
          />
        </label>

        <label htmlFor="startDateUtc" style={{ display: "flex", flexDirection: "column" }}>
          <span>Treatment start date (UTC)</span>
          <input
            id="startDateUtc"
            type="date"
            value={startDateUtc}
            onChange={e => setStartDateUtc(e.target.value)}
          />
        </label>

        <label htmlFor="durationMonths" style={{ display: "flex", flexDirection: "column" }}>
          <span>Duration (months)</span>
          <input
            id="durationMonths"
            type="number"
            placeholder="Duration months"
            value={durationMonths}
            onChange={e => setDurationMonths(parseInt(e.target.value || "0", 10))}
            style={{ width: 140 }}
          />
        </label>

        <button type="button" onClick={queryMatches}>Get matching contracts</button>

        {matches && (
          matches.length > 0 ? (
            <label htmlFor="contractId" style={{ display: "flex", flexDirection: "column" }}>
              <span>Contract</span>
              <select
                id="contractId"
                value={contractId ?? undefined}
                onChange={e => setContractId(parseInt(e.target.value, 10))}
              >
                {matches.map(m => (
                  <option key={m.id} value={m.id}>Contract {m.id}</option>
                ))}
              </select>
            </label>
          ) : <span>No matches</span>
        )}

        <button type="submit">Create</button>
      </form>
    </>
  );
}
