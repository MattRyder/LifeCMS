import { UserProfile } from "../../components/Profile/BasicInfoComponent";


const InitialState = {
    userProfile: new UserProfile(),
    statuses: [],
    posts: [],
};

const UserReducer = (state = InitialState, action) => {
    switch(action.type) {
        default:
            return state;
    }
};

export default UserReducer;