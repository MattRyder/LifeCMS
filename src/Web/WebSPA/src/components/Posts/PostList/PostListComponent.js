import React from 'react';
import { useTranslations } from '../../../hooks';
import Icon, { Icons } from '../../App/Iconography/Icon';
import CenteredMessageComponent from '../../App/Components/CenteredMessageComponent';
import PostComponent from '../PostComponent';

import './PostListComponent.scss';

function PostListComponent({ posts, maxLines }) {
    const { t, TextTranslationKeys } = useTranslations();

    const renderNoneAvailable = () => (
        <div className="none-available">
            <CenteredMessageComponent
                message={t(TextTranslationKeys.post.noPostsAvailable)}
                icon={<Icon icon={Icons.folder} />}
            />
        </div>
    );

    const renderPostsAvailable = () => (
        posts.map((post) => <PostComponent post={post} key={post.id} maxLines={maxLines} />)
    );

    return (
        <div className="post-list">
            {posts.length > 0 ? renderPostsAvailable() : renderNoneAvailable()}
        </div>
    );
}

PostListComponent.defaultProps = {
    posts: [],
    maxLines: 5,
};

export default PostListComponent;
