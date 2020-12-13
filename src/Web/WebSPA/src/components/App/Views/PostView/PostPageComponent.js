import React from 'react';
import PostListComponent from '../../../Posts/PostList/PostListComponent';
import CreatePostComponent from '../../../Posts/CreatePost/CreatePostComponent';

import './PostPageComponent.scss';

function PostPageComponent({
    accessToken,
    error,
    loading,
    posts,
}) {
    const sortedPosts = posts ? posts.sort((x, y) => x.createdAt < y.createdAt) : [];

    return (
        <div className="post-page-component">
            {accessToken ? <CreatePostComponent accessToken={accessToken} /> : null}

            <PostListComponent posts={sortedPosts} loading={loading} error={error} maxLines={4} />
        </div>
    );
}

PostPageComponent.defaultProps = {
    accessToken: '',
    error: null,
    loading: false,
    posts: [],
};

export default PostPageComponent;
