import { css, cx } from 'emotion';
import { useToggle } from 'hooks';
import React from 'react';
import { Collapse } from 'reactstrap';
import Icon, { Icons } from '../../../../App/Iconography/Icon';

const styles = {
    headerTrigger: css`
        background-color: rgba(0, 0, 0, 0.11);
        border: 0;
        color: hsl(0, 0%, 22%);
        text-transform: uppercase;
        margin: 0;
        padding: 0.75rem 0.5rem;
        width: 100%;
    `,
    header: css`
        display: flex;
        font-size: 0.75rem;
        justify-content: space-between;
    `,
};

export default function AttributePanelContainer({ title, children }) {
    const [isOpen, toggleOpen] = useToggle(true);

    return (
        <div className={cx(styles.container)}>
            <div className={cx(styles.header)}>
                <button
                    type="button"
                    className={cx(styles.headerTrigger)}
                    onClick={toggleOpen}
                >
                    <span className={cx(styles.header)}>
                        {title}
                        <Icon icon={isOpen ? Icons.caretUp : Icons.caretDown} />
                    </span>
                </button>
            </div>

            <Collapse isOpen={isOpen}>
                {children}
            </Collapse>
        </div>
    );
}
