import PropTypes from 'proptypes';

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
    createdAt: PropTypes.instanceOf(Date)
};

export default Status;