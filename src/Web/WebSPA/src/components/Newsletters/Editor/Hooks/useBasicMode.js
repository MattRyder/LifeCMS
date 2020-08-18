import { useState } from 'react';

export default function useBasicMode(value = true) {
    const [isBasicMode, setIsBasicMode] = useState(value);

    const toggleBasicMode = () => setIsBasicMode(!isBasicMode);

    return [
        isBasicMode,
        toggleBasicMode,
    ];
}
