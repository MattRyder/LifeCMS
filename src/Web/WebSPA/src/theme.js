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

export const boxShadow = (color) => css`
    box-shadow: 0 1px 2px ${color},
                0 2px 4px ${color},
                0 4px 8px ${color},
                0 8px 16px ${color};
`;

const theme = () => ({

});

export default theme;
