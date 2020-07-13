import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as FontAwesomeIcons from '@fortawesome/free-solid-svg-icons';

export const Icons = {
    attentionTriangle: FontAwesomeIcons.faExclamationTriangle,
    chat: FontAwesomeIcons.faComment,
    cog: FontAwesomeIcons.faCog,
    ellipsisHorizontal: FontAwesomeIcons.faEllipsisH,
    folder: FontAwesomeIcons.faFolderOpen,
    logo: FontAwesomeIcons.faCubes,
    notification: FontAwesomeIcons.faBell,
    newspaper: FontAwesomeIcons.faNewspaper,
    message: FontAwesomeIcons.faEnvelope,
    film: FontAwesomeIcons.faFilm,
    photo: FontAwesomeIcons.faPhotoVideo,
    settings: FontAwesomeIcons.faCogs,
    userEdit: FontAwesomeIcons.faUserEdit,
    caretUp: FontAwesomeIcons.faCaretUp,
    caretDown: FontAwesomeIcons.faCaretDown,
    trash: FontAwesomeIcons.faTrashAlt,
    clone: FontAwesomeIcons.faClone,
    closeBox: FontAwesomeIcons.faWindowClose,
};

export default ({ icon, size }) => <FontAwesomeIcon icon={icon} size={size} />;
