-- 00 DDL.sql - Creación de Base de Datos y Tablas
CREATE DATABASE IF NOT EXISTS gran_et12;
USE gran_et12;

DROP TABLE IF EXISTS puntuaciones;
DROP TABLE IF EXISTS plantilla_suplentes;
DROP TABLE IF EXISTS plantilla_titulares;
DROP TABLE IF EXISTS plantillas;
DROP TABLE IF EXISTS jugadores;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS equipos;
DROP TABLE IF EXISTS posiciones;

CREATE TABLE posiciones (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE equipos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    fecha_nacimiento DATE NOT NULL,
    password_hash VARCHAR(64) NOT NULL,
    es_admin TINYINT(1) NOT NULL DEFAULT 0
);

CREATE TABLE jugadores (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    apodo VARCHAR(100) NULL,
    fecha_nacimiento DATE NOT NULL,
    equipo_id INT NOT NULL,
    posicion_id INT NOT NULL,
    cotizacion DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (equipo_id) REFERENCES equipos(id),
    FOREIGN KEY (posicion_id) REFERENCES posiciones(id)
);

CREATE TABLE plantillas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL UNIQUE,
    nombre VARCHAR(100) NOT NULL,
    presupuesto_maximo DECIMAL(12, 2) NOT NULL DEFAULT 100000000.00,
    cantidad_maxima_jugadores INT NOT NULL DEFAULT 20,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE
);

CREATE TABLE plantilla_titulares (
    plantilla_id INT NOT NULL,
    jugador_id INT NOT NULL,
    PRIMARY KEY (plantilla_id, jugador_id),
    FOREIGN KEY (plantilla_id) REFERENCES plantillas(id) ON DELETE CASCADE,
    FOREIGN KEY (jugador_id) REFERENCES jugadores(id)
);

CREATE TABLE plantilla_suplentes (
    plantilla_id INT NOT NULL,
    jugador_id INT NOT NULL,
    PRIMARY KEY (plantilla_id, jugador_id),
    FOREIGN KEY (plantilla_id) REFERENCES plantillas(id) ON DELETE CASCADE,
    FOREIGN KEY (jugador_id) REFERENCES jugadores(id)
);

CREATE TABLE puntuaciones (
    id INT AUTO_INCREMENT PRIMARY KEY,
    jugador_id INT NOT NULL,
    nro_fecha INT NOT NULL,
    nota DECIMAL(4, 2) NOT NULL,
    UNIQUE KEY uq_jugador_fecha (jugador_id, nro_fecha),
    FOREIGN KEY (jugador_id) REFERENCES jugadores(id) ON DELETE CASCADE
);
