// ============================================================================
// 🏛️ Module: Reality Decoupling Engine [v1.0-FINAL]
// Subsystem: Unity DOTS (Entity-Component-System)
// Clearance: Weaver's Syndicate (Mid-Tier)
//
// Finalizes the Hardness Transfer System — production-ready structure enforcing:
//   - SAGA Multiplicative Aggregation Filter
//   - Never-Overwrite Doctrine
//   - K-Math Causal Validation
// ============================================================================

using Unity.Entities;
using Unity.Mathematics;

// ─── I. COMPONENT DEFINITIONS (The Atomic Variables) ────────────────────────

// ∘A COMPONENT: Pure Data, No Logic
// Represents the structural integrity of an entity.
// In the Magisterium, this is the "Saturnian Constraint."
public struct HardnessComponent : IComponentData
{
    public float Value;       // 0.0f (Fluid) to 1.0f (Absolute)
    public bool IsLocked;     // Prevents unauthorized transfer (Silver Tether Lock)
    public Entity SourceID;   // Tracks origin for Never-Overwrite auditing
}

// Ex∘ COMPONENT: The Intent to Mutate
// Signals the system that a property transfer is requested.
// Acts as the "Event Bus" message in component form.
public struct TransferRequestComponent : IComponentData
{
    public Entity TargetEntity;
    public double Timestamp;  // For asynchronous event ordering (Chronogenesis)
    public float Q_Score;     // Epistemic Quality Metric (SAGA Validation)
    public float K_Constant;  // The Causal Weight of this transfer (K-Math Grounding)
}

// Φ COMPONENT: System State Tracking (Never-Overwrite Doctrine)
// Used to prevent cascade failures and audit history.
// Buffer ensures history is additive, never overwritten.
public struct TransferHistoryBuffer : IBufferElementData
{
    public Entity PreviousOwner;
    public double TransferTime;
    public float HarmonizationConstant; // H_c applied during transfer
    public float K_Shift;               // The K-Math delta applied
}
