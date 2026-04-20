// TIER_III/schemas/temporal_event.schema.ts

export type EventType =
  | 'WEAVE_COMPLETE'
  | 'Q_GATE_PASSED'
  | 'ANNULAR_PURGE'
  | 'CONTRADICTION_LOGGED'
  | 'REALITY_RENDERED';

export interface TemporalEvent {
  event_id: string;
  event_type: EventType;
  source_node: string;
  dependencies?: string[];
  payload: {
    type: string;
    [key: string]: unknown;
  };
  causal_vector?: {
    node_id: string;
    timestamp: number;
    sequence_number: number;
    dependencies: string[];
  };
}
