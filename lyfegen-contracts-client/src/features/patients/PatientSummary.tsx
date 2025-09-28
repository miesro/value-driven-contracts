import type { PatientDetailsDto } from "../../api/patients.types";

type Props = { patient: PatientDetailsDto };

export default function PatientSummary({ patient }: Props) {
  return (
    <>
      <h1>{patient.firstName} {patient.lastName}</h1>
      <p>Birth date: {patient.birthDate}</p>
      <p>Cancer stage: {patient.cancerStage}</p>
    </>
  );
}
