CREATE DATABASE course_system;

USE course_system;

CREATE TABLE Courses (
    CourseID INT PRIMARY KEY,
    CourseName VARCHAR(255) NOT NULL,
    Credits INT NOT NULL,
    Prerequisite INT DEFAULT NULL,
    TimeSlot VARCHAR(50) NOT NULL
);

CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE Selections (
    SelectionID INT AUTO_INCREMENT PRIMARY KEY,
    StudentID INT,
    CourseID INT,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

SELECT * FROM Courses;

SELECT * FROM Students;

SELECT * FROM Selections;

INSERT INTO Courses (CourseID, CourseName, Credits, Prerequisite, TimeSlot)
VALUES 
(201, 'Computer Networks', 3, NULL, 'Mon 14:00-15:40'),
(202, 'Computer Networks Lab', 1, 201, 'Mon 18:30-20:10'),
(203, 'Principles of Marxism', 3, NULL, 'Mon 20:20-21:05'),
(204, 'Physical Education III', 1, NULL, 'Tue 8:30-10:10'),
(205, 'Machine Learning', 2, NULL, 'Tue 10:30-12:10'),
(206, 'Digital Image Processing Lab', 0.5, 207, 'Wed 8:30-10:10'),
(207, 'Digital Image Processing', 2, NULL, 'Wed 10:30-12:10'),
(208, 'Database Systems', 3, NULL, 'Wed 14:00-15:40'),
(209, 'Human-computer Interaction', 2, NULL, 'Wed 18:30-20:10'),
(210, 'Numerical Computation', 2, NULL, 'Fri 8:30-10:10'),
(211, 'Numerical Computation Experiments', 2, 210, 'Fri 10:30-12:10');

INSERT INTO Students (StudentID, Name)
VALUES 
(2022180163, 'RuiHao'),
(2023100888, 'XiXi');

UPDATE Courses
SET TimeSlot = 'Mon 15:00-16:35'
WHERE CourseID = 201;

UPDATE Courses
SET TimeSlot = 'Mon 18:30-21:05'
WHERE CourseID = 202;

UPDATE Courses
SET TimeSlot = 'Mon 18:30-21:05'
WHERE CourseID = 203;

UPDATE Courses
SET TimeSlot = 'Wed 14:00-16:35'
WHERE CourseID = 208;

INSERT INTO Courses (CourseID, CourseName, Credits, Prerequisite, TimeSlot)
VALUES (212, 'Inter Communication', 2, NULL, 'Mon 15:50-17:30');

INSERT INTO Courses (CourseID, CourseName, Credits, Prerequisite, TimeSlot)
VALUES 
(213, 'International Organization', 3, NULL, 'Mon 14:00-16:35');

INSERT INTO Selections (StudentID, CourseID)
VALUES 
(2022180163, 201),  -- Student 2022180163 adds Course 201
(2022180163, 202),  
(2022180163, 203),  
(2022180163, 204),  
(2022180163, 205),
(2022180163, 206),
(2022180163, 207),  
(2022180163, 208),
(2022180163, 209),
(2022180163, 210),
(2022180163, 211),
(2022180163, 212),
(2022180163, 213);

INSERT INTO Courses (CourseID, CourseName, Credits, Prerequisite, TimeSlot)
VALUES (214, 'Graduation Project', 12, NULL, 'Thu 8:30-21:05');

INSERT INTO Selections (StudentID, CourseID)
VALUES 
(2022180163, 214);

DELETE FROM Selections
WHERE StudentID = 2022180163 AND CourseID = 214;




SELECT * FROM Selections;


SELECT * FROM Students;  -- show students of the class

SELECT * FROM Courses;  -- display all the courses

INSERT INTO Selections (StudentID, CourseID)  -- add a new course for the student
VALUES 
(2022180163, 206);

DELETE FROM Selections -- remove course
WHERE StudentID = 2022180163 AND CourseID = 206;

SELECT S.StudentID, C.CourseID, C.CourseName, C.Credits, C.TimeSlot  -- display selected course
FROM Selections S
JOIN Courses C ON S.CourseID = C.CourseID
WHERE S.StudentID = 2022180163;







