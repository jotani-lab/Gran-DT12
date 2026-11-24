-- Migración única para bases existentes: requiere haber ejecutado antes 05_RenombrarTablas.sql.
-- No ejecutar después de scripts/00_DDL.sql, que crea el esquema directamente con los nombres nuevos.
USE 5to_GranDT12;

-- Al terminar, ejecutar scripts/01_SP.sql y scripts/02_Triggers.sql para recrear procedimientos y trigger.
DROP PROCEDURE IF EXISTS sp_RegistrarPuntuacion;
DROP PROCEDURE IF EXISTS sp_CalcularPuntajePlantillaFecha;
DROP PROCEDURE IF EXISTS sp_CalcularPuntajePlantillaMesFecha;
DROP TRIGGER IF EXISTS trg_ValidarMaximoEquipos;

ALTER TABLE Usuario
    RENAME COLUMN fecha_nacimiento TO fechaNacimiento,
    RENAME COLUMN password_hash TO passwordHash,
    RENAME COLUMN es_admin TO esAdmin;

ALTER TABLE Jugador
    RENAME COLUMN fecha_nacimiento TO fechaNacimiento,
    RENAME COLUMN equipo_id TO equipoId,
    RENAME COLUMN posicion_id TO posicionId;

ALTER TABLE Plantilla
    RENAME COLUMN usuario_id TO usuarioId,
    RENAME COLUMN presupuesto_maximo TO presupuestoMaximo,
    RENAME COLUMN cantidad_maxima_jugadores TO cantidadMaximaJugadores;

ALTER TABLE PlantillaTitular
    RENAME COLUMN plantilla_id TO plantillaId,
    RENAME COLUMN jugador_id TO jugadorId;

ALTER TABLE PlantillaSuplente
    RENAME COLUMN plantilla_id TO plantillaId,
    RENAME COLUMN jugador_id TO jugadorId;

ALTER TABLE Puntuacion
    RENAME COLUMN jugador_id TO jugadorId,
    RENAME COLUMN nro_mes TO nroMes,
    RENAME COLUMN nro_fecha TO nroFecha;

ALTER TABLE Puntuacion
    RENAME INDEX uq_jugador_mes_fecha TO uqJugadorMesFecha;
