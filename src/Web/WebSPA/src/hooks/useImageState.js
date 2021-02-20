import { useEffect, useState } from 'react';
import FileUrnService from 'services/FileUrnService';
import useUser from './useUser';

const InitialState = {
    uri: null,
    urn: null,
};

export default function useImageState({ urn }) {
    const [state, setState] = useState(InitialState);

    const { accessToken } = useUser();

    const [attempted, setAttempted] = useState(false);

    useEffect(() => {
        async function getUri() {
            const uri = await FileUrnService(accessToken, urn) || null;

            setState({
                ...state,
                uri,
            });
        }

        if (urn && !attempted) {
            getUri();

            setAttempted(true);
        }
    }, [accessToken, urn, state, attempted]);

    return state;
}
