import React from 'react';
// import { connect } from 'redux';
import { Link } from 'react-router-dom';
import PostComponent from './PostComponent';

export default function PostDetailComponent({ post }) {
    return (
        <div className="post-detail">
            <Link to="/profile/posts">Back to Posts</Link>

            <PostComponent post={post} />
        </div>
    );
}