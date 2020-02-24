export default {
    authority: 'http://localhost:5000',
    client_id: 'SocialiteWebApiClient',
    redirect_uri: 'http://localhost:3000/session/oauth_callback',
    post_logout_redirect_uri: 'http://localhost:3000',
    response_type: 'code',
    scope: 'status:read',
};
