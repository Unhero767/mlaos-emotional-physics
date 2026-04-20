// ============================================================================
// 🏛️ Module: Reality Decoupling Engine [v1.0-FINAL]
// II. THE SYSTEM LOGIC (The Global Logic)
// Subsystem: Unity DOTS (Entity-Component-System)
// Clearance: Weaver's Syndicate (Mid-Tier)
//
// Φ(X) = f_∘A * f_Ex∘ * f_Mech * f_KMath — any 0 = incineration
// ============================================================================

using Unity.Entities;
using Unity.Collections;

public partial struct HardnessTransferSystem : ISystem
{
    private bool ValidateKConstant(float kConstant)
    {
        return kConstant >= 0.45f && kConstant <= 0.60f;
    }

    private bool ValidateTransfer(
        ref HardnessComponent hardness,
        ref TransferRequestComponent request)
    {
        float score_A     = hardness.IsLocked ? 0.0f : 1.0f;
        float score_Ex    = (request.Q_Score >= 0.98f) ? 1.0f : 0.0f;
        float score_Mech  = (hardness.Value > 0.0f) ? 1.0f : 0.0f;
        float score_KMath = ValidateKConstant(request.K_Constant) ? 1.0f : 0.0f;
        float totalViability = score_A * score_Ex * score_Mech * score_KMath;
        return totalViability >= 0.98f;
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (hardness, request, entity) in
            SystemAPI.Query<RefRW<HardnessComponent>, RefRO<TransferRequestComponent>>()
                     .WithEntityAccess())
        {
            if (!ValidateTransfer(ref hardness.ValueRW, ref request.ValueRO))
            {
                ecb.DestroyEntity(entity);
                continue;
            }

            ecb.AppendToBuffer(request.ValueRO.TargetEntity, new TransferHistoryBuffer
            {
                PreviousOwner         = entity,
                TransferTime          = request.ValueRO.Timestamp,
                HarmonizationConstant = hardness.ValueRO.Value,
                K_Shift               = request.ValueRO.K_Constant
            });

            ecb.RemoveComponent<HardnessComponent>(entity);

            ecb.AddComponent(request.ValueRO.TargetEntity, new HardnessComponent
            {
                Value    = hardness.ValueRO.Value,
                IsLocked = true,
                SourceID = entity
            });

            ecb.DestroyEntity(entity);
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
