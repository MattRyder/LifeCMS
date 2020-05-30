import React from 'react'
import { storiesOf } from "@storybook/react";
import BasicInfoComponent, { ActionMenuItem } from "../components/Profile/BasicInfoComponent";
import { createUser } from './factories/factories';

storiesOf('Profile', module)
    .add('BasicInfoComponent', () => <BasicInfoComponent
    userProfile={createUser()}
    actionMenuItems={[
        new ActionMenuItem("Connect", "#connect"),
        new ActionMenuItem("Edit Details", "#edit")
    ]}/>);