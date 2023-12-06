#!/bin/bash
#set -x
# ===========================================================================
# Licensed Materials - Property of DB1 Global Software
# "Restricted Materials of DB1 Global Software"
# 
# DB1 Scripting
# (C) Copyright DB1 Global Software. 2023. All Rights Reserved
# ===========================================================================
# Title           : db1-hp.sh
# Description     : Automation Script Health Proicess ( DB + APP )
# Author          : levi.alves@db1.com.br
# Date            : 2023-Dec-06
# Version         : 1.0
# ===========================================================================
check_existing_docker() {
    if command -v docker >/dev/null || command -v podman >/dev/null && command -v docker-compose >/dev/null; then
        echo "Docker / Podman e Docker Compose já estão instalados no sistema."
        echo ""
    else
        check_linux_distribution
    fi
}

check_linux_distribution() {
    if [ -f /etc/os-release ]; then
        . /etc/os-release
        if [ "$ID" = "debian" ] || [ "$ID" = "ubuntu" ]; then
            echo "Debian ou Ubuntu detectado."
            install_docker_compose
        elif [ "$ID" = "rhel" ] || [ "$ID" = "centos" ] || [ "$ID" = "fedora" ] ; then
            echo "RedHat, CentOS ou Fedora detectado."
            install_docker_compose
        elif [ "$ID" = "suse" ] || [ "$ID" = "sles" ]; then
            echo "SUSE detectado."
            install_docker_compose
        elif [ "$ID" = "amzn" ]; then
            echo "Amazon Linux detectado."
            install_docker_compose_amzn
        elif [ "$ID" = "ol" ]; then
            echo "Oracle Linux detectado."
            install_docker_compose_ol
        else
            echo "Distribuição não suportada."
        fi
    else
        echo "Arquivo /etc/os-release não encontrado. Não é possível determinar a distribuição."
    fi
}

install_docker_compose() {
    echo "Instalando Docker..."
    curl -fsSL https://get.docker.com -o get-docker.sh
    sudo sh get-docker.sh
    sudo usermod -aG docker $USER
    sudo systemctl start docker
    sudo systemctl enable docker
    sudo rm -f get-docker.sh
    echo "Docker instalado com sucesso."

    echo "Instalando Docker Compose v2..."
    sudo curl -fsSL -o /usr/local/bin/docker-compose https://github.com/docker/compose/releases/download/v2.23.0/docker-compose-linux-x86_64
    sudo chmod +x /usr/local/bin/docker-compose
    echo "Docker Compose v2 instalado com sucesso."
}

install_docker_compose_amzn() {
    echo "Instalando Docker..."
    sudo yum install docker -y
    sudo systemctl start docker
    sudo systemctl enable docker
    echo "Docker instalado com sucesso."

    echo "Instalando Docker Compose v2..."
    sudo curl -fsSL -o /usr/local/bin/docker-compose https://github.com/docker/compose/releases/download/v2.23.0/docker-compose-linux-x86_64
    sudo chmod +x /usr/local/bin/docker-compose
    echo "Docker Compose v2 instalado com sucesso."
}

install_docker_compose_ol() {
    echo "Instalando Docker / Podman..."
    sudo yum install docker -y
    sudo systemctl start podman
    sudo systemctl enable podman
    echo "Docker / Podman instalado com sucesso."

    echo "Instalando Docker Compose v2..."
    sudo curl -fsSL -o /usr/local/bin/docker-compose https://github.com/docker/compose/releases/download/v2.23.0/docker-compose-linux-x86_64
    sudo chmod +x /usr/local/bin/docker-compose
    echo "Docker Compose v2 instalado com sucesso."
}

check_existing_browser() {
    if command -v firefox >/dev/null; then
        firefox https://localhost:7101/swagger/index.html &
    elif command -v google-chrome >/dev/null; then
        google-chrome https://localhost:7101/swagger/index.html &
    else
        echo "Nao existe Firefox ou Google Chrome instalado"
    fi
}

ask_for_scp() {
    while true; do
        read -p "Deseja atualizar o banco de dados? (S/N): " answer
        case $answer in
            [Ss]* ) return 0;;
            [Nn]* ) return 1;;
            * ) echo "Por favor, responda com 'S' para Sim ou 'N' para Não.";;
        esac
    done
}

pgclear() {
    echo "Stopping PostgreSQL..."
    docker container stop HealthPanelDevPostgres 2>/dev/null
    docker container prune -f 2>/dev/null
    sleep 5 2>/dev/null
    echo "Stopped PostgreSQL !"
}

appstop() {
    echo "Stopping App..."
    ps -ef|grep db1-healthpanel-back | grep -v grep | awk '{print $2}' | xargs -l kill 2>/dev/null
    sleep 5 2>/dev/null
    echo "App Stopped Successfully !"
}

main_help() {
    echo ""
    echo "Por favor, forneça os parâmetros corretos!"
    echo ""
    echo "Exemplo de uso:"
    echo "./db1-hp.sh \$USER \$TYPE"
    echo ""
    echo "./db1-hp.sh db1.user db       ---> Only DB"
    echo "./db1-hp.sh db1.user app      ---> Only App + DB"
    echo "./db1-hp.sh db1.user clear    ---> Clear all the project"
    echo "./db1-hp.sh db1.user pgclear  ---> PostgreSQL Clear"
    echo "./db1-hp.sh db1.user appstop  ---> Stop App"
    echo ""
}

main() {
    HC_PATH=$(pwd)
    echo "DB1 Global Software - Health Process"
    USER=$1
    TYPE=$2

    if [ "$#" -lt 2 ]; then
        main_help
    else
        if [ "$TYPE" = "db" ]; then
            check_existing_docker
            if ask_for_scp; then
                echo "Digite sua senha DB1 intranet para copiar o arquivo Dump PostgreSQL"
                scp $USER@10.200.10.16:/tmp/backup_all_databases.sql /tmp/ 2>/dev/null
            fi
            pgclear
            docker run --name HealthPanelDevPostgres -p 5432:5432 -e POSTGRES_USER=healthpanel -e POSTGRES_PASSWORD=healthpanel -e POSTGRES_DB=healthpanelprocess -d postgres:15.3
            sleep 5
            docker exec -i HealthPanelDevPostgres psql -U healthpanel -d healthpanelprocess < /tmp/backup_all_databases.sql
        elif [ "$TYPE" = "app" ]; then
            check_existing_docker
            if ask_for_scp; then
                echo "Digite sua senha DB1 de intranet para copiar o arquivo Dump PostgreSQL"
                scp $USER@10.200.10.16:/tmp/backup_all_databases.sql /tmp/ 2>/dev/null
            fi
            pgclear
            docker run --name HealthPanelDevPostgres -p 5432:5432 -e POSTGRES_USER=healthpanel -e POSTGRES_PASSWORD=healthpanel -e POSTGRES_DB=healthpanelprocess -d postgres:15.3
            sleep 5
            docker exec -i HealthPanelDevPostgres psql -U healthpanel -d healthpanelprocess < /tmp/backup_all_databases.sql
            dotnet run appsettings.Development.json &
            sleep 5
            check_existing_browser         
        elif [ "$TYPE" = "clear" ]; then
            echo "Cleaning up..."
            docker container stop HealthPanelDevPostgres 2>/dev/null
            docker container prune -f 2>/dev/null
            docker image rm -f postgres:15.3 2>/dev/null
            HCPID=`lsof -i :7101 | awk '{print $2}'` 2>/dev/null
            kill -9 $HCPID 2>/dev/null
            sleep 5
            echo "Cleared Successfully !"
        elif [ "$TYPE" = "pgclear" ]; then
            pgclear
        elif [ "$TYPE" = "appstop" ]; then
            appstop   
        else
            echo ""
            echo "Parâmetro inválido."
            echo ""
            main_help
        fi
    fi
}

main "$@"
