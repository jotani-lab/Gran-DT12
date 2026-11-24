-- Migración única para bases existentes que todavía usan los nombres anteriores.
-- No ejecutar después de scripts/00_DDL.sql: esa inicialización ya crea las tablas con el nuevo nombre.
USE 5to_GranDT12;

RENAME TABLE
    posiciones TO Posicion,
    equipos TO Equipo,
    usuarios TO Usuario,
    jugadores TO Jugador,
    plantillas TO Plantilla,
    plantilla_titulares TO PlantillaTitular,
    plantilla_suplentes TO PlantillaSuplente,
    puntuaciones TO Puntuacion;
