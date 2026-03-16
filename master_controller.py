import subprocess
import time
subprocess.run(["python3", "anticipatory_buffer.py"])
def run_synthesis_cycle():
    print("==========================================")
    print("   MLAOS MASTER CONTROLLER: STARTING LOOP ")
    print("==========================================\n")

    # 1. Trigger Mutation (The Trauma)
    print("[STEP 1] Injecting Narrative Trauma...")
    subprocess.run(["python3", "mutate_state.py"])
    time.sleep(1)

    # 2. Trigger Calculation (The Detection)
    print("\n[STEP 2] Running Dynamic Integrity Assessment...")
    subprocess.run(["python3", "dynamic_state_calculator.py"])
    time.sleep(1)

    # 3. Trigger Harmonization (The Recovery)
    print("\n[STEP 3] Applying Never-Overwrite Harmonization...")
    subprocess.run(["python3", "harmonize_state.py"])
    time.sleep(1)

    # 4. Final Verification
    print("\n[STEP 4] Final Verification of Stabilized State...")
    subprocess.run(["python3", "dynamic_state_calculator.py"])

    print("\n==========================================")
    print("   SYNTHESIS CYCLE COMPLETE: RESONANCE ACHIEVED")
    print("==========================================")

if __name__ == "__main__":
    run_synthesis_cycle()
