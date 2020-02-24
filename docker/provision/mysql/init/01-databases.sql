-- CREATE DATABASE IF NOT EXISTS `socialite_production`;
-- CREATE DATABASE IF NOT EXISTS `socialite_identity`;

CREATE USER 'socialite_user'@'%' IDENTIFIED BY 'socialite_pass';

GRANT ALL PRIVILEGES ON `socialite_production`.* TO 'socialite_user'@'%';
GRANT ALL PRIVILEGES ON `socialite_identity`.* TO 'socialite_user'@'%';
GRANT ALL PRIVILEGES ON `socialite_identity_server`.* TO 'socialite_user'@'%';
