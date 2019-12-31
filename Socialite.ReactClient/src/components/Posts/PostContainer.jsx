import React from 'react';
import CenteredMessageComponent from '../App/Components/CenteredMessageComponent';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFolderOpen } from '@fortawesome/free-solid-svg-icons';

import './PostContainer.scss';
import PostComponent from './PostComponent';

export default class PostContainer extends React.Component {
    render() {
        const renderNoPostsAvailable = () => (
            <CenteredMessageComponent
                message={"No posts are available that have been authored by this person."}
                icon={<FontAwesomeIcon icon={faFolderOpen} />}
            />
        );

        const renderPosts = () => {
            return this.props.posts.map((post, idx) => <PostComponent post={post} key={idx} />)
        };

        return (
            <div className="post-container">
                {this.props.posts.length > 0 ? renderPosts() : renderNoPostsAvailable()}
            </div>
        );
    }
}