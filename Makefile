.PHONY: help build up down logs test stress shell clean lint prod-up prod-down docker-build

help:
	@echo "MLAOS Emotional Physics Engine - Available Commands"
	@echo ""
	@echo "Development:"
	@echo "  make up              - Start engine (master controller)"
	@echo "  make down            - Stop all containers"
	@echo "  make logs            - View engine logs"
	@echo "  make test            - Run test suite"
	@echo "  make stress          - Run paraconsistent stress test"
	@echo "  make shell           - Open shell in engine container"
	@echo ""
	@echo "Production:"
	@echo "  make prod-up         - Start production engine"
	@echo "  make prod-down       - Stop production"
	@echo "  make prod-logs       - View production logs"
	@echo ""
	@echo "Docker:"
	@echo "  make docker-build    - Build Docker image"
	@echo ""
	@echo "Utilities:"
	@echo "  make lint            - Run code linting"
	@echo "  make clean           - Remove generated files"

build:
	docker compose build

up:
	docker compose up -d engine
	docker compose logs -f engine

down:
	docker compose down

logs:
	docker compose logs -f engine

test:
	docker compose --profile test up test

stress:
	docker compose --profile stress up stress

shell:
	docker compose exec engine bash

lint:
	docker compose exec engine python -m pytest --cov=src --cov-report=html

prod-up:
	docker compose -f docker-compose.prod.yml up -d engine

prod-down:
	docker compose -f docker-compose.prod.yml down

prod-logs:
	docker compose -f docker-compose.prod.yml logs -f engine

docker-build:
	docker build -t mlaos-physics:latest .

clean:
	find . -type d -name __pycache__ -exec rm -rf {} +
	find . -type f -name "*.pyc" -delete
	rm -rf .coverage htmlcov .pytest_cache
	docker compose down -v

.DEFAULT_GOAL := help
