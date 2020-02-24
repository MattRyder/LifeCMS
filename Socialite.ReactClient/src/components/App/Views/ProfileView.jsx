import React from 'react';
import { connect } from 'react-redux';
import { Route } from 'react-router';
import PropTypes from 'proptypes';
import { Container, Row, Col } from 'reactstrap';
import BasicInfoComponent, { ActionMenuItem, UserProfile } from '../../Profile/BasicInfoComponent';
import Status from '../../Statuses/Status';
import StatusContainer from '../../Statuses/StatusContainer';
import PostListComponent from '../../Posts/PostListComponent';
import PostDetailComponent from '../../Posts/PostDetailComponent';
import MenuComponent from '../Components/MenuComponent';
import { fetchStatuses } from '../../../redux/actions/StatusActions';
import { fetchPosts } from '../../../redux/actions/PostActions';
import Post from '../../Posts/Post';

const mapStateToProps = (state) => ({
    userProfile: state.profile.user.userProfile,
    status: state.profile.status,
    post: state.profile.post,
});

const mapDispatchToProps = (dispatch) => ({
    dispatchFetchStatuses: () => dispatch(fetchStatuses()),
    dispatchFetchPosts: () => dispatch(fetchPosts()),
});

class ProfileViewComponent extends React.Component {
    componentDidMount() {
        const { dispatchFetchPosts, dispatchFetchStatuses } = this.props;

        dispatchFetchPosts();
        dispatchFetchStatuses();
    }

    static getActionMenuItems(path) {
        return [
            new ActionMenuItem('Connect', `${path}/connect`),
            new ActionMenuItem('Edit Details', `${path}/edit`),
        ];
    }

    static getSideMenuItems(path) {
        return [
            new ActionMenuItem('Statuses', `${path}/statuses`),
            new ActionMenuItem('Posts', `${path}/posts`),
            new ActionMenuItem('Photos', `${path}/photos`),
        ];
    }

    render() {
        const {
            match: { url },
            userProfile,
            status,
            post,
            selectedPost,
        } = this.props;

        return (
            <div className="profile-view">
                <div className="basic-info-row">
                    <BasicInfoComponent
                        userProfile={userProfile}
                        actionMenuItems={ProfileViewComponent.getActionMenuItems(url)}
                    />
                </div>
                <Container fluid>
                    <Row>
                        <Col sm="3">
                            <MenuComponent menuItems={ProfileViewComponent.getSideMenuItems(url)} />
                        </Col>
                        <Col sm="9">
                            <Route
                                path={`${url}/statuses`}
                                render={() => (
                                    <StatusContainer
                                        statuses={status.statuses}
                                        loading={status.loading}
                                        error={status.error}
                                    />
                                )}
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

const ProfileView = connect(mapStateToProps, mapDispatchToProps)(ProfileViewComponent);

export default ProfileView;
