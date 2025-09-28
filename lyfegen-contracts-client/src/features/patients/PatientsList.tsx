import { Link } from "react-router-dom";
import type { PatientListItemDto } from "../../api/patients.types";

type Props = {
  items: PatientListItemDto[];
};

export default function PatientsList({ items }: Props) {
  if (!items || items.length === 0) return <p>No patients</p>;

  return (
    <ul>
      {items.map(p => (
        <li key={p.id}>
          <Link to={`/patients/${p.id}`}>{p.name}</Link>
        </li>
      ))}
    </ul>
  );
}
