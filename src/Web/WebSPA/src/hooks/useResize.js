import { useEffect, useState } from 'react';

export default function useResize() {
    const [width, setWidth] = useState(window.innerWidth);

    const [height, setHeight] = useState(window.innerHeight);

    useEffect(() => {
        function handleResize() {
            setWidth(window.innerWidth);

            setHeight(window.innerHeight);
        }

        handleResize();

        window.addEventListener('resize', handleResize);

        return () => {
            window.removeEventListener('resize', handleResize);
        };
    });

    return {
        width,
        height,
    };
}
