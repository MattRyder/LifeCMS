import { css } from 'emotion';
import { rgba } from 'polished';

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
        modal: {
            background: '#d8f3fd',
            message: '#fffff4',
        },
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
        textPlaceholder: '#bababa',
        textMuted: '#a1a1a1',
        tableBorder: '#686e70',
        subscriberListHeaderBackground: '#fafafa',
    },
    components: {
        pageStyleForm: css`
            background-color: #fff;
            display: flex;
            flex-direction: column;
            justify-content: space-around;
            padding: 1rem;
            ${boxShadow(rgba(0, 0, 0, 0.05))}

            > div {
                padding: 0.5rem 0;
            }
        `,
        link: css`
            vertical-align: middle;
            font-weight: 400;
            color: #117cfa;
            text-decoration: none;
            &:hover {
                text-decoration: underline;
            }
        `,
    },
});

export default theme;
