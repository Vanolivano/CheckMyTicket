# Тестовое задание:
- Сделать проект web api на .NET Core, которое позволяет пользователям проверить выигрыш в лотерею, по номеру лотерейного билета.
- Вид номера билета: 
- <Номер тиража>-<Номер билета>
- Например: 128-A234D31
Условия:
-	Проверить валидность данных
-	Использовать любое хранилище данных
-	Асинхронный код
-	Заложиться под большие нагрузки
- Написать юнит тесты


# Решение состоит из двух проектов
- CheckMyTicket
- CheckMyTicket.Test

# CheckMyTicket - dotnet core project with api controller
- TicketsController - controller реализут два метода: GetTickets, CheckMyTicket
- [GET: api/Tickets] GetTickets - возвращает список всех билетов
- [POST: api/Tickets/CheckMyTicket] CheckMyTicket - проверяет выигрыш билета по номеру, возвращает true - если выигрыш, false - проигрыш

# CheckMyTicket.Test - xUnit project with two methods CheckMyTicket_CheckStatusCode, CheckMyTicket_CheckReturns
- CheckMyTicket_CheckStatusCode - проверят валидацию метода CheckMyTicket
- CheckMyTicket_CheckReturns - проверяет результат выполнения метода CheckMyTicket

# Коментарии от автора:
- Можно было уточнить несколько моментов, например какую именно валидацию делать (какой именно формат имеет лотерейный билет)
- По поводу БД было принято решение поступить следующим образом: БД состоит из одной таблицы "Билет" с составным первичным ключом "Тираж" и "Номер".
- Данная таблица содержит только выигрышные билеты. В качестве хранилища использовал InMemoryDatabase, все равно доступ производится через ef core
- Сделал небольшое кеширование на методе CheckMyTicket


