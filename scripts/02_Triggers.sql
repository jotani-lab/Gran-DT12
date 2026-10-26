-- 02 Triggers.sql - Trigger para validar máximo 32 equipos
USE gran_et12;

DELIMITER //

DROP TRIGGER IF EXISTS trg_ValidarMaximoEquipos //
CREATE TRIGGER trg_ValidarMaximoEquipos
BEFORE INSERT ON equipos
FOR EACH ROW
BEGIN
    DECLARE total_equipos INT;
    SELECT COUNT(*) INTO total_equipos FROM equipos;
    IF total_equipos >= 32 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No se pueden registrar más de 32 equipos en la liga.';
    END IF;
END //

DELIMITER ;
