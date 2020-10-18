import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as FontAwesomeIcons from '@fortawesome/free-solid-svg-icons';

export const Icons = {
    attentionTriangle: FontAwesomeIcons.faExclamationTriangle,
    bullhorn: FontAwesomeIcons.faBullhorn,
    bold: FontAwesomeIcons.faBold,
    chat: FontAwesomeIcons.faComment,
    check: FontAwesomeIcons.faCheckCircle,
    cog: FontAwesomeIcons.faCog,
    ellipsisHorizontal: FontAwesomeIcons.faEllipsisH,
    folder: FontAwesomeIcons.faFolderOpen,
    italic: FontAwesomeIcons.faItalic,
    home: FontAwesomeIcons.faHome,
    notification: FontAwesomeIcons.faBell,
    newspaper: FontAwesomeIcons.faNewspaper,
    message: FontAwesomeIcons.faEnvelope,
    film: FontAwesomeIcons.faFilm,
    photo: FontAwesomeIcons.faPhotoVideo,
    plus: FontAwesomeIcons.faPlus,
    settings: FontAwesomeIcons.faCogs,
    userEdit: FontAwesomeIcons.faUserEdit,
    underline: FontAwesomeIcons.faUnderline,
    caretUp: FontAwesomeIcons.faCaretUp,
    caretDown: FontAwesomeIcons.faCaretDown,
    trash: FontAwesomeIcons.faTrashAlt,
    clone: FontAwesomeIcons.faClone,
    closeBox: FontAwesomeIcons.faWindowClose,
};

export default ({ icon, size }) => <FontAwesomeIcon icon={icon} size={size} />;
