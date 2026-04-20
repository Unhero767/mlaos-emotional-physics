# MLAOS Emotional Physics Engine - Deployment Guide

## Prerequisites

- Docker Engine 20.10+
- Docker Compose 2.0+
- 2GB+ RAM available
- 4GB+ compute capacity recommended for physics calculations

## Single-Host Deployment (Docker Compose)

### 1. Prepare Environment

```bash
cd ~/mlaos-emotional-physics

# Copy and customize environment
cp .env.example .env

# Edit with your values (optional - defaults work)
nano .env
```

### 2. Build and Deploy

```bash
# Build image
docker build -t mlaos-physics:latest .

# Start engine
docker compose -f docker-compose.prod.yml up -d

# Verify running
docker compose -f docker-compose.prod.yml ps
```

### 3. Verify Engine is Running

```bash
# Check logs
docker compose -f docker-compose.prod.yml logs engine

# Run tests
docker compose -f docker-compose.prod.yml --profile test up test
```

## Kubernetes Deployment

### Generate Manifests

Create `k8s/` directory:

```bash
mkdir -p k8s
```

**k8s/namespace.yaml:**

```yaml
apiVersion: v1
kind: Namespace
metadata:
  name: mlaos-physics
```

**k8s/configmap.yaml:**

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: physics-config
  namespace: mlaos-physics
data:
  EMOTIONAL_KINETICS_SCALE: "1.0"
  MEMORY_DELTA_DECAY: "0.92"
  LUMINOUS_PROBABILITY_THRESHOLD: "0.5"
  PARACONSISTENT_MODE: "enabled"
  MAX_WORKERS: "4"
  LOG_LEVEL: "INFO"
```

**k8s/engine-deployment.yaml:**

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: mlaos-physics-engine
  namespace: mlaos-physics
spec:
  replicas: 2
  strategy:
    type: RollingUpdate
    rollingUpdate:
      maxSurge: 1
      maxUnavailable: 0
  selector:
    matchLabels:
      app: mlaos-physics-engine
  template:
    metadata:
      labels:
        app: mlaos-physics-engine
    spec:
      containers:
      - name: engine
        image: mlaos-physics:latest
        imagePullPolicy: Always
        envFrom:
        - configMapRef:
            name: physics-config
        resources:
          requests:
            memory: "2Gi"
            cpu: "2"
          limits:
            memory: "4Gi"
            cpu: "4"
        livenessProbe:
          exec:
            command:
            - python
            - -c
            - "import master_controller; print('alive')"
          initialDelaySeconds: 30
          periodSeconds: 60
          timeoutSeconds: 10
```

**k8s/engine-service.yaml:**

```yaml
apiVersion: v1
kind: Service
metadata:
  name: mlaos-physics-engine
  namespace: mlaos-physics
spec:
  type: ClusterIP
  selector:
    app: mlaos-physics-engine
  ports:
  - port: 8000
    targetPort: 8000
    protocol: TCP
```

**k8s/test-cronjob.yaml:**

```yaml
apiVersion: batch/v1
kind: CronJob
metadata:
  name: mlaos-physics-test
  namespace: mlaos-physics
spec:
  schedule: "0 2 * * *"  # Daily at 2 AM
  jobTemplate:
    spec:
      template:
        spec:
          containers:
          - name: test
            image: mlaos-physics:latest
            command:
            - python
            - -m
            - pytest
            - -v
            - tests/
          restartPolicy: OnFailure
```

### Deploy to Kubernetes

```bash
# Create namespace
kubectl apply -f k8s/namespace.yaml

# Create config
kubectl apply -f k8s/configmap.yaml

# Deploy engine
kubectl apply -f k8s/engine-deployment.yaml
kubectl apply -f k8s/engine-service.yaml

# Create test job
kubectl apply -f k8s/test-cronjob.yaml

# Verify
kubectl get all -n mlaos-physics
kubectl logs -n mlaos-physics deployment/mlaos-physics-engine
```

## Scaling

### Docker Compose

Scale engine replicas:

```bash
docker compose -f docker-compose.prod.yml up -d --scale engine=3
```

Use a reverse proxy (nginx/traefik) for load balancing.

### Kubernetes

```bash
kubectl scale deployment mlaos-physics-engine -n mlaos-physics --replicas=5
```

## Monitoring

### Logs

```bash
# Docker Compose
docker compose -f docker-compose.prod.yml logs -f engine

# Kubernetes
kubectl logs -n mlaos-physics deployment/mlaos-physics-engine -f
kubectl logs -n mlaos-physics deployment/mlaos-physics-engine --all-containers=true
```

### Health Checks

```bash
# Docker Compose
docker compose -f docker-compose.prod.yml ps

# Kubernetes
kubectl describe deployment mlaos-physics-engine -n mlaos-physics
kubectl get endpoints -n mlaos-physics
```

## Backup & Recovery

### State Snapshot

```bash
# Inside container
docker compose exec engine python master_controller.py --export-state state_backup.json
```

### Recovery

```bash
docker compose exec engine python master_controller.py --import-state state_backup.json
```

## Troubleshooting

### Engine Won't Start

```bash
# Check logs
docker compose -f docker-compose.prod.yml logs engine

# Verify image
docker images | grep mlaos-physics

# Rebuild
docker build -t mlaos-physics:latest .
```

### Out of Memory

```bash
# Check memory usage
docker stats

# Increase limits in docker-compose.prod.yml
# Or Kubernetes resource requests/limits
```

### Tests Failing

```bash
# Run tests manually
docker compose --profile test up test

# Or in Kubernetes
kubectl run test-pod --image=mlaos-physics:latest -n mlaos-physics -it -- python -m pytest tests/ -v
```

## Security Hardening

For production:

1. ✅ Non-root user execution (appuser)
2. ✅ Resource limits defined
3. ✅ Multi-stage build (no build tools in runtime)
4. ✅ Minimal base image (python:3.11-slim)
5. ✅ Environment-based configuration
6. ✅ Health checks for availability

Additional recommendations:

- Use private image registry for production
- Enable image scanning for vulnerabilities
- Implement network policies in Kubernetes
- Use secrets management for sensitive data
- Enable audit logging
- Regular security updates

## Maintenance

### Regular Tasks

Update dependencies:

```bash
# In development
pip install --upgrade -r requirements.txt
docker build -t mlaos-physics:latest .
```

Run stress tests:

```bash
docker compose --profile stress up stress
```

## Performance Tuning

### CPU Optimization

For compute-heavy physics calculations:

```yaml
# docker-compose.prod.yml
deploy:
  resources:
    limits:
      cpus: '8'      # Increase for larger systems
    reservations:
      cpus: '4'
```

### Memory Optimization

Monitor memory usage:

```bash
docker stats mlaos-physics-engine
```

Adjust limits if needed:

```yaml
resources:
  limits:
    memory: 8G    # Increase for complex calculations
```

## Support

For issues, see:
- `README.md` - Overview
- `QUICK_REFERENCE.md` - Commands
- `architecture/` - Technical details

Contact: kennydallmier@gmail.com
