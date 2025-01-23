CREATE DATABASE FlightSimulationDB;
USE FlightSimulationDB;
CREATE TABLE FlightLogs (
    timestamp DATETIME,
    user_id INT,
    altitude DOUBLE,
    speed DOUBLE,
    fuel_level DOUBLE
);
INSERT INTO FlightLogs (timestamp, altitude, speed, fuel_level)
VALUES (NOW(), 0, 0, 100);
SELECT * FROM FlightLogs;

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('JohnDoe', 'Beginner', '{"theme": "dark"}');

INSERT INTO FlightLogs (user_id, timestamp, altitude, speed, fuel_level)
VALUES (1, NOW(), 10000, 550, 300);

DESCRIBE FlightLogs;

ALTER TABLE FlightLogs ADD COLUMN user_id INT;

INSERT INTO FlightLogs (user_id, timestamp, altitude, speed, fuel_level)
VALUES (1, NOW(), 10000, 550, 300);

SELECT * FROM FlightLogs;

DROP TABLE IF EXISTS FlightLogs;

CREATE TABLE FlightLogs (
    flight_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    timestamp DATETIME,
    altitude DOUBLE,
    speed DOUBLE,
    fuel_level DOUBLE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

DESCRIBE Users;

ALTER TABLE Users ADD COLUMN user_id INT PRIMARY KEY AUTO_INCREMENT;

DESCRIBE Users;

DROP TABLE IF EXISTS FlightLogs;

CREATE TABLE FlightLogs (
    flight_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    timestamp DATETIME,
    altitude DOUBLE,
    speed DOUBLE,
    fuel_level DOUBLE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);


ALTER TABLE Users ADD COLUMN user_id INT AUTO_INCREMENT PRIMARY KEY;

ALTER TABLE Users CHANGE COLUMN id user_id INT AUTO_INCREMENT PRIMARY KEY;

DROP TABLE IF EXISTS FlightLogs;

CREATE TABLE FlightLogs (
    flight_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    timestamp DATETIME,
    altitude DOUBLE,
    speed DOUBLE,
    fuel_level DOUBLE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

ALTER TABLE Users ADD COLUMN user_id INT AUTO_INCREMENT PRIMARY KEY;

DROP TABLE IF EXISTS FlightLogs;

CREATE TABLE FlightLogs (
    flight_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    timestamp DATETIME,
    altitude DOUBLE,
    speed DOUBLE,
    fuel_level DOUBLE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

DESCRIBE Users;

ALTER TABLE Users ADD COLUMN user_id INT AUTO_INCREMENT PRIMARY KEY;

DROP TABLE IF EXISTS FlightLogs;

CREATE TABLE FlightLogs (
    flight_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    timestamp DATETIME,
    altitude DOUBLE,
    speed DOUBLE,
    fuel_level DOUBLE,
    FOREIGN KEY (user_id) REFERENCES Users(id)
);

SHOW CREATE TABLE FlightLogs;

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('Rui', 'Beginner', '{"theme": "dark"}');

INSERT INTO FlightLogs (user_id, timestamp, altitude, speed, fuel_level)
VALUES (1, NOW(), 10000, 550, 300);

SELECT * FROM FlightLogs;

SELECT * FROM FlightLogs;

INSERT INTO FlightLogs (user_id, timestamp, altitude, speed, fuel_level)
VALUES (1, NOW(), 10000, 550, 300);

SHOW CREATE TABLE FlightLogs;
SHOW CREATE TABLE Users;

SELECT * FROM Users;

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('RuiHao', 'Intermediate', '{"theme": "light"}');

DESCRIBE Users;

ALTER TABLE Users
ADD COLUMN experience_level VARCHAR(20);

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('RuiHao', 'Intermediate', '{"theme": "light"}');

ALTER TABLE Users
ADD COLUMN profile_settings VARCHAR(30);

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('RuiHao', 'Intermediate', '{"theme": "light"}');

ALTER TABLE Users MODIFY COLUMN password VARCHAR(255) NULL;

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('RuiHao', 'Intermediate', '{"theme": "light"}');

DESCRIBE Users;

DESCRIBE Users;
SELECT * FROM Users;

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('HaoRui', 'Beginner', '{"theme": "dark"}');

INSERT INTO FlightLogs (user_id, timestamp, altitude, speed, fuel_level)
VALUES (1, NOW(), 10000, 550, 300);

INSERT INTO Users (username, experience_level, profile_settings)
VALUES ('HaoRui', 'Beginner', '{"theme": "dark"}');


SELECT * FROM FlightLogs;

SELECT * FROM Users;

SELECT * FROM Users;

INSERT INTO FlightLogs (user_id, timestamp, altitude, speed, fuel_level)
VALUES (4, NOW(), 14000, 800, 340);


SELECT * FROM FlightLogs;

DESCRIBE Users;

ALTER TABLE Users RENAME COLUMN experience_level TO experience;

DESCRIBE Users;

SELECT * FROM Users;

UPDATE Users SET experience = ? WHERE id = ?;



UPDATE Users 
SET experience = 'Pro' 
WHERE id = 5;

SELECT * FROM Users;

SHOW TRIGGERS LIKE 'Users';

UPDATE Users 
SET experience = 'Pro'
WHERE id = 5;

SELECT * FROM Users WHERE id = 5;

SELECT * FROM Users;

UPDATE Users 
SET experience = 'Pro' 
WHERE id = 5;

SELECT * FROM Users;

DESCRIBE Users;

UPDATE Users 
SET experience = 'Medium' 
WHERE id = 5;

INSERT INTO Users (username, experience, profile_settings)
VALUES ('DRH', 'Zero', '{"theme": "yellow"}');

SELECT * FROM Users;

UPDATE Users 
SET experience = 'Mid-lvl' 
WHERE id = 16;



