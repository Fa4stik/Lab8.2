CREATE TYPE ROLE_TYPE as ENUM('Оператор по отлову', 'Куратор ВетСлужбы', 
							  'Куратор ОМСУ', 'Куратор по отлову', 
							  'Оператор ВетСлужбы', 'Оператор ОМСУ', 
							  'Подписант ВетСлужбы', 'Подписант ОМСУ', 
							  'Подписант по отлову');
							  
CREATE TYPE ORDER_TYPE as ENUM('План-график', 'Заказ-наряд');

CREATE TYPE OPERATION as ENUM('Удаление карточки из реестра', 'Добавление карточки в реестр',
							 'Изменение карточки', 'Удаление файла');

CREATE TYPE APPLICANT_TYPE as ENUM('Физическое лицо', 'Юридическое лицо');

CREATE TYPE ANIMAL_TYPE as ENUM('Кошка', 'Котёнок', 'Собака', 'Щенок');

CREATE TYPE ANIMAL_SIZE as ENUM('Большой', 'Средний', 'Маленький');

CREATE TYPE ANIMAL_HAIR as ENUM('Короткошёрстная', 'Длинношёрстная', 'Жесткошёрстная', 'Кудрявая');


CREATE TABLE Animal
(
	id SERIAL PRIMARY KEY,
	animalType ANIMAL_TYPE,
	kingColor VARCHAR(50),
	size ANIMAL_SIZE,
	hair ANIMAL_HAIR,
	ears VARCHAR(50),
	tail VARCHAR(50),
	description VARCHAR(50)
);

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

CREATE TABLE OMSU
(
	id SERIAL PRIMARY KEY,
	nameOMSU VARCHAR(100) NOT NULL,
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
	firstNameExecuter VARCHAR(50) NOT NULL, -- Имя исполнителя 
	surNameExecuter VARCHAR(50) NOT NULL, -- фамилия исполнителя 
	patronymicExecuter VARCHAR(50) NOT NULL, -- отчество исполнителя 
	phoneNumberExecuter VARCHAR(11) NOT NULL, -- номер исполнителя 
	typeApplicant APPLICANT_TYPE NOT NULL, -- тип заявителя 
	firstNameAppl VARCHAR(50) NOT NULL, -- имя заявителя 
	surNameAppl VARCHAR(50) NOT NULL, -- фамилия заявителя 
	patronymicAppl VARCHAR(50), -- отчество заявителя 
	adressAppl VARCHAR(100) NOT NULL, -- адрес заявителя 
	phoneNumberAppl VARCHAR(11) NOT NULL, -- номер заявителя 
	id_org INT NOT NULL, 
	accessRoles ROLE_TYPE[],
	id_animal INT,
	FOREIGN KEY (id_org) REFERENCES Organisation (id),
	FOREIGN KEY (id_animal) REFERENCES Animal (id),
	FOREIGN KEY (id_municip) REFERENCES Municip (id),
	FOREIGN KEY (id_OMSU) REFERENCES OMSU (id)
);

CREATE TABLE Log
(
	id SERIAL PRIMARY KEY,
	date date NOT NULL,
	id_user INT NOT NULL,
	id_card INT NOT NULL,
	operation OPERATION NOT NULL,
	FOREIGN KEY (id_user) REFERENCES TUser (id),
	FOREIGN KEY (id_card) REFERENCES Card (id)
);

-- DROP TYPE Name
-- DROP TABLE Name