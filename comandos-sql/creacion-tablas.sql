CREATE TABLE USUARIO
(ID_USUARIO BIGINT PRIMARY KEY,
NOMBRE VARCHAR(25),
APELLIDO VARCHAR(25),
EDAD INT,
EMAIL VARCHAR(30) UNIQUE,
TELEFONO BIGINT UNIQUE,
CONSTRAINT CHECK_USUARIO CHECK (EDAD >= 15 AND EMAIL LIKE '%@%')
)

CREATE TABLE LIBRO
(
ID_LIBRO BIGINT PRIMARY KEY,
TITULO VARCHAR(50),
NOMBR_EAUTOR VARCHAR(25),
APELLIDO_AUTOR VARCHAR(25),
EDITORIAL VARCHAR(25),
SECCION VARCHAR(25),
CANTIDAD INT
)

CREATE TABLE STAFF
(
ID_STAFF BIGINT PRIMARY KEY,
NOMBRE VARCHAR(25),
APELLIDO VARCHAR(25),
TURNO VARCHAR(25),
CONSTRAINT CHECK_STAFF CHECK (TURNO = 'MATUTINO' OR TURNO = 'VESPERTINO'
OR TURNO = 'NOCTURNO')
)

CREATE TABLE PRESTAMO
(
ID_USUARIO BIGINT FOREIGN KEY REFERENCES USUARIO(ID_USUARIO),
ID_LIBRO BIGINT FOREIGN KEY REFERENCES LIBRO(ID_LIBRO),
ID_STAFF BIGINT FOREIGN KEY REFERENCES STAFF(ID_STAFF),
FECHA_PRESTAMO DATE,
FEHCA_DEVOLUCION DATE,
ESTATUS VARCHAR(25),
CONSTRAINT CHECK_PRESTAMO CHECK (ESTATUS = 'PRESTAMO' OR 
ESTATUS = 'ENTREGADO')
)