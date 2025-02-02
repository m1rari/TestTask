
<img align="right" highth="50%" width="50%" src="https://telegram-bot-sdk.com/img/feature-rich.png">



# 🚀 TestApp (API & DataSeeder)

> 📌 *Проект выполнен в рамках тестового задания.*

## 📌 Описание проекта

**TestApp** — это основное Web API-приложение, предназначенное для работы с данными пациентов. Оно предоставляет REST API для создания, чтения, обновления и удаления данных.

**DataSeeder** — вспомогательное консольное .NET 6-приложение, предназначенное для генерации тестовых данных и отправки их в API.

## 🎯 Возможности

✅ REST API для управления пациентами\
✅ Консольный генератор тестовых данных\
✅ Поддержка Docker для удобного развертывания\
✅ Логирование и обработка ошибок

## 🏗️ Архитектура

```
📂 TestApp
├── 📂 API               # Основное Web API
│   ├── Controllers      # Контроллеры API
│   ├── Services         # Бизнес-логика
│   ├── Models           # Модели данных
│   └── Program.cs       # Точка входа API
│
├── 📂 DataSeeder        # Консольное приложение для генерации данных
│   ├── Services         # Сервисы генерации
│   ├── Models           # Модели данных
│   ├── Resources        # JSON-файлы с именами и фамилиями
│   └── Program.cs       # Точка входа в DataSeeder
│
└── README.md            # Документация проекта
```

## 🚀 Быстрый старт

### 📦 1. Клонирование репозитория

```bash
git clone https://github.com/your-repo/testapp.git
cd testapp
```

### ⚙️ 2. Настройка окружения

Перед запуском убедитесь, что у вас установлен .NET 6 и Docker.

### 🐳 5. Запуск всего стека через Makefile

В корне проекта лежит `Makefile`

Если у вас не утсловлен `Make` на компьютере, откройте powerShell и выполните

📌 Установи Scoop (если он не установлен):

```bash
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
irm get.scoop.sh | iex
```

📌 Теперь установи make:

```bash
scoop install make
```


Теперь для запуска всего стека достаточно выполнить:

```bash
make run
```

После запуска API будет доступен по адресу: http://127.0.0.1:18443/swagger/index.html

### 🔄 4. Запуск генерации данных

```bash
cd DataSeeder
dotnet run
```




## 📜 API-эндпоинты

| Метод    | URL                       | Описание                  |
| -------- | --------------------------| ------------------------- |
| `POST`   | `/api/v1/patients/Create `| Добавить нового пациента  |
| `GET`    | `/api/v1/patients/Get`    | Получить список пациентов |
| `PUT`    | `/api/v1/patients/Update` | Обновить данные пациента  |
| `DELETE` | `/api/v1/patients/Delete/{id}`   | Удалить пациента          |

## 📸 Пример JSON-данных

```json
{
  "name": {"family": "Иванов", "given": ["Иван", "Сергеевич"]},
  "gender": "Male",
  "birthDate": "1995-05-20",
  "active": true
}
```
