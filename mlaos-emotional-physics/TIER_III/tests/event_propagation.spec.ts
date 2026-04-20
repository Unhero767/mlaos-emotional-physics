// TIER_III/event_propagation.ts

import { CausalEvent, topologicallySortEvents } from "./causal_ordering";

export interface RealityState<TState = unknown> {
  tick: number;
  value: TState;
}

export type EventReducer<TState, TPayload> = (
  state: RealityState<TState>,
  event: CausalEvent<TPayload>
) => RealityState<TState>;

/**
 * Applies a set of causally-related events to a state, in causal order.
 */
export function propagateEvents<TState, TPayload>(
  initial: RealityState<TState>,
  events: CausalEvent<TPayload>[],
  reducer: EventReducer<TState, TPayload>
): RealityState<TState> {
  const ordered = topologicallySortEvents(events);
  let current = initial;
  for (const ev of ordered) {
    current = reducer(current, ev);
  }
  return current;
}
