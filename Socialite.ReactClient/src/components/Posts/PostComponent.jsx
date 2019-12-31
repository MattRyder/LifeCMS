import React from 'react';
import PropTypes from 'proptypes';
import strftime from 'strftime';

import './PostComponent.scss';

export default class PostComponent extends React.Component {
    render() {
        return (
            <div className="post ">
                <p className="created-at" title={this.props.post.createdAt.toString()}>
                    {strftime("%d %B %Y", this.props.post.createdAt)}
                </p>
                <h2 className="title">{this.props.post.title}</h2>
                <div className="text">
                    {this.props.post.text.map((para, _) => <p>{para}</p>)}
                </div>
            </div>
        )
    }
}