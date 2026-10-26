COMANDOS Y UBICACIONES PARA CONECTAR LA BASE DE DATOS
=====================================================

1) BASE DE DATOS DEL PROYECTO

La app está configurada para usar la base de datos:
- Nombre: gran_et12
- Usuario: 5to_agbd
- Password: Trigg3rs!
- Servidor: localhost

Archivo de configuración de la API:
- ProyectoMinimalAPI/appsettings.json

Cadena de conexión esperada:
"DefaultConnection": "Server=localhost;Database=gran_et12;Uid=5to_agbd;Pwd=Trigg3rs!;"

Archivo de conexión en la API:
- ProyectoMinimalAPI/Program.cs

Allí se usa:
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=gran_et12;User ID=5to_agbd;Password=Trigg3rs!;";

2) CREAR LA BASE DE DATOS EN MYSQL WORKBENCH

Abre MySQL Workbench y conectate al servidor local.

Ejecuta esto:

CREATE DATABASE IF NOT EXISTS gran_et12;
USE gran_et12;

Luego ejecuta el archivo SQL del proyecto:
- scripts/00 DDL.sql

O en una sola línea:
mysql -u5to_agbd -pTrigg3rs! < "/home/et12alumno/Documentos/GranDT12-V5-main/grandt/scripts/00 DDL.sql"

3) VERIFICAR QUE LA BASE EXISTE

mysql -u5to_agbd -pTrigg3rs! -e "SHOW DATABASES;"
mysql -u5to_agbd -pTrigg3rs! -e "USE gran_et12; SHOW TABLES;"

4) VERIFICAR QUE EL PROGRAMA SE CONECTA

Desde la carpeta del proyecto:
cd /home/et12alumno/Documentos/GranDT12-V5-main/grandt/ProyectoMinimalAPI

dotnet run

Si la conexión funciona, la API arranca sin errores de MySQL.

5) SQL PARA CREAR LA BASE SI HACE FALTA

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

6) RUTA IMPORTANTES

- ProyectoMinimalAPI/Program.cs
- ProyectoMinimalAPI/appsettings.json
- scripts/00 DDL.sql
- scripts/03 Inserts.sql

7) RESUMEN

Si quieres conectar la app y la base:

- Crear la base gran_et12
- Usar usuario 5to_agbd
- Password Trigg3rs!
- Conectar en localhost
- Ejecutar dotnet run desde ProyectoMinimalAPI


