-- Выборка клиентов с суммой на счету ниже 2000, отсортированных по возрастанию суммы
SELECT c.first_name, c.last_name, SUM(a.balance) AS total_balance
FROM client c
JOIN account a ON c.id = a.client_id
GROUP BY c.id
HAVING SUM(a.balance) < 2000
ORDER BY total_balance ASC;

-- Поиск клиента с минимальной суммой на счете
WITH min_balance AS (
    SELECT SUM(a.balance) AS total_balance
    FROM client c
    JOIN account a ON c.id = a.client_id
    GROUP BY c.id
    ORDER BY total_balance ASC
    LIMIT 1
)
SELECT c.first_name, c.last_name, SUM(a.balance) AS total_balance
FROM client c
JOIN account a ON c.id = a.client_id
GROUP BY c.id
HAVING SUM(a.balance) = (SELECT total_balance FROM min_balance);


-- Подсчет суммы денег у всех клиентов
SELECT SUM(a.balance) AS total_balance
FROM account a;

-- Получение выборки банковских счетов и их владельцев
SELECT c.first_name, c.last_name, a.account_number, a.balance
FROM account a
JOIN client c ON a.client_id = c.id;

-- Вывод клиентов от самых старших к самым младшим
SELECT first_name, last_name, date_of_birth
FROM client
ORDER BY date_of_birth ASC;

-- Подсчет количества человек с одинаковым возрастом
SELECT date_part('year', age(date_of_birth)) AS age, COUNT(*) AS count
FROM client
GROUP BY age;

-- Вывод списка клиентов с указанием их возраста
SELECT first_name, last_name, date_part('year', age(date_of_birth)) AS age
FROM client
ORDER BY age;

-- Выводим только 3 человека из таблицы клиентов
SELECT *
FROM client
LIMIT 3;
