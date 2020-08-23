import React from 'react';
import { useSelector } from 'react-redux';
import { useUser, useContentApi } from '../../../../hooks';
import { fetchPosts } from '../../../../redux/actions/PostActions';
import PostPageComponent from './PostPageComponent';

import './HomeView.scss';

export default function HomeView() {
    const { userId, accessToken } = useUser();

    const userPostState = useSelector((state) => state.post[userId] || []);

    useContentApi(
        () => fetchPosts(accessToken, userId),
        accessToken,
    );

    return (
        <div className="home-view">
            <PostPageComponent
                accessToken={accessToken}
                posts={userPostState.posts}
                loading={userPostState.loading}
                error={userPostState.error}
            />
        </div>
    );
}
