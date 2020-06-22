#!/bin/bash

###########
# Build liboqs
#
# Environment variables:
#  - LIBOQS_USE_OPENSSL: the value to pass to the -DOQS_USE_OPENSSL build flag. Can be 'ON' or 'OFF',
#                        and is 'ON' by default.
###########

set -exo pipefail

LIBOQS_USE_OPENSSL=${LIBOQS_USE_OPENSSL:-"ON"}

# For now only support x64 (also hardcoded in scripts folder)
ARCH=x64
INSTALL_DIR=${INSTALL_DIR:-"$(pwd)/$ARCH"}
cd oqs-test/tmp/liboqs

rm -rf build
mkdir build && cd build

# Only build shared
cmake .. -GNinja -DOQS_BUILD_ONLY_LIB=ON -DBUILD_SHARED_LIBS=ON -DOQS_USE_OPENSSL="${LIBOQS_USE_OPENSSL}"
ninja
echo "installing to ${INSTALL_DIR}"
mkdir ${INSTALL_DIR}
cp lib/liboqs.* ${INSTALL_DIR}
