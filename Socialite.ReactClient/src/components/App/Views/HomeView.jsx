import React from 'react';
import { connect } from 'react-redux';
import { Container } from 'reactstrap';
import { withTranslation } from 'react-i18next';
import TextTranslationKeys from '../../../i18n/TextTranslationKeys';
import PostPageComponent from './HomeView/PostPageComponent';

import './HomeView.scss';
import MenuComponent from './HomeView/MenuComponent';
import Icon, { Icons } from '../Iconography/Icon';

const mapStateToProps = (state, { match: { params: { id: userId } } }) => ({
    user: state.oidc.user,
    userStatusState: state.status[userId],
    userPostState: state.post[userId],
});


const HomeView = ({ user, userPostState = [], t }) => {
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

export default connect(mapStateToProps, null)(withTranslation()(HomeView));
