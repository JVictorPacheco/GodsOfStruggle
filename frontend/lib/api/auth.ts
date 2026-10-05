import { apiFetch } from "./client";

export const syncUser = (token: string): Promise<void> =>
  apiFetch("/api/v1/auth/sync", {
    method: "POST",
    headers: { Authorization: `Bearer ${token}` },
  });
