import React from 'react';
import { useSelector } from 'react-redux';
import { useUser, useContentApi, useTranslations } from '../../../../hooks';
import { fetchPosts } from '../../../../redux/actions/PostActions';
import PageTitleBar from '../../Components/PageTitleBar/PageTitleBar';
import PostPageComponent from './PostPageComponent';

import './HomeView.scss';

export default function HomeView() {
    const { userId, accessToken } = useUser();

    const userPostState = useSelector((state) => state.post[userId] || []);

    const { t, TextTranslationKeys } = useTranslations();

    useContentApi(
        () => fetchPosts(accessToken, userId),
        accessToken,
    );

    return (
        <div className="home-view">
            <PageTitleBar>
                <span>{t(TextTranslationKeys.homeView.pageTitle)}</span>
            </PageTitleBar>
            <PostPageComponent
                accessToken={accessToken}
                posts={userPostState.posts}
                loading={userPostState.loading}
                error={userPostState.error}
            />
        </div>
    );
}
