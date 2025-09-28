import type { ProblemDetails } from "./contracts.types";

const BASE_URL = import.meta.env.VITE_API_BASE_URL ?? "https://localhost:7037";

export async function getJson<T>(path: string, init?: RequestInit): Promise<T> {
  const res = await fetch(`${BASE_URL}${path}`, { ...init, headers: { "Accept": "application/json", ...(init?.headers ?? {}) } });
  if (!res.ok) {
    const maybePd = await safeReadProblemDetails(res);
    throw maybePd ?? new Error(`${res.status} ${res.statusText}`);
  }
  return res.json() as Promise<T>;
}

export async function postJson<TBody, TOut>(path: string, body: TBody): Promise<TOut> {
  const res = await fetch(`${BASE_URL}${path}`, {
    method: "POST",
    headers: { "Content-Type": "application/json", "Accept": "application/json" },
    body: JSON.stringify(body)
  });
  if (!res.ok) {
    const maybePd = await safeReadProblemDetails(res);
    throw maybePd ?? new Error(`${res.status} ${res.statusText}`);
  }
  return res.json() as Promise<TOut>;
}

async function safeReadProblemDetails(res: Response): Promise<ProblemDetails | null> {
  try {
    const text = await res.text();
    if (!text) return null;
    return JSON.parse(text) as ProblemDetails;
  } catch {
    return null;
  }
}
