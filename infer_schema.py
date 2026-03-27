import json
import os

SAMPLE_DIR = "codex_samples"
SCHEMA_FILE = "codex_schema.json"

def generate_schema():
    all_data = []
    for filename in os.listdir(SAMPLE_DIR):
        if filename.endswith(".json"):
            filepath = os.path.join(SAMPLE_DIR, filename)
            with open(filepath, 'r') as f:
                all_data.append(json.load(f))

    if not all_data:
        print("No sample data found. Cannot infer schema.")
        return

    inferred_schema = {
        "$schema": "http://json-schema.org/draft-07/schema#",
        "title": "CodexPayload",
        "type": "object",
        "properties": {
            "codex_id": {"type": "string"},
            "timestamp": {"type": "string", "format": "date-time"},
            "content": {
                "type": "object",
                "properties": {
                    "type": {"type": "string"},
                    "value": {
                        "oneOf": [
                            {"type": "string"},
                            {"type": "number"},
                            {
                                "type": "array",
                                "items": {}
                            },
                            {
                                "type": "object",
                                "properties": {
                                    "list": {
                                        "type": "array",
                                        "items": {}
                                    },
                                    "details": {
                                        "type": "object",
                                        "properties": {
                                            "description": {"type": "string"},
                                            "valid": {"type": "boolean"}
                                        },
                                        "required": ["description", "valid"]
                                    }
                                },
                                "required": ["list", "details"]
                            }
                        ]
                    }
                },
                "required": ["type", "value"]
            }
        },
        "required": ["codex_id", "timestamp", "content"]
    }

    with open(SCHEMA_FILE, 'w') as f:
        json.dump(inferred_schema, f, indent=2)
    print(f"Schema generated and saved to {SCHEMA_FILE}")

if __name__ == "__main__":
    generate_schema()
