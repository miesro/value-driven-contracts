import { useState } from "react";
import type { TreatmentDto } from "../../api/patients.types";

export type OutcomeEditorProps = {
  treatment: TreatmentDto;
  onSave: (t: TreatmentDto, progression?: string, death?: string) => void;
};

export default function OutcomeEditor({ treatment, onSave }: OutcomeEditorProps) {
  const [progression, setProgression] = useState("");
  const [death, setDeath] = useState("");

  return (
    <form
      onSubmit={e => { e.preventDefault(); onSave(treatment, progression || undefined, death || undefined); }}
      style={{ display: "flex", gap: 12, alignItems: "center", marginTop: 4, flexWrap: "wrap" }}
    >
      <label htmlFor={`progression-${treatment.id}`} style={{ display: "flex", flexDirection: "column" }}>
        <span>Progression date</span>
        <input
          id={`progression-${treatment.id}`}
          type="date"
          value={progression}
          onChange={e => setProgression(e.target.value)}
          placeholder="Progression date"
        />
      </label>

      <label htmlFor={`death-${treatment.id}`} style={{ display: "flex", flexDirection: "column" }}>
        <span>Death date</span>
        <input
          id={`death-${treatment.id}`}
          type="date"
          value={death}
          onChange={e => setDeath(e.target.value)}
          placeholder="Death date"
        />
      </label>

      <button type="submit">Save outcome</button>
    </form>
  );
}
