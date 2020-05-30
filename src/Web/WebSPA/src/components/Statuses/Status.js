import PropTypes from 'prop-types';

class Status {
    constructor(mood, text, createdAt) {
        this.mood = mood;
        this.text = text;
        this.createdAt = createdAt;
    }
}

Status.propTypes = {
    mood: PropTypes.string,
    text: PropTypes.string,
    createdAt: PropTypes.instanceOf(Date),
};

export default Status;
