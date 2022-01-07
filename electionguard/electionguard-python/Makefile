.PHONY: all environment openssl-fix install install-gmp install-gmp-mac install-gmp-linux install-gmp-windows install-mkdocs auto-lint validate test test-example bench coverage coverage-html coverage-xml coverage-erase generate-sample-data

CODE_COVERAGE ?= 90
OS ?= $(shell python -c 'import platform; print(platform.system())')
ifeq ($(OS), Linux)
PKG_MGR ?= $(shell python -c 'import subprocess as sub; print(next(filter(None, (sub.getstatusoutput(f"command -v {pm}")[0] == 0 and pm for pm in ["apt-get", "pacman"])), "undefined"))')
endif
IS_64_BIT ?= $(shell python -c 'from sys import maxsize; print(maxsize > 2**32)')
SAMPLE_BALLOT_COUNT ?= 5
SAMPLE_BALLOT_SPOIL_RATE ?= 50

all: environment

environment:
	@echo 🔧 ENVIRONMENT SETUP
	make install-gmp
	python -m pip install -U pip
	pip3 install 'poetry==1.1.10'
	poetry config virtualenvs.in-project true 
	poetry install
	@echo 🚨 Be sure to add poetry to PATH