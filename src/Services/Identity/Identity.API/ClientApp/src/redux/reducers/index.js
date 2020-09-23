import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import registrationReducer from './RegistrationReducer';
import errorReducer from './ErrorReducer';
import authenticationReducer from './AuthenticationReducer';
import passwordReducer from './PasswordReducer';

const createRootReducer = (history) => combineReducers({
    router: connectRouter(history),
    registrationState: registrationReducer,
    authenticationState: authenticationReducer,
    passwordState: passwordReducer,
    errorState: errorReducer,
});

export default createRootReducer;
