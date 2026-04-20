# MLAOS Physics Engine - Quick Reference

## Essential Commands

### Start
```bash
cd ~/mlaos-emotional-physics
make up            # Start engine
make logs          # Watch logs
```

### Testing
```bash
make test          # Unit tests
make stress        # Stress testing
```

### Production
```bash
cp .env.example .env
make prod-up       # Deploy
make prod-logs     # Monitor
```

### Utilities
```bash
make shell         # Shell access
make help          # All commands
make clean         # Cleanup
```

## File Locations

```
/Users/kennethdallmier/mlaos-emotional-physics/
├── Dockerfile              ✓
├── docker-compose.yml      ✓
├── docker-compose.prod.yml ✓
├── .env.example            ✓
├── Makefile                ✓
├── README.md               ✓
└── [your code files]
```

## Environment Setup

```bash
cp .env.example .env
nano .env          # Edit if needed
```

## Docker Compose Commands

```bash
# Development (default)
docker compose up              # All services
docker compose --profile test up test    # Tests only
docker compose --profile stress up stress # Stress tests

# Production
docker compose -f docker-compose.prod.yml up -d
```

## Common Tasks

### View Logs
```bash
make logs                  # Engine logs
make prod-logs             # Production logs
docker compose logs engine # Direct docker
```

### Run Tests
```bash
make test                  # Run tests
docker compose exec engine pytest tests/ -v  # More control
```

### Connect to Engine
```bash
make shell
# Inside container:
python master_controller.py
python -m pytest tests/
```

## Profiles

**Default (engine):**
```bash
docker compose up
```

**With tests:**
```bash
docker compose --profile test up test
```

**With stress tests:**
```bash
docker compose --profile stress up stress
```

## Production Scaling

```bash
# Scale engine
docker compose -f docker-compose.prod.yml up -d --scale engine=3
```

## Cleanup

```bash
make clean              # Complete cleanup
docker compose down     # Stop only
docker compose down -v  # Stop + remove volumes
```

## Structure

```
Physics Engine
├── master_controller.py (orchestration)
├── dynamic_state_calculator.py (computation)
├── harmonize_state.py (equilibrium)
├── mutate_state.py (evolution)
└── paraconsistent_stress_test.py (validation)
```

## Physics Constants

```
Emotional Kinetics (Θ_E)      = ∫₀^∞ (Δ_M · L_p) e^(-t) dt
Memory Delta (Δ_M)            = 0.92 (decay factor)
Luminous Probability (L_p)    = 0.5 (threshold)
```

## Next Steps

1. `cd ~/mlaos-emotional-physics`
2. `make up` - Start engine
3. `make logs` - View output
4. `make test` - Verify tests pass
5. `make shell` - Explore code

---

Full docs: `README.md` | Complete guide: `architecture/`
