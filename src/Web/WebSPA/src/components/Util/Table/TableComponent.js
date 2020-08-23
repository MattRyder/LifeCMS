import React from 'react';
import { Table } from 'reactstrap';
import { useTranslations } from '../../../hooks';

import './TableComponent.scss';

function TableComponent({ headings, rowComponent: RowComponent, collection }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className="table-component">
            <Table responsive>
                <caption className="a11y-visually-hidden">
                    {t(TextTranslationKeys.newsletterView.listCaption)}
                </caption>
                <thead>
                    <tr>
                        {
                            headings.map((h) => <th key={h}>{h}</th>)
                        }
                        <th label="actions" />
                    </tr>
                </thead>
                <tbody>
                    {
                        collection.length > 0
                            ? collection.map((item) => <RowComponent key={item.id} item={item} />)
                            : null
                    }
                </tbody>
            </Table>
        </div>
    );
}

TableComponent.defaultProps = {
    collection: [],
};

export default TableComponent;
