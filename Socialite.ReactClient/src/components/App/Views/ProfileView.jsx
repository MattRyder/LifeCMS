import React from 'react';
import { connect } from 'react-redux';
import { Route } from 'react-router';
import PropTypes from 'prop-types';
import { Container, Row, Col } from 'reactstrap';
import { withTranslation } from 'react-i18next';
import BasicInfoComponent, { ActionMenuItem, UserProfile } from '../../Profile/BasicInfoComponent';
import TextTranslationKeys from '../../../i18n/TextTranslationKeys';
import Status from '../../Statuses/Status';
import { fetchStatuses } from '../../../redux/actions/StatusActions';
import { fetchPosts } from '../../../redux/actions/PostActions';
import StatusPageComponent from './ProfileView/StatusPageComponent';
// import PostPageComponent from './ProfileView/PostPageComponent';

const mapStateToProps = (state, { match: { params: { id: userId } } }) => ({
    user: state.oidc.user,
    userStatusState: state.status[userId],
    userPostState: state.post[userId],
});

const mapDispatchToProps = (dispatch) => ({
    dispatchFetchStatuses: (accessToken, userId) => dispatch(fetchStatuses(accessToken, userId)),
    dispatchFetchPosts: (accessToken, userId) => dispatch(fetchPosts(accessToken, userId)),
});

class ProfileViewComponent extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            dataLoaded: false,
        };
    }

    componentDidMount() {
        this.loadData();
    }

    componentDidUpdate() {
        this.loadData();
    }

    static getActionMenuItems(t, path) {
        return [
            new ActionMenuItem(t(TextTranslationKeys.profileView.connect), `${path}/connect`),
            new ActionMenuItem(t(TextTranslationKeys.profileView.editDetails), `${path}/edit`),
        ];
    }

    static getSideMenuItems(t, path) {
        return [
            new ActionMenuItem(t(TextTranslationKeys.profileView.menuItemStatuses), `${path}/statuses`),
            new ActionMenuItem(t(TextTranslationKeys.profileView.menuItemPosts), `${path}/posts`),
            new ActionMenuItem(t(TextTranslationKeys.profileView.menuItemPhotos), `${path}/photos`),
        ];
    }

    loadData() {
        const { dispatchFetchPosts, dispatchFetchStatuses, user, match: { params: { id } } } = this.props;

        const { dataLoaded } = this.state;

        if (!dataLoaded && user) {
            dispatchFetchPosts(user.access_token, id);

            dispatchFetchStatuses(user.access_token, id);

            this.setState({ dataLoaded: true });
        }
    }

    render() {
        const {
            match: { url },
            userStatusState = { statuses: [] },
            userPostState = { posts: [] },
            t,
            user,
        } = this.props;

        const { access_token: accessToken } = user || {};

        return (
            <div className="profile-view">
                <div className="basic-info-row">
                    <BasicInfoComponent
                        userProfile={{}}
                        actionMenuItems={ProfileViewComponent.getActionMenuItems(t, url)}
                    />
                </div>
                <Container fluid>
                    <Route
                        path={`${url}/statuses`}
                        render={() => (
                            <StatusPageComponent
                                accessToken={accessToken}
                                statuses={userStatusState.statuses}
                                loading={userStatusState.loading}
                                error={null}
                            />
                        )}
                    />
                    {/* <Route
                        path={`${url}/posts`}
                        render={() => (
                            <PostPageComponent
                                accessToken={accessToken}
                                posts={userPostState.posts}
                                loading={userPostState.loading}
                                error={null}
                            />
                        )}
                    /> */}
                    {/* <Route
                        path={`${url}/posts/:postId`}
                        render={() => (
                            <PostDetailComponent post={selectedPost} />
                        )}
                    /> */}
                </Container>
            </div>
        );
    }
}

const TranslatedComponent = withTranslation()(ProfileViewComponent);

const ProfileView = connect(mapStateToProps, mapDispatchToProps)(TranslatedComponent);

export default ProfileView;
