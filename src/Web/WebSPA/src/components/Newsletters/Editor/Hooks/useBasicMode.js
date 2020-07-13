import { useState } from 'react';

export default function useBasicMode() {
    const [isBasicMode, setIsBasicMode] = useState(true);

    const toggleBasicMode = () => setIsBasicMode(!isBasicMode);

    return [
        isBasicMode,
        toggleBasicMode,
    ];
}
