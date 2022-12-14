

INSERT INTO Organisation (nameOrg, firstNameDir, surNameDir, patronymicDir, adress, phoneNumber)
VALUES
('ООО Отлов', 'Смирнов', 'Иван', 'Андреевич', 'Ленина 38', '89237384958'),
('МирЖивотным', 'Ратыков', 'Илья', 'Сергеевич', 'Перекопская 15а', '89237384358');

INSERT INTO Municip (nameMunicip)
VALUES
('Калинский муниципальный район'),
('Центральный мениципальный район');

-- Запустить в новом запросе
INSERT INTO OMSU (nameOMSU,firstNameDir, surNameDir, patronymicDir, adress, phoneNumber, id_municip)
VALUES
('Калиннский административный район','имя1','фамилия1','отчество1','ленина д.5','89515556784', 1),
('Центральный административный район','имя2','фамилия2','отчество2','Республик д.5','89516456784', 2);

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

-- Запустить в новом запросе
INSERT INTO Card (numMK, dateMK, id_OMSU, id_municip, adressTrapping, numWorkOrder, locality, dateWorkOrder, dateTrapping, targetOrder, typeOrder, id_org, accessRoles)
VALUES
(23123, '13.05.2018', 1, 1, 'adressTrapping1', 23, 'locality1', '20.05.2018', '21.09.2012', 'targetOrder1', 'Заказ-наряд', 1, '{"Оператор по отлову", "Куратор ВетСлужбы", "Куратор ОМСУ", "Подписант ОМСУ"}'),
(35121, '03.11.2020', 2, 2, 'adressTrapping2', 32, 'locality2', '27.11.2020', '22.01.2022', 'targetOrder2', 'План-график',  2, '{"Оператор по отлову","Куратор ВетСлужбы", "Подписант по отлову"}'),
(31989, '05.01.2021', 2, 2, 'adressTrapping3', 33, 'locality3', '11.01.2020', '25.11.2017', 'targetOrder3', 'План-график',  1, '{"Оператор по отлову","Подписант ВетСлужбы", "Оператор ОМСУ"}'),
(54898, '19.03.2021', 1, 1, 'adressTrapping4', 42, 'locality4', '15.02.2021', '26.06.2016', 'targetOrder4', 'Заказ-наряд',  1, '{"Оператор по отлову","Куратор ВетСлужбы", "Подписант по отлову"}'),
(19937, '09.02.2022', 1, 1, 'adressTrapping5', 35, 'locality5', '16.04.2021', '19.02.2020', 'targetOrder5', 'План-график',  2, '{"Оператор по отлову","Подписант по отлову", "Куратор ОМСУ"}'),
(65252, '28.04.2022', 2, 2, 'adressTrapping6', 92, 'locality6', '05.03.2022', '20.01.2021', 'targetOrder6', 'Заказ-наряд',  2, '{"Оператор по отлову","Куратор по отлову", "Оператор ВетСлужбы"}');

-- Запустить в новом запросе
INSERT INTO Log (date, id_user, id_card, operation)
VALUES
('13.05.2018', 2, 1, 'Добавление карточки в реестр'),
('19.07.2018', 1, 1, 'Удаление карточки из реестра'),
('23.06.2019', 3, 1, 'Изменение карточки'),
('28.12.2020', 5, 1, 'Удаление файла');