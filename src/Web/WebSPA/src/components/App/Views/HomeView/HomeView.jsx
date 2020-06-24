import React, { useEffect, useState } from 'react';
import { connect, useDispatch } from 'react-redux';
import PostPageComponent from './PostPageComponent';
import { fetchPosts } from '../../../../redux/actions/PostActions';
import MenuComponent from './MenuComponent';
import decodeToken from '../../../../openid/Token';
import Icon, { Icons } from '../../Iconography/Icon';
import { useUser, useTranslations } from '../../../../hooks';

import './HomeView.scss';

const mapStateToProps = (state) => {
    const token = state.oidc.user && decodeToken(state.oidc.user.access_token);

    const userId = token && token.sub;

    return ({
        userPostState: state.post[userId],
    });
};

const getMenuItems = (t, TextTranslationKeys) => [
    {
        text: t(TextTranslationKeys.common.domain.newsFeed),
        icon: <Icon icon={Icons.newspaper} />,
        path: '/news-feed',
    },
    {
        text: t(TextTranslationKeys.common.domain.messages),
        icon: <Icon icon={Icons.message} />,
        path: '/messages',
    },
    {
        text: t(TextTranslationKeys.common.domain.watch),
        icon: <Icon icon={Icons.film} />,
        path: '/photos',
    },
];

const HomeView = ({ userPostState = [] }) => {
    const dispatch = useDispatch();

    const { userId, accessToken } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isPostsLoading, setPostsLoading] = useState(false);

    useEffect(() => {
        if (!isPostsLoading && userId) {
            dispatch(fetchPosts(accessToken, userId));

            setPostsLoading(true);
        }
    }, [isPostsLoading, dispatch, userId, accessToken]);

    return (
        <div className="home-view">
            <MenuComponent menuItems={getMenuItems(t, TextTranslationKeys)} />

            <PostPageComponent
                accessToken={accessToken}
                posts={userPostState.posts}
                loading={userPostState.loading}
                error={null}
            />
        </div>
    );
};

export default connect(mapStateToProps, null)(HomeView);
