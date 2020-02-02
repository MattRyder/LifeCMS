import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import registrationReducer from './RegistrationReducer';
import authenticationReducer from './AuthenticationReducer';

const createRootReducer = (history) => combineReducers({
    router: connectRouter(history),
    registrationState: registrationReducer,
    authenticationState: authenticationReducer
})

export default createRootReducer;
