import { useSelector } from 'react-redux';
import decodeToken from '../openid/Token';

export default function () {
    const user = useSelector((state) => state.oidc.user);

    if (!user) {
        return {};
    }

    const { sub } = decodeToken(user.access_token);

    return {
        userId: sub,
        accessToken: user.access_token,
    };
}
