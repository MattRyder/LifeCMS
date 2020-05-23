import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as FontAwesomeIcons from '@fortawesome/free-solid-svg-icons';

export const Icons = {
    attentionTriangle: FontAwesomeIcons.faExclamationTriangle,
    chat: FontAwesomeIcons.faComment,
    ellipsisHorizontal: FontAwesomeIcons.faEllipsisH,
    folder: FontAwesomeIcons.faFolderOpen,
    logo: FontAwesomeIcons.faCubes,
    notification: FontAwesomeIcons.faBell,
    newspaper: FontAwesomeIcons.faNewspaper,
    message: FontAwesomeIcons.faEnvelope,
    film: FontAwesomeIcons.faFilm,
    photo: FontAwesomeIcons.faPhotoVideo,

};

export default ({ icon, size }) => <FontAwesomeIcon icon={icon} size={size} />;
