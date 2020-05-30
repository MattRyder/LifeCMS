#!/bin/bash
set -euf -o pipefail

/var/www/lifecms-frontend/generate-runtime-configuration.sh /var/www/lifecms-frontend/runtime/config.js

nginx -g "daemon off;"