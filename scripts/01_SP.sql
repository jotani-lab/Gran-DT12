-- 01 SP.sql - Procedimientos Almacenados
USE 5to_GranDT12;

DELIMITER //

DROP PROCEDURE IF EXISTS spRegistrarPuntuacion //
CREATE PROCEDURE spRegistrarPuntuacion(
    IN pJugadorId INT,
    IN pNroMes INT,
    IN pNroFecha INT,
    IN pNota DECIMAL(4, 2)
)
BEGIN
    IF pNroMes <= 0 OR pNroMes > 12 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El número de mes debe estar entre 1 y 12.';
    END IF;
    IF pNroFecha <= 0 OR pNroFecha >= 50 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El número de fecha debe ser menor a 50.';
    END IF;
    IF pNota < 1.0 OR pNota > 10.0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La nota debe estar entre 1.0 y 10.0.';
    END IF;

    INSERT INTO Puntuacion (jugadorId, nroMes, nroFecha, nota)
    VALUES (pJugadorId, pNroMes, pNroFecha, pNota)
    ON DUPLICATE KEY UPDATE nota = pNota;
END //

DROP PROCEDURE IF EXISTS spCalcularPuntajePlantillaFecha //
CREATE PROCEDURE spCalcularPuntajePlantillaFecha(
    IN pPlantillaId INT,
    IN pNroFecha INT
)
BEGIN
    SELECT IFNULL(SUM(p.nota), 0) AS puntaje_total
    FROM PlantillaTitular pt
    INNER JOIN Puntuacion p ON pt.jugadorId = p.jugadorId
    WHERE pt.plantillaId = pPlantillaId AND p.nroFecha = pNroFecha;
END //

DROP PROCEDURE IF EXISTS spCalcularPuntajePlantillaMesFecha //
CREATE PROCEDURE spCalcularPuntajePlantillaMesFecha(
    IN pPlantillaId INT,
    IN pNroMes INT,
    IN pNroFecha INT
)
BEGIN
    IF pNroMes <= 0 OR pNroMes > 12 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El número de mes debe estar entre 1 y 12.';
    END IF;

    IF pNroFecha <= 0 OR pNroFecha >= 50 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El número de fecha debe ser menor a 50.';
    END IF;

    SELECT IFNULL(SUM(p.nota), 0) AS puntaje_total
    FROM PlantillaTitular pt
    INNER JOIN Puntuacion p ON pt.jugadorId = p.jugadorId
        WHERE pt.plantillaId = pPlantillaId
            AND p.nroMes = pNroMes
            AND p.nroFecha = pNroFecha;
END //

DELIMITER ;
