import React from 'react';
import { storiesOf } from '@storybook/react';
import GalleryComponent from '../components/Albums/GalleryComponent';

storiesOf('Albums', module)
    .add('GalleryComponent', () => <GalleryComponent />);
