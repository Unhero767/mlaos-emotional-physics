// TIER_III/causal_ordering.ts

export type EventId = string;

export interface CausalEvent<TPayload = unknown> {
  id: EventId;
  timestamp: number; // logical or wall-clock
  causes: EventId[]; // ids of direct causes
  payload: TPayload;
}

/**
 * Ensures events are ordered so that every cause appears
 * before any event that depends on it.
 */
export function topologicallySortEvents<TPayload>(
  events: CausalEvent<TPayload>[]
): CausalEvent<TPayload>[] {
  const byId = new Map<EventId, CausalEvent<TPayload>>();
  const incomingCount = new Map<EventId, number>();
  const outgoing = new Map<EventId, Set<EventId>>();

  for (const ev of events) {
    byId.set(ev.id, ev);
    incomingCount.set(ev.id, 0);
    outgoing.set(ev.id, new Set());
  }

  for (const ev of events) {
    for (const cause of ev.causes) {
      if (!byId.has(cause)) continue; // external cause
      incomingCount.set(ev.id, (incomingCount.get(ev.id) ?? 0) + 1);
      outgoing.get(cause)?.add(ev.id);
    }
  }

  const queue: EventId[] = [];
  for (const [id, count] of incomingCount.entries()) {
    if (count === 0) queue.push(id);
  }

  const ordered: CausalEvent<TPayload>[] = [];
  while (queue.length > 0) {
    const id = queue.shift()!;
    const ev = byId.get(id);
    if (!ev) continue;
    ordered.push(ev);

    for (const child of outgoing.get(id) ?? []) {
      const nextCount = (incomingCount.get(child) ?? 0) - 1;
      incomingCount.set(child, nextCount);
      if (nextCount === 0) queue.push(child);
    }
  }

  if (ordered.length !== events.length) {
    throw new Error("Causal cycle detected in events");
  }

  return ordered;
}
