-- 01 SP.sql - Procedimientos Almacenados
USE gran_et12;

DELIMITER //

DROP PROCEDURE IF EXISTS sp_RegistrarPuntuacion //
CREATE PROCEDURE sp_RegistrarPuntuacion(
    IN p_jugador_id INT,
    IN p_nro_fecha INT,
    IN p_nota DECIMAL(4, 2)
)
BEGIN
    IF p_nro_fecha <= 0 OR p_nro_fecha >= 50 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El número de fecha debe ser menor a 50.';
    END IF;
    IF p_nota < 1.0 OR p_nota > 10.0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La nota debe estar entre 1.0 y 10.0.';
    END IF;

    INSERT INTO puntuaciones (jugador_id, nro_fecha, nota)
    VALUES (p_jugador_id, p_nro_fecha, p_nota)
    ON DUPLICATE KEY UPDATE nota = p_nota;
END //

DROP PROCEDURE IF EXISTS sp_CalcularPuntajePlantillaFecha //
CREATE PROCEDURE sp_CalcularPuntajePlantillaFecha(
    IN p_plantilla_id INT,
    IN p_nro_fecha INT
)
BEGIN
    SELECT IFNULL(SUM(p.nota), 0) AS puntaje_total
    FROM plantilla_titulares pt
    INNER JOIN puntuaciones p ON pt.jugador_id = p.jugador_id
    WHERE pt.plantilla_id = p_plantilla_id AND p.nro_fecha = p_nro_fecha;
END //

DELIMITER ;
