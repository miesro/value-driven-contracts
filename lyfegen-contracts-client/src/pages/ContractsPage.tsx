import { useEffect, useState } from "react";
import { getContracts } from "../api/contracts.api";
import type { ContractDto } from "../api/contracts.types";
import CreateContractForm from "../features/contracts/CreateContractForm";
import ContractsList from "../features/contracts/ContractsList";

export default function ContractsPage() {
  const [items, setItems] = useState<ContractDto[]>([]);
  const [loading, setLoading] = useState(true);

  async function load() {
    setLoading(true);
    try {
      const res = await getContracts();
      setItems(res);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    load();
  }, []);

  return (
    <main>
      <h1>Contracts</h1>
      <CreateContractForm onCreated={load} />
      {loading ? (
        <p>Loading...</p>
      ) : (
        <ContractsList items={items} reload={load} />
      )}
    </main>
  );
}
