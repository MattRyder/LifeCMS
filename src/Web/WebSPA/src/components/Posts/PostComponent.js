import React from 'react';
import strftime from 'strftime';
import Truncate from 'react-truncate';

import './PostComponent.scss';

export default function PostComponent({ post, maxLines }) {
    const getParagraphStringsArray = (text) => {
        if (text instanceof Array) {
            return text;
        }

        if (typeof text === 'string') {
            return text.split(/\r?\n/);
        }

        return [];
    };

    const renderPostText = (postText) => {
        const paragraphStrings = getParagraphStringsArray(postText);

        return (
            <Truncate lines={maxLines}>
                {
                    paragraphStrings.map((paragraph, i, array) => {
                        const line = <span key={i}>{paragraph}</span>;

                        return (i === array.length - 1) ? line : [line, <br key={`${i}br`} />];
                    })
                }
            </Truncate>
        );
    };

    return (
        <div className="post-component">
            <p className="created-at" title={post.createdAt}>
                {strftime('%d %B %Y', new Date(post.createdAt))}
            </p>
            <div className="text">
                {renderPostText(post.text)}
            </div>
        </div>
    );
}
