import { useUser } from 'hooks';
import { useEffect, useState } from 'react';
import FileUrnService from 'services/FileUrnService';

const InitialState = {
    uri: null,
    urn: null,
    newFile: {
        url: null,
    },
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

    const setNewFile = (newFile) => setState({
        ...state,
        newFile,
    });

    return [
        state,
        setNewFile,
    ];
}
