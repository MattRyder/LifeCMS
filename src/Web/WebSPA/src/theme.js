import { css } from 'emotion';

export const accessibility = {
    visuallyHidden: css`
        :not(:focus):not(:active) {
            position: absolute !important;
            height: 1px; 
            width: 1px;
            overflow: hidden;
            clip: rect(1px, 1px, 1px, 1px);
            white-space: nowrap;
        }
    `,
};

const theme = () => ({

});

export default theme;
