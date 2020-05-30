import React from 'react';
import { storiesOf } from '@storybook/react';
import PostComponent from '../components/Posts/PostComponent';
import PostListComponent from '../components/Posts/PostListComponent';
import { createPost } from './factories/factories';

storiesOf('Posts', module)
    .add('PostComponent', () => <PostComponent post={createPost()} />)
    .add('PostListComponent/None', () => <PostListComponent posts={[]} />)
    .add('PostListComponent/Some', () => <PostListComponent posts={[createPost(), createPost(), createPost()]} />);
