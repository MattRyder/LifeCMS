import PropTypes from 'prop-types';

export const PostStates = Object.freeze({
    DRAFTED: Symbol("drafted"),
    PUBLISHED: Symbol("published")
});

export default class Post {
    constructor(title, state, text, createdAt) {
        this.title = title;
        this.state = state;

        if(text instanceof Array) {
            this.text = text;
        } else if(typeof text === 'string') {
            this.text = text.split(/\r?\n/);
        } else {
            this.text = [];
        }

        this.createdAt = createdAt;
    }
}

Post.propTypes = {
    title: PropTypes.string,
    state: PropTypes.instanceOf(Symbol),
    text: PropTypes.arrayOf(PropTypes.string),
    createdAt: PropTypes.instanceOf(Date)
};