import React from 'react';
import { storiesOf } from '@storybook/react';
import GalleryComponent from '../components/Albums/GalleryComponent';

const faker = require('faker');

storiesOf("Albums", module)
    .add('GalleryComponent', () => <GalleryComponent />);