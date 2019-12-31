import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import registrationReducer from './RegistrationReducer';
import loginReducer from './LoginReducer';

const createRootReducer = (history) => combineReducers({
    router: connectRouter(history),
    registrationState: registrationReducer,
    loginState: loginReducer
})

export default createRootReducer;
