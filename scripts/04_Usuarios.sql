-- 04 Usuarios.sql - Configuración de Usuarios y Permisos
USE gran_et12;

CREATE USER IF NOT EXISTS 'grandt_user'@'%' IDENTIFIED BY 'grandt_pass';
GRANT ALL PRIVILEGES ON gran_et12.* TO 'grandt_user'@'%';
FLUSH PRIVILEGES;
