import { useSelector } from 'react-redux';

export default function useStateSelector(userId, reducer, resource, resourceId) {
    return useSelector((state) => state[reducer]
        && state[reducer][userId]
        && state[reducer][userId][resource]
        && state[reducer][userId][resource].find((n) => n.id === resourceId));
}
