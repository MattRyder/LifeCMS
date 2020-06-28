import React, { useState } from 'react';

export default function ({ avatarImageUri }) {
    const [avatarExpanded, setAvatarExpanded] = useState(false);

    return (
        <div className="user-avatar-component">
            <div
                className={`avatar ${avatarExpanded ? 'expanded' : ''}`}
                style={{ backgroundImage: `url('${avatarImageUri}')` }}
                onClick={() => setAvatarExpanded(!avatarExpanded)}
                onKeyDown={(e) => (e.key === 'Enter' ? setAvatarExpanded(!avatarExpanded) : null)}
                role="button"
                tabIndex="0"
                aria-label="This person's avatar image"
            />
        </div>
    );
}
