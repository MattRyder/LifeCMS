import { createUserManager } from 'redux-oidc';
import Settings from './Settings';

export default createUserManager(Settings.json());
