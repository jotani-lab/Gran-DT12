COMANDOS Y UBICACIONES PARA CONECTAR LA BASE DE DATOS
=====================================================


1) BASE DE DATOS DEL PROYECTO


La app está configurada para usar la base de datos:
- Nombre: 5to_GranDT12
- Usuario: 5to_agbd
- Password: Trigg3rs!
- Servidor: localhost


Archivo de configuración de la API:
- ProyectoMinimalAPI/appsettings.json


Cadena de conexión esperada:
 `DefaultConnection": "Server=localhost;Database=5to_GranDT12;Uid=5to_agbd;Pwd=Trigg3rs!;`


Archivo de conexión en la API:
- ProyectoMinimalAPI/Program.cs


Allí se usa:
``` csharp
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
   ?? "Server=localhost;Database=5to_GranDT12;User ID=5to_agbd;Password=Trigg3rs!;";
```


2) CREAR LA BASE DE DATOS EN MYSQL WORKBENCH


Abre MySQL Workbench y conectate al servidor local.


Ejecuta esto:


```sql
CREATE DATABASE IF NOT EXISTS 5to_GranDT12;
USE 5to_GranDT12;


-- Luego ejecuta el archivo SQL del proyecto:
- scripts/00 DDL.sql
```


O en una sola línea:
```bash
mysql -u5to_agbd -pTrigg3rs! < "/home/et12alumno/Documentos/GranDT12-V5-main/grandt/scripts/00 DDL.sql"
```


3) VERIFICAR QUE LA BASE EXISTE
```bash
mysql -u5to_agbd -pTrigg3rs! -e "SHOW DATABASES;"
mysql -u5to_agbd -pTrigg3rs! -e "USE 5to_GranDT12; SHOW TABLES;"
```
4) VERIFICAR QUE EL PROGRAMA SE CONECTA


Desde la carpeta del proyecto:
cd /home/et12alumno/Documentos/GranDT12-V5-main/grandt/ProyectoMinimalAPI


dotnet run


Si la conexión funciona, la API arranca sin errores de MySQL.


5) SQL PARA CREAR LA BASE SI HACE FALTA


CREATE DATABASE IF NOT EXISTS 5to_GranDT12;
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
   usuarioId INT NOT NULL UNIQUE,
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


6) RUTA IMPORTANTES


- ProyectoMinimalAPI/Program.cs
- ProyectoMinimalAPI/appsettings.json
- scripts/00 DDL.sql
- scripts/03 Inserts.sql


7) RESUMEN


Si quieres conectar la app y la base:


- Crear la base 5to_GranDT12
- Usar usuario 5to_agbd
- Password Trigg3rs!
- Conectar en localhost
- Ejecutar dotnet run desde ProyectoMinimalAPI







