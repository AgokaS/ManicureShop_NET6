.PHONY: run clean restore build seed

# Основная команда запуска
run:
	chmod +x run-dev.sh
	./run-dev.sh

# Очистка проекта
clean:
	rm -rf bin obj manicureshop.db

# Восстановление пакетов
restore:
	dotnet restore

# Сборка проекта
build:
	dotnet build

# Запуск без очистки
start:
	dotnet run

# Просмотр логов
logs:
	tail -f manicureshop.db.log || echo "Файл логов не найден"

# Помощь
help:
	@echo "Доступные команды:"
	@echo "  make run     - Полный запуск с очисткой и тестовыми данными"
	@echo "  make start   - Быстрый запуск без очистки"
	@echo "  make clean   - Очистка проекта"
	@echo "  make build   - Сборка проекта"
	@echo "  make restore - Восстановление пакетов"
# Docker commands
docker-build:
	./docker-manage.sh build

docker-run:
	./docker-manage.sh run

docker-dev:
	./docker-manage.sh dev

docker-stop:
	./docker-manage.sh stop

docker-logs:
	./docker-manage.sh logs

docker-shell:
	./docker-manage.sh shell

docker-status:
	./docker-manage.sh status

docker-init:
	./docker-manage.sh init-dev

# Combined commands
dev-docker: docker-init
	@echo "Development environment with Docker is running"

prod-docker: docker-build docker-run
	@echo "Production environment with Docker is running"
