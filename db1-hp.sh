#!/bin/bash
set -x
echo "Digite sua senha de intranet para copiar o arquivo Dump PostgreSQL"
scp $USER@10.200.10.16:/tmp/backup_all_databases.sql /tmp/

docker run --name HealthPanelDevPostgres -e POSTGRES_USER=healthpanel -e POSTGRES_PASSWORD=healthpanel -e POSTGRES_DB=healthpanelprocess -d postgres:15.3

sleep 5

docker exec -i HealthPanelDevPostgres psql -U healthpanel -d healthpanelprocess < /tmp/backup_all_databases.sql

dotnet run appsettings.Development.json
