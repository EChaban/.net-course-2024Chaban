--Создаем расширение для работы с UUID
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

--Создаем таблицу employee(сотрудники)
CREATE TABLE employee (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    position VARCHAR(50)
);

--Создаем таблицу client(клиенты)
CREATE TABLE client (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    date_of_birth DATE
);

--Создаем таблицу account(счета)
CREATE TABLE account (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    client_id UUID,
    account_number VARCHAR(50),
    currency_name VARCHAR(50),
    balance DECIMAL(15, 2),
    FOREIGN KEY (client_id) REFERENCES client(id)
);

--Попробуем добавить запись с несуществующим client_id
INSERT INTO account (client_id, account_number, currency_name, balance)
VALUES ('123e4567-e89b-12d3-a456-426614174000', '1234567890', 'USD', 1000.00);
--Эта операция завершится ошибкой из-за внешнего ключа.

-- Наполняем таблицу Employee
INSERT INTO employee (first_name, last_name, position) VALUES
('Иван', 'Иванов', 'Менеджер'),
('Анна', 'Сидорова', 'Клерк'),
('Петр', 'Петров', 'Директор'),
('Мария', 'Кузнецова', 'Бухгалтер');

--Наполняем таблицу Client
INSERT INTO client (first_name, last_name, date_of_birth) VALUES
('Алексей', 'Смирнов', '1980-01-15'),
('Ольга', 'Лебедева', '1992-03-22'),
('Сергей', 'Григорьев', '1975-11-30'),
('Елена', 'Федорова', '1992-05-10');

--Наполняем таблицу Account
INSERT INTO account (client_id, account_number, currency_name, balance) VALUES
((SELECT id FROM client WHERE first_name = 'Алексей' AND last_name = 'Смирнов'), '1234567890', 'RUB', 1500.00),
((SELECT id FROM client WHERE first_name = 'Ольга' AND last_name = 'Лебедева'), '0987654321', 'RUB', 2000.00),
((SELECT id FROM client WHERE first_name = 'Сергей' AND last_name = 'Григорьев'), '1122334455', 'RUB', 800.00),
((SELECT id FROM client WHERE first_name = 'Елена' AND last_name = 'Федорова'), '2233445566', 'RUB', 300.00);

--Выборка клиентов с суммой на счету ниже 2000, отсортированных по возрастанию суммы
SELECT c.first_name, c.last_name, SUM(a.balance) AS total_balance
FROM client c
JOIN account a ON c.id = a.client_id
GROUP BY c.id
HAVING SUM(a.balance) < 2000
ORDER BY total_balance ASC;

--Поиск клиента с минимальной суммой на счете
SELECT c.first_name, c.last_name, SUM(a.balance) AS total_balance
FROM client c
JOIN account a ON c.id = a.client_id
GROUP BY c.id
ORDER BY total_balance ASC
LIMIT 1;

--Подсчет суммы денег у всех клиентов
SELECT SUM(a.balance) AS total_balance
FROM account a;

--Получение выборки банковских счетов и их владельцев
SELECT c.first_name, c.last_name, a.account_number, a.balance
FROM account a
JOIN client c ON a.client_id = c.id;

--Вывод клиентов от самых старших к самым младшим
SELECT first_name, last_name, date_of_birth
FROM client
ORDER BY date_of_birth ASC;

--Подсчет количества человек с одинаковым возрастом
SELECT date_part('year', age(date_of_birth)) AS age, COUNT(*) AS count
FROM client
GROUP BY date_part('year', age(date_of_birth))
ORDER BY age;

--Вывод списка клиентов с указанием их возраста
SELECT first_name, last_name, date_part('year', age(date_of_birth)) AS age
FROM client
ORDER BY age;

--Выводим только 3 человека из таблицы клиентов
SELECT *
FROM client
LIMIT 3;

