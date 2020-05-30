import React from 'react';
import { storiesOf } from '@storybook/react';

import CreateStatusComponent from '../components/Statuses/CreateStatus/CreateStatusComponent';
import EmojiDropdownComponent from '../components/Statuses/CreateStatus/EmojiDropdownComponent';
import StatusComponent from '../components/Statuses/StatusComponent';
import StatusContainer from '../components/Statuses/StatusContainer';
import { createStatus } from './factories/factories';

const createStatusArray = () => [...Array(5).keys()].map(createStatus);

storiesOf('Statuses', module)
    .add('StatusComponent', () => <StatusComponent status={createStatus()} />)
    .add('StatusContainer/None', () => <StatusContainer statuses={[]} />)
    .add('StatusContainer/Some', () => <StatusContainer statuses={createStatusArray()} />)
    .add('CreateStatusComponent', () => <CreateStatusComponent />);

storiesOf('EmojiDropdown', module)
    .add('EmojiDropdown', () => <EmojiDropdownComponent />);
    // .add('StatusForm', () => <StatusForm />);