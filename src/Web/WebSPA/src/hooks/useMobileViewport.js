import { useMediaQuery } from 'react-responsive';

export default function useMobileViewport() {
    return useMediaQuery({
        query: '(max-width: 1199px)',
    });
}
