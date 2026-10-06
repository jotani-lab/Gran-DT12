#!/bin/bash

echo "Iniciando base de datos GranDT"

# Configuración de credenciales de MySQL
DB_USER="5to_agbd"
DB_PASS="Trigg3rs!"

# Ejecución en orden de los archivos SQL
mysql -u "5to_agbd" -p"Trigg3rs!" < 00_DDL.sql
mysql -u "5to_agbd" -p"Trigg3rs!" < 01_SP.sql
mysql -u "5to_agbd" -p"Trigg3rs!" < 02_Triggers.sql
mysql -u "5to_agbd" -p"Trigg3rs!" < 03_Inserts.sql
mysql -u "5to_agbd" -p"Trigg3rs!" < 04_Usuarios.sql

echo "Base de datos creada con exito."

#chmod +x init_db.sh
#./init_db.sh