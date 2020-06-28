import React from 'react';

export default function ({ name, occupation, location }) {
    return (
        <div className="user-info-component">
            <p className="name">{name}</p>

            <div className="what-where">
                <div>
                    <span>{occupation}</span>
                </div>
                <div>
                    <span>&bull;</span>
                </div>
                <div>
                    <span>{location}</span>
                </div>
            </div>
        </div>
    );
}
