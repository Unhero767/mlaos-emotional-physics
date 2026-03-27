import json
import jsonschema
import sys
import logging

logging.basicConfig(level=logging.INFO, format='%(asctime)s - [EPISTEMIC-GATE] - %(levelname)s - %(message)s')

def validate_codex_entry(payload_path, schema_path="codex_schema.json"):
    """
    Acts as the VARO firewall. Rejects any LLM generation that violates ◦A consistency.
    """
    try:
        with open(schema_path, 'r') as file:
            schema = json.load(file)
        with open(payload_path, 'r') as file:
            payload = json.load(file)
            
        jsonschema.validate(instance=payload, schema=schema)
        logging.info(f"Artifact {payload.get('artifact_id')} passed the Aletheia Gate. Q-metric stable.")
        return True
        
    except jsonschema.exceptions.ValidationError as err:
        logging.error(f"Metalogical Burn Detected (Hallucination): {err.message}")
        return False
    except FileNotFoundError as err:
        logging.error(f"Missing Architectural File: {err}")
        return False
    except Exception as e:
        logging.error(f"Systemic Fracture: {e}")
        return False

if __name__ == "__main__":
    if len(sys.argv) < 2:
        logging.warning("No payload provided. Syntax: python3 saga_agent_module.py <payload.json>")
        sys.exit(1)
        
    target_payload = sys.argv[1]
    is_valid = validate_codex_entry(target_payload)
    
    if is_valid:
        sys.exit(0)
    else:
        sys.exit(1)
