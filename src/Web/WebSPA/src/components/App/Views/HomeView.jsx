import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import TextTranslationKeys from '../../../i18n/TextTranslationKeys';
import PostPageComponent from './HomeView/PostPageComponent';
import { fetchPosts } from '../../../redux/actions/PostActions';
import MenuComponent from './HomeView/MenuComponent';
import decodeToken from '../../../openid/Token';
import Icon, { Icons } from '../Iconography/Icon';
import './HomeView.scss';

const mapStateToProps = (state) => {
    const token = state.oidc.user && decodeToken(state.oidc.user.access_token);

    const userId = token && token.sub;

    return ({
        user: state.oidc.user,
        userStatusState: state.status[userId],
        userPostState: state.post[userId],
    });
};

const mapDispatchToProps = (dispatch) => ({
    dispatchFetchPosts: (accessToken, userId) => dispatch(fetchPosts(accessToken, userId)),
});

const HomeView = ({ user, userPostState = [], dispatchFetchPosts, t }) => {
    const menuItems = [
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

    const [isPostsLoading, setPostsLoading] = useState(false);

    useEffect(() => {
        if (!isPostsLoading && user) {
            const { sub: userId } = decodeToken(user.access_token);

            dispatchFetchPosts(user.access_token, userId);

            setPostsLoading(true);
        }
    }, [isPostsLoading, dispatchFetchPosts, user]);

    return (
        <div className="home-view">
            <MenuComponent menuItems={menuItems} />
            <PostPageComponent
                accessToken={user && user.access_token}
                posts={userPostState.posts}
                loading={userPostState.loading}
                error={null}
            />
        </div>
    );
};

export default connect(mapStateToProps, mapDispatchToProps)(withTranslation()(HomeView));
