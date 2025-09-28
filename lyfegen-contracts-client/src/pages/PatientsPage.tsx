import { useEffect, useState } from "react";
import { getPatients, createPatient } from "../api/patients.api";
import type { PatientListItemDto, CreatePatientDto } from "../api/patients.types";
import PatientsList from "../features/patients/PatientsList";
import CreatePatientForm from "../features/patients/CreatePatientForm";

export default function PatientsPage() {
  const [items, setItems] = useState<PatientListItemDto[]>([]);

  async function load() {
    try {
      const res = await getPatients();
      setItems(res);
    } catch (e) {
      console.error(e);
    }
  }

  useEffect(() => { load(); }, []);

  async function handleCreate(p: CreatePatientDto) {
    try {
      await createPatient(p);
      await load();
    } catch (e) {
      console.error(e);
    }
  }

  return (
    <main>
      <h1>Patients</h1>
      <CreatePatientForm onCreated={handleCreate} />
      <PatientsList items={items} />
    </main>
  );
}
