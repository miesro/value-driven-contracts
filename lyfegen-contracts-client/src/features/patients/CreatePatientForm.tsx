import { useState } from "react";
import type { CreatePatientDto } from "../../api/patients.types";

type Props = {
  onCreated: (p: CreatePatientDto) => Promise<void> | void;
};

export default function CreatePatientForm({ onCreated }: Props) {
  const [firstName, setFirst] = useState("");
  const [lastName, setLast] = useState("");
  const [birthDate, setBirth] = useState("");
  const [cancerStage, setStage] = useState(0);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    try {
      await onCreated({
        firstName: firstName.trim(),
        lastName: lastName.trim(),
        birthDate,
        cancerStage: Number(cancerStage)
      });
      setFirst(""); setLast(""); setBirth(""); setStage(0);
    } catch (err) {
      console.error(err);
    }
  }

  return (
    <form onSubmit={submit} style={{ marginBottom: 12 }}>
      <div>
        <label htmlFor="firstName">First name</label><br />
        <input id="firstName" placeholder="First name" value={firstName} onChange={e => setFirst(e.target.value)} required />
      </div>

      <div>
        <label htmlFor="lastName">Last name</label><br />
        <input id="lastName" placeholder="Last name" value={lastName} onChange={e => setLast(e.target.value)} required />
      </div>

      <div>
        <label htmlFor="birthDate">Birth date</label><br />
        <input id="birthDate" type="date" value={birthDate} onChange={e => setBirth(e.target.value)} required />
      </div>

      <div>
        <label htmlFor="cancerStage">Cancer stage</label><br />
        <input
          id="cancerStage"
          type="number"
          value={cancerStage}
          onChange={e => setStage(parseInt(e.target.value || "0", 10))}
          required
        />
      </div>

      <button type="submit">Create</button>
    </form>
  );
}
