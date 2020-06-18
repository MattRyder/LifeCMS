import React, { useState, useEffect } from 'react';
import Icon, { Icons } from '../../App/Iconography/Icon';
import usePrevious from '../../../hooks/usePrevious';
import CenteredMessageComponent from '../../App/Components/CenteredMessageComponent';
import PostPlaceholder from './PostPlaceholder';
import PostComponent from '../PostComponent';

import './PostListComponent.scss';
import { useTranslations } from '../../../hooks';

export default function ({ posts, maxLines }) {
    const { t, TextTranslationKeys } = useTranslations();

    const [hasLoaded, setHasLoaded] = useState(false);

    const previousPosts = usePrevious({ posts });

    useEffect(() => {
        if (previousPosts !== posts && !hasLoaded) {
            setTimeout(() => setHasLoaded(true), 1250);
        }
    }, [hasLoaded, setHasLoaded, posts, previousPosts]);

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

    const renderLoading = () => [...Array(5)].map((_v, i) => <PostPlaceholder key={i} />);

    const renderPosts = () => (posts.length > 0 ? renderPostsAvailable() : renderNoneAvailable());

    return (
        <div className="post-list">
            {hasLoaded ? renderPosts() : renderLoading()}
        </div>
    );
}
