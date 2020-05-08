#!/bin/bash
set -euf -o pipefail

/var/www/socialite-frontend/generate-runtime-configuration.sh /var/www/socialite-frontend/runtime/config.js

nginx -g "daemon off;"