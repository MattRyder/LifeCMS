import React from 'react';
import PropTypes from 'prop-types';
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
    eyedropper: FontAwesomeIcons.faEyeDropper,
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
    sync: FontAwesomeIcons.faSync,
    userEdit: FontAwesomeIcons.faUserEdit,
    underline: FontAwesomeIcons.faUnderline,
    caretUp: FontAwesomeIcons.faCaretUp,
    caretDown: FontAwesomeIcons.faCaretDown,
    trash: FontAwesomeIcons.faTrashAlt,
    clone: FontAwesomeIcons.faClone,
    closeBox: FontAwesomeIcons.faWindowClose,
};

export default function Icon({ icon, size }) {
    return <FontAwesomeIcon icon={icon} size={size} />;
}

Icon.propTypes = {
    icon: PropTypes.oneOf(Object.values(Icons)).isRequired,
    size: PropTypes.string,
};

Icon.defaultProps = {
    size: '1x',
};
