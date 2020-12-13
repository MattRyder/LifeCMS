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

const theme = ({
    colors: {
        main: '#07729d',
        mainAccent: '#117cfa',
        mainLink: '#2f393d',
        mainBackground: '#f5f5f5',
        mainUnderBackground: '#bababa',
        mainMenuHeader: '#686e70',
        mainMenuItemActiveBackground: '#e2eaf3',
        editorSelected: '#117cfa',
        tableBackground: '#fff',
        tableHeaderBackground: '#e4e4e4',
    },
});

export default theme;
