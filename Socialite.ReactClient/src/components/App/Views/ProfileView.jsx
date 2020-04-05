import React from 'react';
import { connect } from 'react-redux';
import { Route } from 'react-router';
import PropTypes from 'prop-types';
import { Container, Row, Col } from 'reactstrap';
import { withTranslation } from 'react-i18next';
import BasicInfoComponent, { ActionMenuItem, UserProfile } from '../../Profile/BasicInfoComponent';
import TextTranslationKeys from '../../../i18n/TextTranslationKeys';
import Status from '../../Statuses/Status';
import StatusContainer from '../../Statuses/StatusContainer';
import PostListComponent from '../../Posts/PostListComponent';
import PostDetailComponent from '../../Posts/PostDetailComponent';
import MenuComponent from '../Components/MenuComponent';
import { fetchStatuses } from '../../../redux/actions/StatusActions';
import { fetchPosts } from '../../../redux/actions/PostActions';
import Post from '../../Posts/Post';
import StatusPageComponent from './ProfileView/StatusPageComponent';

const mapStateToProps = (state) => ({
    user: state.oidc.user,
    userProfile: state.profile.user.userProfile,
    status: state.profile.status,
    post: state.profile.post,
});

const mapDispatchToProps = (dispatch) => ({
    dispatchFetchStatuses: (accessToken) => dispatch(fetchStatuses(accessToken)),
    dispatchFetchPosts: (accessToken) => dispatch(fetchPosts(accessToken)),
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
        const { dispatchFetchPosts, dispatchFetchStatuses, user } = this.props;

        const { dataLoaded } = this.state;

        if (!dataLoaded && user) {
            dispatchFetchPosts(user.access_token);

            dispatchFetchStatuses(user.access_token);

            this.setState({ dataLoaded: true });
        }
    }

    render() {
        const {
            match: { url },
            userProfile,
            status,
            post,
            selectedPost,
            t,
        } = this.props;

        return (
            <div className="profile-view">
                <div className="basic-info-row">
                    <BasicInfoComponent
                        userProfile={userProfile}
                        actionMenuItems={ProfileViewComponent.getActionMenuItems(t, url)}
                    />
                </div>
                <Container fluid>
                    <Row>
                        <Col sm="3">
                            <MenuComponent menuItems={ProfileViewComponent.getSideMenuItems(t, url)} />
                        </Col>
                        <Col sm="9">
                            <Route
                                path={`${url}/statuses`}
                                render={() => <StatusPageComponent status={status} />}
                            />
                            <Route
                                path={`${url}/posts`}
                                render={() => (
                                    <PostListComponent
                                        posts={post.posts}
                                        loading={post.loading}
                                        error={post.error}
                                    />
                                )}
                            />
                            <Route
                                path={`${url}/posts/:postId`}
                                render={() => (
                                    <PostDetailComponent post={selectedPost} />
                                )}
                            />
                        </Col>
                    </Row>
                </Container>
            </div>
        );
    }
}

ProfileViewComponent.propTypes = {
    userProfile: PropTypes.instanceOf(UserProfile),
    actionMenuItems: PropTypes.arrayOf(PropTypes.instanceOf(ActionMenuItem)),
    statuses: PropTypes.arrayOf(PropTypes.instanceOf(Status)),
    posts: PropTypes.arrayOf(PropTypes.instanceOf(Post)),
};

const TranslatedComponent = withTranslation()(ProfileViewComponent);

const ProfileView = connect(mapStateToProps, mapDispatchToProps)(TranslatedComponent);

export default ProfileView;
