# ManicureShop - Веб-приложение для маникюрного салона

ASP.NET Core MVC приложение для управления услугами маникюрного салона с корзиной, заказами и админ-панелью.

## 🚀 Возможности

- **Каталог услуг** - просмотр и управление услугами маникюра
- **Корзина покупок** - добавление, удаление и изменение количества услуг
- **Система заказов** - оформление и отслеживание заказов
- **Админ-панель** - управление услугами (CRUD операции)
- **Аутентификация** - регистрация и вход через ASP.NET Identity
- **Docker поддержка** - контейнеризация приложения

## 🛠 Технологии

- **Backend**: ASP.NET Core 8.0, Entity Framework Core, SQLite
- **Frontend**: Razor Pages, HTML, CSS
- **Аутентификация**: ASP.NET Core Identity
- **Контейнеризация**: Docker, Docker Compose
- **Сборка**: Makefile, Bash скрипты

## 📁 Структура проекта

```
ManicureShop/
├── Controllers/          # MVC контроллеры
│   ├── AdminController.cs
│   ├── CartController.cs
│   ├── HomeController.cs
│   ├── OrdersController.cs
│   └── ProductsController.cs
├── Models/              # Модели данных
│   ├── ApplicationUser.cs
│   ├── CartItem.cs
│   ├── Order.cs
│   └── Product.cs
├── Data/               # Контекст базы данных
│   └── ApplicationDbContext.cs
├── Views/              # Razor представления
│   ├── Admin/
│   ├── Cart/
│   ├── Home/
│   ├── Orders/
│   ├── Products/
│   └── Shared/
├── wwwroot/            # Статические файлы
│   ├── css/site.css
│   └── images/
└── Скрипты и конфиги
    ├── docker-compose.yml
    ├── Dockerfile
    ├── docker-manage.sh
    ├── makefile
    ├── run-dev.sh
    └── testURL.sh
```

## 🚀 Быстрый старт

### Локальная разработка

```bash
# Клонировать репозиторий
git clone <repository-url>
cd ManicureShop_NET6

# Способ 1: Использовать скрипт (рекомендуется)
./run-dev.sh

# Способ 2: Использовать make
make run

# Способ 3: Ручной запуск
dotnet restore
dotnet build
dotnet run
```

Приложение будет доступно по адресу: http://localhost:5000

### Запуск в Docker

```bash
# Инициализация и запуск в Docker
./docker-manage.sh init-dev

# Или поэтапно:
./docker-manage.sh build    # Сборка образа
./docker-manage.sh run      # Запуск контейнера
```

## 📋 Функциональность

### Для пользователей
- 📋 Просмотр каталога услуг
- 🛒 Добавление услуг в корзину
- 📦 Оформление заказов
- 📊 Просмотр истории заказов
- 🔐 Регистрация и авторизация

### Для администраторов
- ⚡ Управление услугами (добавление, редактирование, удаление)
- 📤 Загрузка изображений услуг
- 👥 Просмотр всех заказов

## 🗄 Модели данных

### Product (Услуга)
- Название, описание, цена
- Путь к изображению
- Связь с заказами

### Order (Заказ)
- Пользователь, дата, общая сумма
- Список позиций заказа (OrderItem)

### CartItem (Элемент корзины)
- Временное хранение в сессии
- Количество, цена, информация о услуге

## 🔧 Скрипты управления

### Основные скрипты
- `run-dev.sh` - запуск в режиме разработки
- `docker-manage.sh` - управление Docker-контейнерами
- `testURL.sh` - проверка доступности страниц

### Команды Docker
```bash
./docker-manage.sh build      # Сборка образа
./docker-manage.sh run        # Запуск в production
./docker-manage.sh dev        # Запуск в development
./docker-manage.sh stop       # Остановка контейнеров
./docker-manage.sh logs       # Просмотр логов
./docker-manage.sh db-backup  # Резервная копия БД
```

### Команды Make
```bash
make run          # Полный запуск с очисткой
make start        # Быстрый запуск
make clean        # Очистка проекта
make docker-dev   # Запуск в Docker
```

## ⚙ Конфигурация

### Настройки базы данных
По умолчанию используется SQLite с файлом `manicureshop.db`. Настройки в `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=manicureshop.db"
  }
}
```

### Переменные окружения
- `ASPNETCORE_URLS` - URL для запуска приложения
- `ASPNETCORE_ENVIRONMENT` - среда выполнения

## 🐛 Тестирование

После запуска проверьте доступность страниц:

```bash
./testURL.sh
```

Или вручную откройте в браузере:
- Главная: http://localhost:5000
- Каталог: http://localhost:5000/Products
- Корзина: http://localhost:5000/Cart
- Админка: http://localhost:5000/Admin

## 📝 Тестовые данные

При первом запуске автоматически создаются тестовые услуги:
- Классический маникюр (1000 руб.)
- SPA маникюр (1500 руб.)
- Гель-лак покрытие (1200 руб.)
- И другие услуги

## 🔒 Безопасность

- ASP.NET Core Identity для аутентификации
- Валидация моделей данных
- Защита от CSRF атак
- Безопасная обработка файлов

## 🚢 Деплой

### Production с Docker
```bash
./docker-manage.sh build
./docker-manage.sh run
```

### Локальный деплой
```bash
dotnet publish -c Release -o ./publish
```

## 📞 Поддержка

При возникновении проблем:

1. Проверьте логи: `./docker-manage.sh logs` или `make logs`
2. Убедитесь, что порт 5000 свободен
3. Проверьте наличие прав на выполнение скриптов: `chmod +x *.sh`
4. Для проблем с БД: используйте `./docker-manage.sh db-backup`

## 📄 Лицензия

Учебный проект для демонстрации возможностей ASP.NET Core MVC.
