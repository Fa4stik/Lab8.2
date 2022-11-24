INSERT INTO Animal (animalType, kingColor, size, hair, ears, tail, description)
VALUES 
('Собака', 'Белая', 143, 'Кудрявые', 'Маленькие', 'Пушистый', NULL),
('Кошка', 'Рыжая', 67, 'Прямые', 'Короткий', NULL, NULL),
('Обезьяна', 'Корничевая', 121, 'Нет', NULL, NULL, NULL),
('Орёл', 'Светло-корничевый', 102, 'Шерстянные', 'Маленькие', NULL, NULL);

INSERT INTO Organisation (nameOrg, firstNameDir, surNameDir, patronymicDir, adress, phoneNumber)
VALUES
('ООО Отлов', 'Смирнов', 'Иван', 'Андреевич', 'Ленина 38', '892373849582'),
('МирЖивотным', 'Ратыков', 'Илья', 'Сергеевич', 'Перекопская 15а', '892373843582');

INSERT INTO OMSU (nameOMSU, munFormation)
VALUES
('Калиннский административный район', 'Калинский муниципальный район'),
('Центральный административный район', 'Центральный мениципальный район');

-- Запустить в новом запросе
INSERT INTO TUser (role, id_org, login, passwordHash)
VALUES
('Оператор по отлову', 1, 'operOtl', 'f1e30885d8bfb15d355c24006f6c03af21331b0c976e1efda12a50921026af96'),
('Куратор ВетСлужбы', 2, 'kurVet', '2dfa2f91fd942d2ce908a02de291dd4f6c1370ce347aaa97f70c747a82ef1478'),
('Куратор ОМСУ', 2, 'kurOMSU', '18572d82a8e645a37cd8b155c8fa5c0b9ef8d1dab86a508251f2da1c71d1cf7b'),
('Куратор по отлову', 1, 'kurOtl', '899068f26e62ed866965daf48e0e9c59d1760652d76b950937fbca9a089a0169'),
('Оператор ВетСлужбы', 1, 'operVet', '8e0dc1dc0545b219da1ff686bc2aa90cf2faf01246ae2e6c090455bbc92181c2'),
('Оператор ОМСУ', 2, 'operOMSU', '017bd3af8b62ab91955742fcfa1bb68dbf1612fffe6836c712f3afbfc4535078'),
('Подписант ВетСлужбы', 2, 'podpisVet', '4a491a3690c6c4a409ed31aca5bbd3d958b111979d3b59d47a6d6148198cd5a5'),
('Подписант ОМСУ', 1, 'podpisOMSU', '671fecc874b8a2ce874de8e7c9470c71f4ff7584d6f967071f0bddaa4c077dd0'),
('Подписант по отлову', 2, 'podpisOtl', '3b5dd350c00c85b94cde6c27e3429238cdd82a61fdbe2247651b6e85c63a4bbd');

INSERT INTO Card (numMK, dateMK, OMSU, executorMK, numWorkOrder, locality, dateWorkOrder, targetOrder, typeOrder, id_org, accessRoles, animalId)
VALUES
(23123, '13.05.2018', 'OMSU1', 'executorMK1', 23, 'locality1', '20.05.2018', 'targetOrder1', 'Заказ-наряд', 1, '{"Оператор по отлову", "Куратор ВетСлужбы", "Куратор ОМСУ", "Подписант ОМСУ"}', NULL),
(35121, '03.11.2020', 'OMSU2', 'executorMK2', 32, 'locality2', '27.11.2020', 'targetOrder2', 'План-график', 2, '{"Оператор по отлову","Куратор ВетСлужбы", "Подписант по отлову"}', 2),
(31989, '05.01.2021', 'OMSU3', 'executorMK3', 32, 'locality3', '11.01.2020', 'targetOrder3', 'План-график', 1, '{"Оператор по отлову","Подписант ВетСлужбы", "Оператор ОМСУ"}', 1),
(54898, '19.03.2021', 'OMSU4', 'executorMK4', 32, 'locality4', '15.02.2021', 'targetOrder4', 'Заказ-наряд', 1, '{"Оператор по отлову","Куратор ВетСлужбы", "Подписант по отлову"}', 3),
(19937, '09.02.2022', 'OMSU5', 'executorMK5', 32, 'locality5', '16.04.2021', 'targetOrder5', 'План-график', 2, '{"Оператор по отлову","Подписант по отлову", "Куратор ОМСУ"}', 3),
(65252, '28.04.2022', 'OMSU6', 'executorMK6', 32, 'locality6', '05.03.2022', 'targetOrder6', 'Заказ-наряд', 2, '{"Оператор по отлову","Куратор по отлову", "Оператор ВетСлужбы"}', 4);

-- Запустить в новом запросе
INSERT INTO Log (date, userId, cardId, operation)
VALUES
('13.05.2018', 2, 1, 'Добавление карточки в реестр'),
('19.07.2018', 1, 1, 'Удаление карточки из реестра'),
('23.06.2019', 3, 1, 'Изменение карточки'),
('28.12.2020', 5, 1, 'Удаление файла');