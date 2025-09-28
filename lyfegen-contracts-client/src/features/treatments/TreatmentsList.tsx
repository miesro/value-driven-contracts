import type { TreatmentDto } from "../../api/patients.types";
import OutcomeEditor from "./OutcomeEditor";

type TreatmentsListProps = {
  items: TreatmentDto[];
  onSaveOutcome: (t: TreatmentDto, progression?: string, death?: string) => void;
};

export default function TreatmentsList({ items, onSaveOutcome }: TreatmentsListProps) {
  if (!items || items.length === 0) return <p>No treatments.</p>;

  return (
    <ul>
      {items.map(t => (
        <li key={t.id} style={{ marginBottom: 8 }}>
          <div>
            <strong>#{t.id}</strong> · productPackId: {t.productPackId} · contractId: {t.contractId} · start: {t.startDateUtc} · duration: {t.durationMonths}m
          </div>
          {t.outcome ? (
            <div style={{ fontSize: 14 }}>
              Outcome → rate: {t.outcome.paymentRate}% · payable: CHF {t.outcome.payableAmountChf} · refund: CHF {t.outcome.refundAmountChf} ·
              progression: {t.outcome.progressionDateUtc ?? "-"} · death: {t.outcome.deathDateUtc ?? "-"} · effective: {t.outcome.effectiveDate ?? "-"}
            </div>
          ) : (
            <OutcomeEditor treatment={t} onSave={onSaveOutcome} />
          )}
        </li>
      ))}
    </ul>
  );
}
