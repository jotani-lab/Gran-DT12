-- 02 Triggers.sql - Trigger para validar máximo 32 equipos
USE 5to_GranDT12;

DELIMITER //

DROP TRIGGER IF EXISTS trgValidarMaximoEquipos //
CREATE TRIGGER trgValidarMaximoEquipos
BEFORE INSERT ON Equipo
FOR EACH ROW
BEGIN
    DECLARE totalEquipos INT;
    SELECT COUNT(*) INTO totalEquipos FROM Equipo;
    IF totalEquipos >= 32 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No se pueden registrar más de 32 equipos en la liga.';
    END IF;
END //

DELIMITER ;
