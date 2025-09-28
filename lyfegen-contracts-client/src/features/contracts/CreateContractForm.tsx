import { useState } from "react";
import { createContract } from "../../api/contracts.api";
import type { CreateContractDto } from "../../api/contracts.types";

type Props = { onCreated: () => void };

const defaults: CreateContractDto = {
  payerPartyId: 1,
  manufacturerPartyId: 2,
  brandedProductId: 1,
  osMonths: 12,
  pfsMonths: 9,
  osAfterMonthsRate: 75,
  osBeforeMonthsRate: 30,
  pfsAfterMonthsRate: 85,
  pfsBeforeMonthsRate: 40,
  minStage: 0,
  maxStage: 3,
  maxAgeExclusive: 55
};

const fields: (keyof CreateContractDto)[] = [
  "payerPartyId",
  "manufacturerPartyId",
  "brandedProductId",
  "osMonths",
  "pfsMonths",
  "osAfterMonthsRate",
  "osBeforeMonthsRate",
  "pfsAfterMonthsRate",
  "pfsBeforeMonthsRate",
  "minStage",
  "maxStage",
  "maxAgeExclusive"
];

export default function CreateContractForm({ onCreated }: Props) {
  const [form, setForm] = useState<CreateContractDto>(defaults);

  function onChange(e: React.ChangeEvent<HTMLInputElement>) {
    const key = e.target.name as keyof CreateContractDto;
    const val = Number(e.target.value);
    setForm(prev => ({ ...prev, [key]: val }));
  }

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    try {
      await createContract(form);
      onCreated();
      setForm(defaults);
    } catch (err) {
      console.error("Create contract failed:", err);
    }
  }

  return (
    <form onSubmit={submit}>
      <h2>Create Contract</h2>

      {fields.map((f) => (
        <div key={f}>
          <label style={{ display: "block" }}>{f}</label>
          <input
            type="number"
            name={f}
            value={form[f] as number}
            onChange={onChange}
          />
        </div>
      ))}
      <button type="submit">Create</button>
    </form>
  );
}
