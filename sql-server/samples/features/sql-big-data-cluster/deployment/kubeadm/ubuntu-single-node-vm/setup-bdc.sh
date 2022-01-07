#!/bin/bash
set -Eeuo pipefail

if [ "$EUID" -ne 0 ]
  then echo "Please run as root"
  exit
fi

# This is a script to create single-node Kubernetes cluster and deploy BDC on it.
#
export BDCDEPLOY_DIR=bdcdeploy

# Get password as input. It is used as default for controller, SQL Server Master instance (sa account) and Knox.
#
while true; do
    read -s -p "Create Password for Big Data Cluster: " password
    echo
    read -s -p "Confirm your Password: " password2
    echo
    [ "$password" = "$password2" ] && break
    echo "Password mismatch. Please try again."
done


# Name of virtualenv variable used. This
#
export VIRTUALENV_NAME="bdcvenv"
export LOG_FILE="bdcdeploy.log"
export DEBIAN_FRONTEND=noninteractive

# Requirements file.
#
# export REQUIREMENTS_LINK="https://aka.ms/azdata"

# Kube version.
#
KUBE_DPKG_VERSION=1.20.7-00
KUBE_VERSION=1.20.7

# Wait for 5 minutes for the cluster to be ready.
#
TIMEOUT=600
RETRY_INTERVAL=5

# Variables for pulling dockers.
#
export DOCKER_REGISTRY="mcr.microsoft.com"
export DOCKER_REPOSITORY="mssql/bdc"
export DOCKER_TAG="2019-CU13-ubuntu-20.04"

# Variables used for azdata cluster creation.
#
export AZDATA_USERNAME=admin
export AZDATA_PASSWORD=$password
export ACCEPT_EULA=yes
export CLUSTER_NAME=mssql-cluster
export STORAGE_CLASS=local-storage
export PV_COUNT="30"

IMAGES=(
	    mssql-app-service-proxy
)


# Make a directory for installing the scripts and logs.
#
mkdir -p $BDCDEPLOY_DIR
cd $BDCDEPLOY_DIR/
touch $LOG_FILE

# Install all necessary packages: kubernetes, docker, python3, python3-pip, request, azdata.
#
echo ""
echo "######################################################################################"
echo "Starting installing packages..." 

# Install docker.
#
apt-get update -q

apt --yes install \
    software-properties-common \
    apt-transport-https \
    ca-certificates \
    lsb-release \
    gnupg \
    wget \
    curl


curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -

add-apt-repository \
    "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable"

apt update -q
apt-cache policy docker-ce
apt --yes install docker-ce

usermod --append --groups docker $USER

# Install python3, python3-pip, requests.
#
apt install -y libodbc1 odbcinst odbcinst1debian2 unixodbc apt-transport-https libkrb5-dev

# Download and install azdata package
#
echo ""

curl -sL https://packages.microsoft.com/keys/microsoft.asc |
gpg --dearmor |
tee /etc/apt/trusted.gpg.d/microsoft.asc.gpg > /dev/null

code=$(lsb_release -cs)
if [ $code == "focal" ]; then
   add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/prod.list)"
elif [ $code == "bionic" ]; then
   add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/prod.list)"
elif [ $code == "xenial" ]; then
   add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/16.04/prod.list)"
fi

apt-get update
apt-get install -y azdata-cli

cd -

azdata --version
echo "Azdata has been successfully installed."

# Load all pre-requisites for Kubernetes.
#
echo "###########################################################################"
echo "Starting to setup pre-requisites for kubernetes..." 

# Setup the kubernetes preprequisites.
#
echo $(hostname -i) $(hostname) >> /etc/hosts

swapoff -a
sed -i '/swap/s/^\(.*\)$/#\1/g' /etc/fstab

curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | apt-key add -

cat <<EOF >/etc/apt/sources.list.d/kubernetes.list

deb http://apt.kubernetes.io/ kubernetes-xenial main

EOF

