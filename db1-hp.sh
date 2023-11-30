#!/bin/bash
set -x

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
    check_existing_docker

    echo "Digite sua senha de intranet para copiar o arquivo Dump PostgreSQL"
    scp $USER@10.200.10.16:/tmp/backup_all_databases.sql /tmp/
    docker run --name HealthPanelDevPostgres -e POSTGRES_USER=healthpanel -e POSTGRES_PASSWORD=healthpanel -e POSTGRES_DB=healthpanelprocess -d postgres:15.3
    sleep 5
    docker exec -i HealthPanelDevPostgres psql -U healthpanel -d healthpanelprocess < /tmp/backup_all_databases.sql
    dotnet run appsettings.Development.json &
    sleep 5

    check_existing_browser
}

main
