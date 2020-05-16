import { createUserManager } from 'redux-oidc';
import RuntimeConfiguration from './RuntimeConfiguration';

export default createUserManager(RuntimeConfiguration.json());
