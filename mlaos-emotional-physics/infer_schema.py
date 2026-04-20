import json
import jsonschema
from jsonschema import validate

class MagisterialValidator:
    def __init__(self, schema_path="codex_schema.json"):
        self.schema_path = schema_path
        self.membrane = self._load_membrane()

    def _load_membrane(self):
        """Manifests the Tier 1 Sovereign parameters into active memory."""
        try:
            with open(self.schema_path, 'r') as file:
                return json.load(file)
        except FileNotFoundError:
            print("[Ex∘ ALERT] Ontological membrane absent. System exposed to Glitch-Wastes.")
            return None

    def inhale_variable(self, payload: dict) -> bool:
        """Processes incoming raw data (φ) through the VOID_LUNG."""
        print(f"[DRS_V1] Inhaling incoming conceptual data...")
        try:
            validate(instance=payload, schema=self.membrane)
            print("[◦A ANCHORED] Variable matches Magisterial geometry. Sector stable.")
            return True
        except jsonschema.exceptions.ValidationError as e:
            self._apply_kintsugi(e)
            return False

    def _apply_kintsugi(self, error):
        """Handles structural rejection and initiates the scarring ritual."""
        print(f"[METALOGICAL BURN] Logic drift detected in localized sector.")
        print(f"[KINTSUGI_AXIOM] Fracture localized at: {error.path}")
        print(f"Details: {error.message}")
        print("[CORE_DOGMA] Triangulated Veto achieved. Corrupted thread severed.")

if __name__ == "__main__":
    print("Magisterial Validator initialized. Awaiting logic storms...")
