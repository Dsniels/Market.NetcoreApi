USE MASTER 

CREATE TABLE Estudiantes (
   ID INT PRIMARY KEY,
   Nombre VARCHAR(50) NOT NULL,
   Apellido VARCHAR(50) NOT NULL,
   Edad INT,
   CorreoElectronico VARCHAR(100)
);