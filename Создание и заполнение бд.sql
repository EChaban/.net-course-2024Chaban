-- Создаем таблицу employee (сотрудники)
CREATE TABLE employee (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    position VARCHAR(50)
);

-- Создаем таблицу client (клиенты)
CREATE TABLE client (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    date_of_birth DATE
);

-- Создаем таблицу account (счета)
CREATE TABLE account (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    client_id UUID,
    account_number VARCHAR(50),
    currency_name VARCHAR(50),
    balance DECIMAL(15, 2),
    FOREIGN KEY (client_id) REFERENCES client(id)
);

-- Попробуем добавить запись с несуществующим client_id
INSERT INTO account (client_id, account_number, currency_name, balance)
VALUES ('123e4567-e89b-12d3-a456-426614174000', '1234567890', 'USD', 1000.00);
-- Эта операция завершится ошибкой из-за внешнего ключа.

-- Наполняем таблицу Employee
INSERT INTO employee (first_name, last_name, position) VALUES
('Иван', 'Иванов', 'Менеджер'),
('Анна', 'Сидорова', 'Клерк'),
('Петр', 'Петров', 'Директор'),
('Мария', 'Кузнецова', 'Бухгалтер');

-- Наполняем таблицу Client
INSERT INTO client (first_name, last_name, date_of_birth) VALUES
('Алексей', 'Смирнов', '1980-01-15'),
('Ольга', 'Лебедева', '1992-03-22'),
('Сергей', 'Григорьев', '1975-11-30'),
('Елена', 'Федорова', '1992-05-10');

-- Наполняем таблицу Account
INSERT INTO account (client_id, account_number, currency_name, balance) VALUES
((SELECT id FROM client WHERE first_name = 'Алексей' AND last_name = 'Смирнов'), '1234567890', 'RUB', 1500.00),
((SELECT id FROM client WHERE first_name = 'Ольга' AND last_name = 'Лебедева'), '0987654321', 'RUB', 2000.00),
((SELECT id FROM client WHERE first_name = 'Сергей' AND last_name = 'Григорьев'), '1122334455', 'RUB', 800.00),
((SELECT id FROM client WHERE first_name = 'Елена' AND last_name = 'Федорова'), '2233445566', 'RUB', 300.00);