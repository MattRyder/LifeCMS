import PropTypes from 'prop-types';
import React, { useState } from 'react';
import {
    Dropdown, DropdownToggle, DropdownMenu,
} from 'reactstrap';
import { Picker } from 'emoji-mart';
import 'emoji-mart/css/emoji-mart.css';

const EmojiDropdownComponent = ({ onEmojiClick }) => {
    const [dropdownOpen, setDropdownOpen] = useState(false);

    const [selectedEmoji, setSelectedEmoji] = useState('😀');

    const toggle = () => setDropdownOpen((prevState) => !prevState);

    const handleEmojiClick = (emoji) => {
        toggle();

        setSelectedEmoji(emoji.native);

        onEmojiClick(emoji.native);
    };

    return (
        <div className="emoji-dropdown-component">
            <Dropdown isOpen={dropdownOpen} toggle={toggle}>
                <DropdownToggle caret>
                    {selectedEmoji}
                </DropdownToggle>
                <DropdownMenu>
                    <Picker onSelect={handleEmojiClick} title="" />
                </DropdownMenu>
            </Dropdown>
        </div>
    );
};


EmojiDropdownComponent.propTypes = {
    onEmojiClick: PropTypes.func.isRequired,
};

export default EmojiDropdownComponent;
