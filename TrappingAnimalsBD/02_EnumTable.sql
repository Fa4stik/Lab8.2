CREATE TYPE ROLE_TYPE as ENUM('Оператор по отлову', 'Куратор ВетСлужбы', 
							  'Куратор ОМСУ', 'Куратор по отлову', 
							  'Оператор ВетСлужбы', 'Оператор ОМСУ', 
							  'Подписант ВетСлужбы', 'Подписант ОМСУ', 
							  'Подписант по отлову');
							  
CREATE TYPE ORDER_TYPE as ENUM('План-график', 'Заказ-наряд');

CREATE TYPE OPERATION as ENUM('Удаление карточки из реестра', 'Добавление карточки в реестр',
							 'Изменение карточки', 'Удаление файла');


CREATE TABLE Animal
(
	id SERIAL PRIMARY KEY,
	animalType VARCHAR(50),
	kingColor VARCHAR(50),
	size INT,
	hair VARCHAR(50),
	ears VARCHAR(50),
	tail VARCHAR(50),
	description VARCHAR(50)
);

CREATE TABLE Organisation
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) NOT NULL
);

CREATE TABLE TUser
(
	id SERIAL PRIMARY KEY,
	role ROLE_TYPE NOT NULL,
	id_org INT NOT NULL,
	login VARCHAR(50) UNIQUE,
	passwordHash VARCHAR(66) NOT NULL,
	FOREIGN KEY (id_org) REFERENCES Organisation (id)
);

CREATE TABLE Card
(
	id SERIAL PRIMARY KEY,
	numMK INT NOT NULL,
	dateMK date NOT NULL,
	OMSU VARCHAR(50) NOT NULL,
	executorMK VARCHAR(50) NOT NULL,
	numWorkOrder INT NOT NULL,
	locality VARCHAR(50) NOT NULL,
	dateWorkOrder date NOT NULL,
	targetOrder VARCHAR(50) NOT NULL,
	typeOrder ORDER_TYPE NOT NULL,
	id_org INT NOT NULL,
	accessRoles ROLE_TYPE[],
	animalId INT,
	FOREIGN KEY (id_org) REFERENCES Organisation (id),
	FOREIGN KEY (animalId) REFERENCES Animal (id)
);

CREATE TABLE Log
(
	id SERIAL PRIMARY KEY,
	date date NOT NULL,
	userId INT NOT NULL,
	cardId INT NOT NULL,
	operation OPERATION NOT NULL,
	FOREIGN KEY (userId) REFERENCES TUser (id),
	FOREIGN KEY (cardId) REFERENCES Card (id)
);

-- DROP TYPE Name
-- DROP TABLE Name