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

// ============================================================================
// II. THE SYSTEM LOGIC (The Global Logic)
// Iterates over all entities requesting a transfer.
// Applies the SAGA Multiplicative Aggregation Filter before Structural Change.
// ============================================================================

using Unity.Entities;
using Unity.Collections;

// SYSTEM: Operates on Archetypes possessing both Hardness and TransferRequest
public partial struct HardnessTransferSystem : ISystem
{
    // SAGA VALIDATION: Multiplicative Aggregation Filter
    // Φ(X) = f_∘A * f_Ex∘ * f_Mech
    // If any factor is 0, the transfer is incinerated.
    private bool ValidateTransfer(ref HardnessComponent hardness, ref TransferRequestComponent request)
    {
        // 1. Structural Consistency (∘A)
        float score_A = hardness.IsLocked ? 0.0f : 1.0f;

        // 2. Narrative Yield (Ex∘) - Placeholder for thematic resonance check
        float score_Ex = (request.Q_Score >= 0.98f) ? 1.0f : 0.0f;

        // 3. Mechanical Balance (Mech) - Prevents infinite loops
        float score_Mech = (hardness.Value > 0.0f) ? 1.0f : 0.0f;

        // Multiplicative Aggregation
        float totalViability = score_A * score_Ex * score_Mech;

        return totalViability >= 0.98f; // Threshold for Reality Anchor
    }

    public void OnUpdate(ref SystemState state)
    {
        // ENTITY COMMAND BUFFER: Queues Structural Changes to prevent race conditions
        // Ensures "Wilderness" events don't crash the "Anchor-Node"
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (hardness, request, entity) in SystemAPI.Query<RefRW<HardnessComponent>,
                                                                     RefRO<TransferRequestComponent>>()
                                                                     .WithEntityAccess())
        {
            // STEP 1: SAGA Gatekeeping
            if (!ValidateTransfer(ref hardness.ValueRW, ref request.ValueRO))
            {
                // REJECTION: Log to Annular Purge Queue
                // Hardness transfer rejected due to low Epistemic Quality
                ecb.DestroyEntity(entity); // Flag for manual Weaver review
                continue;
            }

            // STEP 2: Architectural Decoupling (The Transfer)
            // Remove Hardness from Source (Wall)
            ecb.RemoveComponent<HardnessComponent>(entity);

            // Add Hardness to Target (Cloak)
            // Note: This triggers a Structural Change (Archetype Migration)
            var newHardness = new HardnessComponent
            {
                Value    = hardness.ValueRO.Value,
                IsLocked = true,   // Re-lock on new entity
                SourceID = entity  // Audit trail for Never-Overwrite
            };

            ecb.AddComponent(request.ValueRO.TargetEntity, newHardness);

            // STEP 3: Clean Up Request
            ecb.DestroyEntity(entity);
        }

        // STEP 4: Commit Structural Changes
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

// ============================================================================
// III. ASYNCHRONOUS EVENT BUS (The Temporal Loom)
// Prevents main thread blocking during high-entropy events (The Wilderness).
// Transfer request is decoupled from execution via a temporary request entity.
// ============================================================================

// EVENT BUS PRODUCER: Issues the transfer request from the "Wilderness"
public void RequestHardnessTransfer(Entity wall, Entity cloak, float qScore)
{
    // Create a temporary request entity — intent is decoupled from execution
    Entity requestEntity = entityManager.CreateEntity();

    entityManager.AddComponentData(requestEntity, new TransferRequestComponent
    {
        TargetEntity = cloak,
        Timestamp    = Time.ElapsedSeconds,
        Q_Score      = qScore  // Passed from SAGA Analyzer
    });

    // Attach HardnessComponent reference to the source (wall)
    // HardnessTransferSystem will consume and migrate this on next frame tick
    entityManager.AddComponentData(requestEntity, new HardnessComponent
    {
        Value    = entityManager.GetComponentData<HardnessComponent>(wall).Value,
        IsLocked = false,  // Unlocked for migration — will be re-locked on target
        SourceID = wall    // Preserves Never-Overwrite audit trail
    });

    // The Temporal Loom: requestEntity persists until HardnessTransferSystem
    // processes it on the next OnUpdate tick, then destroys it via ECB.
    // This ensures Wilderness events never directly mutate Anchor-Node archetypes.
}
