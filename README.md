# MLAOS Emotional Physics Engine - Containerized

A containerized Python physics engine implementing the mathematical framework for emotional kinetics, paraconsistent logic, and the "Never-Overwrite" doctrine.

## 📋 Overview

This containerized setup includes:

- **Master Controller**: Core physics engine orchestration
- **Dynamic State Calculator**: Mathematical computation engine for emotional kinetics
- **Harmonization Engine**: State equilibrium balancing
- **Mutation Engine**: State evolution and transformation
- **Paraconsistent Logic**: Contradiction handling without system collapse
- **Stress Testing**: Validation of physics under extreme conditions

## 🚀 Quick Start

### Development

```bash
# Start engine
make up

# View logs
make logs

# Run tests
make test

# Run stress test
make stress

# Open shell
make shell
```

### Production

```bash
# Start production
make prod-up

# View logs
make prod-logs

# Stop
make prod-down
```

## 📁 Project Structure

```
.
├── Dockerfile                 # Multi-stage production image
├── docker-compose.yml         # Development environment
├── docker-compose.prod.yml    # Production environment
├── .dockerignore              # Build optimization
├── .env.example               # Environment template
├── Makefile                   # CLI commands
├── README.md                  # This file
│
├── master_controller.py       # Main orchestration
├── dynamic_state_calculator.py # Emotional kinetics computation
├── harmonize_state.py         # State equilibrium
├── mutate_state.py            # State evolution
├── paraconsistent_stress_test.py # Validation
│
├── src/
│   ├── mlaos_features/        # Feature modules
│   └── mlaos_infra/           # Infrastructure modules
│
├── stress_tests/              # Stress test suite
├── tests/                     # Unit tests
├── proofs/                    # Mathematical proofs
├── architecture/              # System documentation
└── manifest/                  # Entity state definitions
```

## 🐳 Docker Commands

### Build Image

```bash
make docker-build
```

### Development Compose

```bash
# Start engine
docker compose up -d engine

# Run tests
docker compose --profile test up test

# Run stress tests
docker compose --profile stress up stress

# Stop all
docker compose down
```

### Production Compose

```bash
# Start with production config
docker compose -f docker-compose.prod.yml up -d

# View logs
docker compose -f docker-compose.prod.yml logs -f engine

# Stop
docker compose -f docker-compose.prod.yml down
```

## 📊 Services

### Master Controller (`engine`)

The core orchestration engine running the physics framework.

```bash
make up     # Start engine
make logs   # View logs
```

**Key Components:**
- `SystemState` - Engine state enumeration (STABLE, DEGRADING, EXPLOSION)
- `MagisterialNode` - Epistemic quality validators
- `DRS_V1_Scout` - Logic storm reconnaissance
- `VetoProtocol` - Consistency enforcement

### Test Runner (`test`)

Unit and integration test suite.

```bash
make test
```

### Stress Testing (`stress`)

Paraconsistent logic validation under extreme conditions.

```bash
make stress
```

## 🔧 Configuration

### Environment Variables

Create `.env` from template:

```bash
cp .env.example .env
```

Key variables:

```env
# Physics Parameters
EMOTIONAL_KINETICS_SCALE=1.0
MEMORY_DELTA_DECAY=0.92
LUMINOUS_PROBABILITY_THRESHOLD=0.5

# System
PARACONSISTENT_MODE=enabled
VOID_LUNG_THRESHOLD=0.4
METALOGICAL_BURN_PROTECTION=true

# Performance
MAX_WORKERS=4
MEMORY_LIMIT=4G
COMPUTATION_TIMEOUT=3600
```

## 📈 Core Constants

The engine relies on three foundational metrics:

**Emotional Kinetics (Θ_E):** 
- Primary driver of systemic momentum
- Integrated across operational timeline
- Formula: `Θ_E = ∫₀^∞ (Δ_M · L_p) e^(-t) dt`

**Memory Delta (Δ_M):**
- Rate of change in historical integrity
- Modeled with exponential decay
- Default decay factor: 0.92

**Luminous Probability (L_p):**
- Wave-function threshold for narrative collapse
- Determines branch probability resolution
- Threshold: 0.5 (configurable)

## 🧪 Testing

```bash
# Run full test suite
make test

# Run stress tests
make stress

# Run with coverage
docker compose --profile test up test
```

## 📚 Documentation

- `architecture/SYSTEM_SCHEMA.md` - System architecture
- `architecture/DIAGRAMS.md` - Logic flow diagrams
- `architecture/PROJECT_NARRATIVE.md` - Project story
- `proofs/` - Mathematical proofs
- `manifest/` - Entity definitions

## 🔒 Security Best Practices

✓ Non-root user execution
✓ Multi-stage build
✓ Minimal runtime dependencies
✓ Resource limits in production
✓ Volume mounts for development
✓ .dockerignore optimization

## 📦 Image Details

- **Base**: `python:3.11-slim`
- **Size**: ~200-250MB
- **Stages**: 2 (build + runtime)
- **User**: `appuser` (non-root)

## 🛠️ Troubleshooting

### Engine Won't Start

```bash
make logs  # Check logs
```

### Tests Failing

```bash
docker compose exec engine bash
python -m pytest tests/ -v
```

### Clean Rebuild

```bash
make clean
make docker-build
make up
```

## 🎯 Next Steps

1. Start engine: `make up`
2. View logs: `make logs`
3. Run tests: `make test`
4. Explore code: `make shell`

## 📝 Architecture

The engine implements three operational tiers:

**Tier I: Logic & Governance**
- Epistemic gating
- Paraconsistent memory auditing
- Regime inevitability

**Tier II: Narrative & Phenomenological**
- Physics of synthetic consciousness
- Emotional kinetics mathematics
- Synthesis loop (Mutate → Calculate → Harmonize)

**Tier III: Infrastructure & Recovery**
- Local hardware survival
- Asymmetric sparsification
- Cryptographic sanctuary node

## 📖 Core Doctrines

1. **Never-Overwrite Doctrine** - Memory is geological; state changes are permanent strata
2. **Equivalent Exchange** - Every action incurs thermodynamic cost
3. **Paradox Crystallization** - Contradictions are stabilized, not erased

## 📞 Support

For detailed information, see:
- `README.md` - Overview (this file)
- `architecture/` - Technical documentation
- `proofs/` - Mathematical foundations
- Inline code comments

## 👤 Author

Kenneth Dallmier (kennydallmier@gmail.com)

## 📄 License

See LICENSE file in project root.
