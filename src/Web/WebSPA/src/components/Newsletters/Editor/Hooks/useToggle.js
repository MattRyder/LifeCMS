import { useState } from 'react';

export default function useToggle(defaultState = false) {
    const [isOpen, setIsOpen] = useState(defaultState);

    const toggle = () => setIsOpen(!isOpen);

    return [
        isOpen,
        toggle,
    ];
}
