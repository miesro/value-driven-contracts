import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getPatientById } from "../api/patients.api";
import { setTreatmentOutcome } from "../api/treatments.api";
import type { PatientDetailsDto, TreatmentDto } from "../api/patients.types";
import PatientSummary from "../features/patients/PatientSummary";
import TreatmentsList from "../features/treatments/TreatmentsList";
import CreateTreatmentForm from "../features/treatments/CreateTreatmentForm";

export default function PatientDetailsPage() {
  const { id } = useParams();
  const patientId = Number(id);
  const [patient, setPatient] = useState<PatientDetailsDto | null>(null);

  async function load() {
    try {
      const data = await getPatientById(patientId);
      setPatient(data);
    } catch (e) {
      console.error(e);
    }
  }

  useEffect(() => {
    load();
  }, [patientId]);

  async function saveOutcome(t: TreatmentDto, progression?: string, death?: string) {
    try {
      await setTreatmentOutcome(t.id, {
        progressionDateUtc: progression || null,
        deathDateUtc: death || null
      });
      await load();
    } catch (e) {
      console.error(e);
    }
  }

  if (!patient) return null;

  return (
    <main>
      <PatientSummary patient={patient} />

      <h2 style={{ marginTop: 16 }}>Treatments</h2>
      <TreatmentsList items={patient.treatments} onSaveOutcome={saveOutcome} />

      <CreateTreatmentForm patient={patient} onCreated={load} />
    </main>
  );
}
