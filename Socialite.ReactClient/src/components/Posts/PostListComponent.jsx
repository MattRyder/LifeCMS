import React from 'react';
import {Link} from 'react-router-dom';
import strftime from 'strftime';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFolderOpen } from '@fortawesome/free-solid-svg-icons';
import CenteredMessageComponent from '../App/Components/CenteredMessageComponent';

import './PostListComponent.scss';

export default class PostListContainer extends React.Component {
    render() {
        const renderNoPostsAvailable = () => (
            <CenteredMessageComponent
                message={"No posts are available that have been authored by this person."}
                icon={<FontAwesomeIcon icon={faFolderOpen} />}
            />
        );

        const renderPosts = () => (
            this.props.posts.map((post) => {
                return (<div className="post">
                    <p className="created-at" title={post.createdAt.toString()}>
                        {strftime("%d %B %Y", post.createdAt)}
                    </p>
                    <Link className="title" to={"/profile/post/"+post.title}>
                        {post.title}
                    </Link>
                </div>);
            })
        );

        return (
            <div className="post-list">
                {this.props.posts.length > 0 ? renderPosts() : renderNoPostsAvailable()}
            </div>
        );
    }
}