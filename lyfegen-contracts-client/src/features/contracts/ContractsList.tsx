import type { ContractDto } from "../../api/contracts.types";

type Props = {
  items: ContractDto[];
  reload: () => void;
};

export default function ContractsList({ items, reload }: Props) {
  return (
    <section>
      <h2>Contracts</h2>
      <button onClick={reload}>Reload</button>
      {items.length === 0 ? (
        <p>No contracts</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Payer</th>
              <th>Manufacturer</th>
              <th>Product</th>
              <th>OS/PFS months</th>
              <th>Rates</th>
              <th>Enrollment</th>
            </tr>
          </thead>
          <tbody>
            {items.map(c => (
              <tr key={c.id}>
                <td>{c.id}</td>
                <td>{c.payerPartyId}</td>
                <td>{c.manufacturerPartyId}</td>
                <td>{c.brandedProductId}</td>
                <td>{c.osMonths}/{c.pfsMonths}</td>
                <td>
                  OS {c.osAfterMonthsRate}/{c.osBeforeMonthsRate} | PFS {c.pfsAfterMonthsRate}/{c.pfsBeforeMonthsRate}
                </td>
                <td>Stage {c.minStage}-{c.maxStage}, MaxAge {c.maxAgeExclusive}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </section>
  );
}
