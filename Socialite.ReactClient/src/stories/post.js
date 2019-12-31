import React from 'react';

import PostComponent from '../components/Posts/PostComponent';
import PostContainer from '../components/Posts/PostContainer';
import { storiesOf } from '@storybook/react';
import { createPost } from './factories/factories';

storiesOf("Posts", module)
    .add("PostComponent", () => <PostComponent {...createPost()} />)
    .add("PostContainer/None", () => <PostContainer posts={[]} />)
    .add("PostContainer/Some", () => <PostContainer posts={
        [<PostComponent {...createPost()} />,
        <PostComponent {...createPost()} />,
        <PostComponent {...createPost()} />,]
    } />);