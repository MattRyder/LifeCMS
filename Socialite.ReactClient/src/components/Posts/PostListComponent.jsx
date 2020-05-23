import React from 'react';
import Icon, { Icons } from '../App/Iconography/Icon';
import { useTranslation } from 'react-i18next';
import TextTranslationKeys from '../../i18n/TextTranslationKeys';
import CenteredMessageComponent from '../App/Components/CenteredMessageComponent';
import PostComponent from './PostComponent';

import './PostListComponent.scss';

export default function PostListComponent({ posts, maxLines }) {
    const { t } = useTranslation();

    const renderNoPostsAvailable = () => (
        <div className="none-available">
            <CenteredMessageComponent
                message={t(TextTranslationKeys.post.noPostsAvailable)}
                icon={<Icon icon={Icons.folder} />}
            />
        </div>
    );

    const renderPosts = () => (
        posts.map((post) => <PostComponent post={post} key={post.id} maxLines={maxLines} />)
    );

    return (
        <div className="post-list">
            {posts.length > 0 ? renderPosts() : renderNoPostsAvailable()}
        </div>
    );
}
