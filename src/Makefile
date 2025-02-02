.PHONY: run up down build

# Запуск всего стека
run: build

# Поднятие Docker-контейнеров
up:
	docker-compose -f ./.docker/docker-compose.yml up -d
	
# Остановка контейнеров
down:
	docker compose -f .docker/docker-compose.yml down

# Пересборка контейнеров
build:
	docker-compose -f ./.docker/docker-compose.yml up --build -d
