#!/bin/bash
#set -x

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

main() {
    HC_PATH=$(pwd)

    echo "DB1 Global Software - Health Process"

    COMPONENT=$HC_PATH/$1

    if [ "$#" -lt 1 ]; then
        echo ""
        echo "Por favor, forneça o parâmetro!"
        echo ""
        echo "1. db1@dgs$ ./db1-hp db  ---> Only DB"
        echo "2. db1@dgs$ ./db1-hp app ---> Only App + DB"
        echo "3. db1@dgs$ ./db1-hp clear ---> Clear the project"
        echo ""
    else
        if [ "$1" = "db" ]; then
            check_existing_docker
            echo "Digite sua senha de intranet para copiar o arquivo Dump PostgreSQL"
            scp $USER@10.200.10.16:/tmp/backup_all_databases.sql /tmp/
            docker run --name HealthPanelDevPostgres -e POSTGRES_USER=healthpanel -e POSTGRES_PASSWORD=healthpanel -e POSTGRES_DB=healthpanelprocess -d postgres:15.3
            sleep 5
            docker exec -i HealthPanelDevPostgres psql -U healthpanel -d healthpanelprocess < /tmp/backup_all_databases.sql
        elif [ "$1" = "app" ]; then
            check_existing_docker

            echo "Digite sua senha de intranet para copiar o arquivo Dump PostgreSQL"
            scp $USER@10.200.10.16:/tmp/backup_all_databases.sql /tmp/
            docker run --name HealthPanelDevPostgres -e POSTGRES_USER=healthpanel -e POSTGRES_PASSWORD=healthpanel -e POSTGRES_DB=healthpanelprocess -d postgres:15.3
            sleep 5
            docker exec -i HealthPanelDevPostgres psql -U healthpanel -d healthpanelprocess < /tmp/backup_all_databases.sql
            dotnet run appsettings.Development.json &
            sleep 5

            check_existing_browser
        elif [ "$1" = "clear" ]; then
            echo "Cleaning..."
            docker container stop HealthPanelDevPostgres 2>/dev/null
            docker container prune -f 2>/dev/null
            docker image rm -f postgres:15.3 2>/dev/null
            HCPID=`lsof -i :7101 | awk '{print $2}'` 2>/dev/null
            kill -9 $HCPID 2>/dev/null
            sleep 5
            echo "Done !"
        else
            echo
            echo "Parâmetro inválido. Use 'db' ou 'app'."
        fi
    fi
}

main "$@"
