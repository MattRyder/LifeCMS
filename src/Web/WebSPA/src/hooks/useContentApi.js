import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';

export default function (fetchFunc, accessToken) {
    const dispatch = useDispatch();

    const [isLoaded, setIsLoaded] = useState(false);

    useEffect(() => {
        if (!isLoaded && accessToken) {
            dispatch(fetchFunc());
            setIsLoaded(true);
        }
    }, [dispatch, fetchFunc, isLoaded, accessToken]);
}
