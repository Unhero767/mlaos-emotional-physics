// TIER_III/reality_anchor.ts

import { CausalEvent } from "./causal_ordering";
import { RealityState, propagateEvents, EventReducer } from "./event_propagation";

export interface RealityAnchorConfig<TState, TPayload> {
  id: string;
  initialState: TState;
  reducer: EventReducer<TState, TPayload>;
}

/**
 * A stable reference point for “this branch of reality”.
 * You feed it events; it maintains a causally-consistent state.
 */
export class RealityAnchor<TState, TPayload> {
  readonly id: string;
  private state: RealityState<TState>;
  private readonly reducer: EventReducer<TState, TPayload>;

  constructor(config: RealityAnchorConfig<TState, TPayload>) {
    this.id = config.id;
    this.state = { tick: 0, value: config.initialState };
    this.reducer = config.reducer;
  }

  getState(): RealityState<TState> {
    return this.state;
  }

  advance(events: CausalEvent<TPayload>[]): RealityState<TState> {
    this.state = propagateEvents(this.state, events, this.reducer);
    this.state = { ...this.state, tick: this.state.tick + 1 };
    return this.state;
  }
}
