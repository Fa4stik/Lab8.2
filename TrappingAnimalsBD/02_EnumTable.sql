CREATE TYPE ROLE_TYPE as ENUM('Оператор по отлову', 'Куратор ВетСлужбы', 
							  'Куратор ОМСУ', 'Куратор по отлову', 
							  'Оператор ВетСлужбы', 'Оператор ОМСУ', 
							  'Подписант ВетСлужбы', 'Подписант ОМСУ', 
							  'Подписант по отлову');
							  
CREATE TYPE ORDER_TYPE as ENUM('План-график', 'Заказ-наряд');

CREATE TYPE OPERATION as ENUM('Удаление карточки из реестра', 'Добавление карточки в реестр',
							 'Изменение карточки', 'Удаление файла');

CREATE TABLE Organisation
(
	id SERIAL PRIMARY KEY,
	nameOrg VARCHAR(50) NOT NULL,
	firstNameDir VARCHAR(50) NOT NULL,
	surNameDir VARCHAR(50) NOT NULL,
	patronymicDir VARCHAR(50),
	adress VARCHAR(100) NOT NULL,
	phoneNumber VARCHAR(11) NOT NULL
);

CREATE TABLE Municip
(
	id SERIAL PRIMARY KEY,
	nameMunicip VARCHAR(100) NOT NULL
);

CREATE TABLE Files
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(30) NOT NULL,
	file bytea NOT NULL
);

CREATE TABLE Log
(
	id SERIAL PRIMARY KEY,
	date date NOT NULL,
	userLogin VARCHAR(50) NOT NULL,
	id_card INT NOT NULL,
	operation OPERATION NOT NULL
);

CREATE TABLE OMSU
(
	id SERIAL PRIMARY KEY,
	nameOMSU VARCHAR(100) NOT NULL,
	firstNameDir VARCHAR(50) NOT NULL,
	surNameDir VARCHAR(50) NOT NULL,
	patronymicDir VARCHAR(50),
	adress VARCHAR(100) NOT NULL,
	phoneNumber VARCHAR(11) NOT NULL,
	id_municip INT NOT NULL,
	FOREIGN KEY (id_municip) REFERENCES Municip(id)
);

CREATE TABLE TUser
(
	id SERIAL PRIMARY KEY,
	role ROLE_TYPE NOT NULL,
	id_org INT,
	id_OMSU INT,
	login VARCHAR(50) UNIQUE,
	passwordHash VARCHAR(66) NOT NULL,
	FOREIGN KEY (id_org) REFERENCES Organisation (id),
	FOREIGN KEY (id_OMSU) REFERENCES OMSU(id)
);

CREATE TABLE Card
(
	id SERIAL PRIMARY KEY,
	numMK INT NOT NULL, -- номер МК 
	dateMK date NOT NULL, -- дата МК 
	id_OMSU INT NOT NULL, -- орган местного самоуправления
	id_municip INT NOT NULL, -- муниципальное образование
	adressTrapping VARCHAR(50) NOT NULL, -- адрес отлова 
	numWorkOrder INT NOT NULL, -- номер заказ-наряда 
	locality VARCHAR(50) NOT NULL, -- Населённый пункт 
	dateWorkOrder date NOT NULL, -- дата заказ-наряда 
	dateTrapping date NOT NULL, -- дата отлова 
	targetOrder VARCHAR(50) NOT NULL, -- цель отлова 
	typeOrder ORDER_TYPE NOT NULL, -- тип заказ-наряда 
	id_org INT NOT NULL, 
	accessRoles ROLE_TYPE[],
	id_file INT,
	FOREIGN KEY (id_org) REFERENCES Organisation (id),
	FOREIGN KEY (id_municip) REFERENCES Municip (id),
	FOREIGN KEY (id_OMSU) REFERENCES OMSU (id),
	FOREIGN KEY (id_file) REFERENCES Files (id)
);

-- DROP TYPE Name
-- DROP TABLE Name