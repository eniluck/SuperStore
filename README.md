## О проекте
Проект показывает основы работы с RabbitMQ с реализацией поставщика и подписчика.

## Настройки перед запуском приложения
Необходимо запустить docker образ RabbitMQ выполнив команду:
```
docker-compose up -d
```
Вэб интерфейс RabbitMQ будет доступен по адресу: 
```
http://localhost:15672
```
Для входа используется Username и Password по умолчанию ``` guest ```

## Структура
Решение состоит из трёх проектов
- SuperStore.Carts - minimal Web API
- SuperStore.Funds - minimal Web API
- SuperStore.Shared - общие классы реализующие поставщика и подписчика.
