import { cx, css } from 'emotion';
import { darken, rgba } from 'polished';
import React from 'react';
import { Table as ReactstrapTable } from 'reactstrap';
import theme, { accessibility, boxShadow } from 'theme';

const styles = {
    table: css`
        ${boxShadow(rgba(0, 0, 0, 0.05))}
        background-color: ${theme.colors.tableBackground};
        td {
            vertical-align: middle !important;

            .dropdown .nav-link {
                float: right;
            }
        }
    `,
    thead: css`
        background-color: ${theme.colors.tableHeaderBackground};
        color: ${darken(0.4, theme.colors.tableHeaderBackground)};
        font-size: 0.85rem;

        tr th {
            border: none !important;
            text-transform: uppercase;
            font-size: smaller;
        }
    `,
    actionsLabel: css`
        display: flex;
        justify-content: right;

        span {
            padding-left: 0.5rem;
            padding-right: 0.5rem;
        }
    `,
};

export default function Table({
    headings,
    rowComponent: RowComponent,
    collection,
    accessibilityDescription,
}) {
    return (
        <ReactstrapTable responsive className={cx(styles.table)}>
            <caption className={cx(accessibility.visuallyHidden)}>
                {accessibilityDescription}
            </caption>
            <thead className={cx(styles.thead)}>
                <tr>
                    {headings.map((h) => <th key={h}>{h}</th>)}
                    <th className={cx(styles.actionsLabel)} label="actions" />
                </tr>
            </thead>
            <tbody>
                {
                    collection && collection.map((item) => (
                        <RowComponent key={item.id} item={item} />))
                }
            </tbody>
        </ReactstrapTable>
    );
}

Table.defaultProps = {
    collection: [],
};
