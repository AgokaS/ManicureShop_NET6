#!/bin/bash

# Скрипт запуска ManicureShop с тестовыми данными
echo "=== Запуск ManicureShop ==="

# Останавливаем предыдущие запущенные процессы
echo "Остановка предыдущих процессов..."
pkill -f "dotnet run" || true
sleep 2

# Очищаем bin и obj
echo "Очистка проекта..."
rm -rf bin obj

# Восстанавливаем пакеты
echo "Восстановление пакетов..."
dotnet restore

# Удаляем старую базу данных
echo "Удаление старой базы данных..."
rm -f manicureshop.db

# Создаем папку для изображений если её нет
mkdir -p wwwroot/images

# Создаем заглушку для изображений
echo "Создание заглушки для изображений..."
cat > wwwroot/images/no-image.png << 'EOF'
<svg width="150" height="150" xmlns="http://www.w3.org/2000/svg">
  <rect width="100%" height="100%" fill="#f4f4f4"/>
  <text x="50%" y="50%" text-anchor="middle" dy="0.35em" font-family="Arial" font-size="14" fill="#999">No Image</text>
</svg>
EOF

# Собираем проект
echo "Сборка проекта..."
dotnet build

# Запускаем приложение
echo "Запуск приложения на http://localhost:5000..."
echo "=== Приложение запущено! ==="
echo "Откройте в браузере: http://localhost:5000"
echo "Для остановки нажмите Ctrl+C"
dotnet run