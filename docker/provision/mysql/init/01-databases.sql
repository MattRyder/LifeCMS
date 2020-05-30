-- CREATE DATABASE IF NOT EXISTS `lifecms_production`;
-- CREATE DATABASE IF NOT EXISTS `lifecms_identity`;

CREATE USER 'lifecms_user'@'%' IDENTIFIED BY 'lifecms_pass';

GRANT ALL PRIVILEGES ON `lifecms_production`.* TO 'lifecms_user'@'%';
GRANT ALL PRIVILEGES ON `lifecms_identity`.* TO 'lifecms_user'@'%';
GRANT ALL PRIVILEGES ON `lifecms_identity_server`.* TO 'lifecms_user'@'%';
