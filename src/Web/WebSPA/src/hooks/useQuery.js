import { useLocation } from 'react-router';

export default function useQuery(paramKey) {
    const { search } = useLocation();

    const params = new URLSearchParams(search);

    return params.get(paramKey);
}
