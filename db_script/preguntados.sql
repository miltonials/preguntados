-- Creación de la base de datos preguntados (si no existe)
CREATE DATABASE IF NOT EXISTS preguntados;
USE preguntados;

-- Creación de la tabla Jugadores para almacenar la información de los jugadores
DROP TABLE IF EXISTS Historial;
DROP TABLE IF EXISTS Jugadores;
DROP TABLE IF EXISTS Preguntas;

CREATE TABLE Jugadores (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Creación de la tabla Preguntas para almacenar las preguntas del juego
CREATE TABLE Preguntas (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Pregunta VARCHAR(500) NOT NULL,
    OpcionA VARCHAR(200) NOT NULL,
    OpcionB VARCHAR(200) NOT NULL,
    OpcionC VARCHAR(200) NOT NULL,
    RespuestaCorrecta CHAR(1) NOT NULL
);

-- Creación de la tabla Historial para almacenar los resultados de las partidas los jugadores
CREATE TABLE Historial (
	ID VARCHAR(30) PRIMARY KEY,
	jugadorID INT NOT NULL,
    Aciertos INT,
    FechaHora DATETIME NOT NULL,
    FOREIGN KEY (JugadorID) REFERENCES Jugadores(ID)
);

-- Crear la función que retorna una tabla con 10 preguntas aleatorias
DROP VIEW IF EXISTS vPreguntasAleatorias;
CREATE VIEW vPreguntasAleatorias AS
SELECT Id, Pregunta, OpcionA, OpcionB, OpcionC, RespuestaCorrecta
FROM Preguntas
ORDER BY RAND()
LIMIT 10;


-- Inserts de preguntas
INSERT INTO Preguntas (Pregunta, OpcionA, OpcionB, OpcionC, RespuestaCorrecta)
VALUES ('¿Cuál es la capital de Francia?', 'Berlín', 'Londres', 'París', 'C'),
	('¿En qué país se encuentra el Taj Mahal?', 'India', 'China', 'Egipto', 'A'),
	('¿Cuál es el río más largo del mundo?', 'Nilo', 'Amazonas', 'Mississippi', 'B'),
	('¿Cuál es el océano más grande?', 'Pacífico', 'Atlántico', 'Índico', 'A'),
	('¿Cuál es el metal más pesado?', 'Plomo', 'Hierro', 'Oro', 'A'),
	('¿Cuál es el animal terrestre más grande?', 'Elefante', 'Jirafa', 'Hipopótamo', 'B'),
	('¿En qué año llegó el hombre a la luna?', '1969', '1972', '1965', 'A'),
	('¿Cuál es el país con más población en el mundo?', 'India', 'Estados Unidos', 'China', 'C'),
	('¿Qué famoso pintor fue conocido por cortarse una oreja?', 'Leonardo da Vinci', 'Vincent van Gogh', 'Pablo Picasso', 'B'),
	('¿Cuál es el ave más grande del mundo?', 'Águila', 'Pingüino Emperador', 'Avestruz', 'C'),
	('¿Cuál es el planeta más cercano al Sol?', 'Venus', 'Tierra', 'Mercurio', 'C'),
	('¿Quién escribió la obra "Romeo y Julieta"?', 'William Shakespeare', 'Charles Dickens', 'Jane Austen', 'A'),
	('¿Cuál es el metal líquido a temperatura ambiente?', 'Hierro', 'Mercurio', 'Plomo', 'B'),
	('¿Cuál es el continente más grande?', 'África', 'América', 'Asia', 'C'),
	('¿En qué país se encuentra la Torre Eiffel?', 'Reino Unido', 'Francia', 'Alemania', 'B'),
	('¿Cuál es el océano más profundo?', 'Pacífico', 'Atlántico', 'Índico', 'A'),
	('¿Quién pintó la "Mona Lisa"?', 'Leonardo da Vinci', 'Pablo Picasso', 'Vincent van Gogh', 'A'),
	('¿En qué país se encuentra la Gran Muralla China?', 'India', 'China', 'Japón', 'B'),
	('¿Cuál es el desierto más grande del mundo?', 'Sahara', 'Atacama', 'Gobi', 'A'),
	('¿En qué país se encuentra el Coliseo Romano?', 'Italia', 'Grecia', 'Egipto', 'A');



DELIMITER $$
DROP FUNCTION IF EXISTS fValidarRespuesta;
CREATE FUNCTION fValidarRespuesta(
    p_idPregunta INT,
    p_respuesta CHAR(1),
    p_idSesion VARCHAR(30)
)
RETURNS INT
BEGIN
    DECLARE respuesta CHAR(1);
    DECLARE existeSesion int;
    DECLARE isNull int;
    
    SELECT RespuestaCorrecta INTO respuesta FROM Preguntas WHERE ID = p_idPregunta;
    SELECT COUNT(*) INTO existeSesion FROM Historial WHERE ID = p_idSesion;
    
    
    IF respuesta = p_respuesta AND existeSesion > 0 THEN
		SELECT COUNT(*) INTO isNull FROM Historial WHERE Aciertos IS NULL AND id = p_idSesion;
        
        IF isNull = 0 THEN
			UPDATE Historial SET Aciertos = Aciertos + 1 WHERE ID = p_idSesion;
		ELSE 
			UPDATE Historial SET Aciertos = 1 WHERE ID = p_idSesion;
		END IF;
        
        RETURN 1;
	ELSE
		IF existeSesion > 0 THEN 
			UPDATE Historial SET Aciertos = 0 WHERE ID = p_idSesion;
			RETURN -1;
		ELSE
			RETURN -1;
		END IF;
    END IF;
END$$
DELIMITER ;

DELIMITER //
DROP PROCEDURE IF EXISTS RegistrarJugador;
CREATE PROCEDURE RegistrarJugador(
    IN p_nombre VARCHAR(100)
)
BEGIN
    DECLARE jugador_count INT;
    DECLARE nombre_mayusculas VARCHAR(100);
    DECLARE partida_id INT;
    DECLARE jugador_id INT;
    
    -- Convertir el nombre a mayúsculas
    SET nombre_mayusculas = UPPER(p_nombre);
    
    -- Verificar si el nombre del jugador ya existe
    SELECT COUNT(*) INTO jugador_count
    FROM jugadores
    WHERE nombre = nombre_mayusculas;
    
    -- Si el jugador no existe, agregarlo a la tabla
    IF jugador_count = 0 THEN
        INSERT INTO jugadores (nombre) VALUES (nombre_mayusculas);
    END IF;
    
    -- Agreagar un nuevo registro de historial
    SELECT ID INTO jugador_id FROM Jugadores
		WHERE Nombre = nombre_mayusculas;
        
    DELETE FROM Historial
		WHERE Aciertos IS NULL AND jugadorID >= 0;
        
	SELECT COUNT(*) + 1 INTO partida_id
		FROM historial 
        WHERE JugadorId = jugador_id;
    
    INSERT INTO historial (ID, jugadorID, FechaHora) VALUES (CONCAT(nombre_mayusculas, partida_id), jugador_id, DATE_SUB(NOW(), INTERVAL 6 HOUR));
END;
//
DELIMITER ;