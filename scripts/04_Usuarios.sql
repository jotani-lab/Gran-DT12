-- 04 Usuarios.sql - Configuración de Usuarios y Permisos
USE 5to_GranDT12;

CREATE USER IF NOT EXISTS 'grandt_user'@'%' IDENTIFIED BY 'grandt_pass';
GRANT ALL PRIVILEGES ON 5to_GranDT12.* TO 'grandt_user'@'%';
FLUSH PRIVILEGES;
