-- 03 Inserts.sql - Carga de Datos Iniciales
USE 5to_GranDT12;

INSERT INTO Posicion (id, nombre) VALUES
(1, 'Arquero'),
(2, 'Defensor'),
(3, 'Mediocampista'),
(4, 'Delantero');

INSERT INTO Equipo (id, nombre) VALUES
(1, 'Boca Juniors'),
(2, 'River Plate'),
(3, 'Racing Club'),
(4, 'Independiente'),
(5, 'San Lorenzo');

-- Contraseña admin: Admin1234 (hash SHA-256 de 64 caracteres)
INSERT INTO Usuario (id, nombre, apellido, email, fechaNacimiento, passwordHash, esAdmin) VALUES
(1, 'Admin', 'Sistema', 'admin@gran-et12.local', '1990-01-01', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 1),
(2, 'Usuario', 'Demo', 'usuario@gran-et12.local', '1995-05-15', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 0);

INSERT INTO Jugador (id, nombre, apellido, apodo, fechaNacimiento, equipoId, posicionId, cotizacion) VALUES
(1, 'Sergio', 'Romero', 'Chiquito', '1987-02-22', 1, 1, 5000000.00),
(2, 'Marcos', 'Rojo', 'Marquitos', '1990-03-20', 1, 2, 4500000.00),
(3, 'Nicolas', 'Figal', 'Nico', '1994-04-03', 1, 2, 4000000.00),
(4, 'Luis', 'Advincula', 'Rayo', '1990-03-02', 1, 2, 4200000.00),
(5, 'Frank', 'Fabra', 'Franki', '1991-02-22', 1, 2, 3800000.00),
(6, 'Kevin', 'Zenon', 'Kevi', '2001-07-30', 1, 3, 6000000.00),
(7, 'Cristian', 'Medina', 'Maravilla', '2002-06-01', 1, 3, 6500000.00),
(8, 'Pol', 'Fernandez', 'Pol', '1991-10-11', 1, 3, 4000000.00),
(9, 'Ezequiel', 'Fernandez', 'Equi', '2002-07-25', 1, 3, 7000000.00),
(10, 'Edinson', 'Cavani', 'Matador', '1987-02-14', 1, 4, 9500000.00),
(11, 'Miguel', 'Merentiel', 'Bestia', '1996-02-24', 1, 4, 8500000.00);

INSERT INTO Plantilla (id, usuarioId, nombre, presupuestoMaximo) VALUES
(1, 2, 'Test DT', 100000000.00);

INSERT INTO PlantillaTitular (plantillaId, jugadorId) VALUES
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8), (1, 9), (1, 10), (1, 11);

INSERT INTO Puntuacion (jugadorId, nroMes, nroFecha, nota) VALUES
(1, 11, 1, 8.50), (2, 11, 1, 7.00), (3, 11, 1, 6.50), (4, 11, 1, 9.00), (5, 11, 1, 6.00),
(6, 11, 1, 8.00), (7, 11, 1, 7.50), (8, 11, 1, 6.00), (9, 11, 1, 8.50), (10, 11, 1, 9.50), (11, 11, 1, 8.00);
