// 🏛️ Module: Reality Decoupling Engine [v1.0]
// Subsystem: Entity-Component-System (ECS) / Unity DOTS
// Clearance: Weaver's Syndicate (Mid-Tier)
//
// Translates the Architectural Decoupling framework into executable logic.
// Migrates HardnessComponent from WallEntity → CloakEntity
// while respecting the Never-Overwrite Doctrine and SAGA Multiplicative Aggregation Filter.

using Unity.Entities;
using Unity.Mathematics;

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
public struct TransferRequestComponent : IComponentData
{
    public Entity TargetEntity;
    public float Timestamp;   // For asynchronous event ordering
    public float Q_Score;     // Epistemic Quality Metric (SAGA Validation)
}

// Φ COMPONENT: System State Tracking
// Used to prevent cascade failures during Structural Changes.
public struct StructuralChangeBuffer : IComponentData
{
    public int PendingMigrations;
    public bool IsStabilized;
}
