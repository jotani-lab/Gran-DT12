-- 00 DDL.sql - Creación de Base de Datos y Tablas
DROP DATABASE IF EXISTS 5to_GranDT12;
CREATE DATABASE 5to_GranDT12;

USE 5to_GranDT12;

DROP TABLE IF EXISTS Puntuacion;
DROP TABLE IF EXISTS PlantillaSuplente;
DROP TABLE IF EXISTS PlantillaTitular;
DROP TABLE IF EXISTS Plantilla;
DROP TABLE IF EXISTS Jugador;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Equipo;
DROP TABLE IF EXISTS Posicion;

CREATE TABLE Posicion (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Equipo (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    fechaNacimiento DATE NOT NULL,
    passwordHash VARCHAR(64) NOT NULL,
    esAdmin TINYINT(1) NOT NULL DEFAULT 0
);

CREATE TABLE Jugador (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    apodo VARCHAR(100) NULL,
    fechaNacimiento DATE NOT NULL,
    equipoId INT NOT NULL,
    posicionId INT NOT NULL,
    cotizacion DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (equipoId) REFERENCES Equipo(id),
    FOREIGN KEY (posicionId) REFERENCES Posicion(id)
);

CREATE TABLE Plantilla (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuarioId INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    presupuestoMaximo DECIMAL(12, 2) NOT NULL DEFAULT 100000000.00,
    cantidadMaximaJugadores INT NOT NULL DEFAULT 20,
    FOREIGN KEY (usuarioId) REFERENCES Usuario(id) ON DELETE CASCADE
);

CREATE TABLE PlantillaTitular (
    plantillaId INT NOT NULL,
    jugadorId INT NOT NULL,
    PRIMARY KEY (plantillaId, jugadorId),
    FOREIGN KEY (plantillaId) REFERENCES Plantilla(id) ON DELETE CASCADE,
    FOREIGN KEY (jugadorId) REFERENCES Jugador(id)
);

CREATE TABLE PlantillaSuplente (
    plantillaId INT NOT NULL,
    jugadorId INT NOT NULL,
    PRIMARY KEY (plantillaId, jugadorId),
    FOREIGN KEY (plantillaId) REFERENCES Plantilla(id) ON DELETE CASCADE,
    FOREIGN KEY (jugadorId) REFERENCES Jugador(id)
);

CREATE TABLE Puntuacion (
    id INT AUTO_INCREMENT PRIMARY KEY,
    jugadorId INT NOT NULL,
    nroMes INT NOT NULL,
    nroFecha INT NOT NULL,
    nota DECIMAL(4, 2) NOT NULL,
    UNIQUE KEY uqJugadorMesFecha (jugadorId, nroMes, nroFecha),
    FOREIGN KEY (jugadorId) REFERENCES Jugador(id) ON DELETE CASCADE,
    CHECK (nroMes BETWEEN 1 AND 12),
    CHECK (nroFecha BETWEEN 1 AND 49),
    CHECK (nota BETWEEN 1.0 AND 10.0)
);
