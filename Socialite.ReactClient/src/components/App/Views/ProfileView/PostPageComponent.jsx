import React from 'react';
import PostListComponent from '../../../Posts/PostListComponent';
import CreatePostComponent from '../../../Posts/CreatePost/CreatePostComponent';

import './PostPageComponent.scss';

export default function PostPageComponent({
    accessToken,
    error,
    loading,
    posts,
}) {
    const sortedPosts = posts ? posts.sort((x, y) => x.createdAt < y.createdAt) : [];

    return (
        <div className="post-page-component">
            {accessToken ? <CreatePostComponent accessToken={accessToken} /> : null}

            <PostListComponent posts={sortedPosts} loading={loading} error={error} maxLines={2} />
        </div>
    )
};