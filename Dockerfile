# Multi-stage build for MLAOS Emotional Physics Engine
# Mathematical framework for emotional kinetics and paraconsistent logic

# Stage 1: Builder
FROM python:3.11-slim AS builder

WORKDIR /build

# Install system dependencies required for building
RUN apt-get update && apt-get install -y --no-install-recommends \
    gcc \
    build-essential \
    && rm -rf /var/lib/apt/lists/*

# Copy requirements first (leverage Docker cache)
COPY requirements.txt .

# Create a virtual environment and install dependencies
RUN python -m venv /opt/venv
ENV PATH="/opt/venv/bin:$PATH"
RUN pip install --no-cache-dir --upgrade pip setuptools wheel && \
    pip install --no-cache-dir -r requirements.txt

# Stage 2: Runtime
FROM python:3.11-slim

WORKDIR /app

# Install only runtime dependencies
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    && rm -rf /var/lib/apt/lists/*

# Copy virtual environment from builder
COPY --from=builder /opt/venv /opt/venv

# Set environment variables
ENV PATH="/opt/venv/bin:$PATH" \
    PYTHONDONTWRITEBYTECODE=1 \
    PYTHONUNBUFFERED=1

# Copy application code - entire project structure
COPY . .

# Create necessary directories
RUN mkdir -p /app/src/mlaos_features /app/src/mlaos_infra /app/stress_tests /app/tests

# Create __init__.py files for Python modules
RUN touch /app/src/__init__.py \
    && touch /app/src/mlaos_features/__init__.py \
    && touch /app/src/mlaos_infra/__init__.py

# Create non-root user for security
RUN useradd -m -u 1000 appuser && \
    chown -R appuser:appuser /app

USER appuser

EXPOSE 8000

# Default command runs tests, can be overridden
CMD ["python", "-m", "pytest", "-v", "tests/"]
