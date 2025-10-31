#!/bin/bash

# Определяем порт из переменной окружения или используем 5000 по умолчанию
PORT=${ASPNETCORE_URLS:-"http://localhost:5000"}
PORT=$(echo $PORT | grep -o '[0-9]\+' | head -1)
PORT=${PORT:-5000}

echo "=== Проверка всех страниц на порту $PORT ==="
echo "1. Главная: http://localhost:$PORT"
echo "2. Каталог: http://localhost:$PORT/Products"
echo "3. Корзина: http://localhost:$PORT/Cart" 
echo "4. Заказы: http://localhost:$PORT/Orders"
echo "5. Админка: http://localhost:$PORT/Admin"
echo "6. Детали товара: http://localhost:$PORT/Products/Details/1"

# Опционально: автоматическая проверка доступности
echo -e "\n=== Проверка доступности... ==="
if curl -f -s -o /dev/null "http://localhost:$PORT"; then
    echo "✅ Приложение доступно на порту $PORT"
else
    echo "❌ Приложение не доступно на порту $PORT"
fi
