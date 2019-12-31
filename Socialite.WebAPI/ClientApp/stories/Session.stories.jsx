import React from 'react';
import { storiesOf } from '@storybook/react';
import LoginForm from '../components/Session/LoginForm';

storiesOf('Session', module)
    .add('LoginForm', () => <LoginForm />);
