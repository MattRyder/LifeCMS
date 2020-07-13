import React from 'react';
import { Table } from 'reactstrap';
import NewsletterListRowComponent from './NewsletterListRowComponent';

function NewsletterListComponent({ newsletters }) {
    return (
        <div className="newsletter-list-component">
            <Table>
                <caption className="a11y-visually-hidden">A list of newsletters</caption>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th label="actions" />
                    </tr>
                </thead>
                <tbody>
                    {
                        newsletters.length > 0
                            ? newsletters.map((newsletter) => (
                                <NewsletterListRowComponent newsletter={newsletter} />
                            )) : null
                    }
                </tbody>
            </Table>
        </div>
    );
}

NewsletterListComponent.defaultProps = {
    newsletters: [],
};

export default NewsletterListComponent;
