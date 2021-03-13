import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';
import { useTranslations } from 'hooks';
import React from 'react';
import { cx, css } from 'emotion';
import {
    Card, CardHeader,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import theme from 'theme';
import { ReactComponent as BlankPageIcon } from './BlankPageIcon.svg';
import { ReactComponent as MarketingStarterIcon } from './MarketingStarterIcon.svg';

const TemplateContainer = ({ title, children }) => {
    const styles = {
        containerBody: css`
            display: flex;
            padding: 1rem;
            flex-wrap: wrap;
            justify-content: space-evenly;

            @media(min-width: 1200px) {
                justify-content: flex-start;
            }
        `,
    };

    return (
        <Card>
            <CardHeader>{title}</CardHeader>
            <div className={cx(styles.containerBody)}>
                {children}
            </div>
        </Card>
    );
};

const TemplateCard = ({
    imageComponent: ImageComponent, title, description, designSourceSlug,
}) => {
    const styles = {
        card: css`
            border: none;
            width: 14rem;
            max-width: 14rem;
            margin: 0.75rem;

            @media(min-width: 480px) and (max-width: 993px) {
                max-width: 17.5rem;
            }
        `,
        image: css`
            display: flex;
            width: 100%;
            height: auto;
            border: 1px solid rgba(0, 0, 0, 0.1);
            border-radius: 3px;
            justify-content: center;
            padding: 1rem;
        `,
        body: css`
            text-align: center;
            padding: 0.5rem;
            color: ${theme.colors.mainLink}
        `,
        title: css`
            font-weight: bold;
            font-size: 0.85rem;
        `,
        text: css`
            font-size: 0.75rem;
        `,
    };

    return (
        <Card className={cx(styles.card)}>
            <Link to={`/templates/from/${designSourceSlug}`}>
                <div className={cx(styles.image)}>
                    <ImageComponent />
                </div>
                <div className={cx(styles.body)}>
                    <header className={cx(styles.title)}>{title}</header>
                    <p className={cx(styles.text)}>{description}</p>
                </div>
            </Link>
        </Card>
    );
};

export default function TemplatesSelector() {
    const { t, TextTranslationKeys } = useTranslations();

    const TemplateTextTransKeys = TextTranslationKeys.newsletterView.templateSelector.templates;

    const templates = [
        {
            title: t(TemplateTextTransKeys.blank.title),
            description: t(TemplateTextTransKeys.blank.description),
            imageComponent: () => <BlankPageIcon />,
            designSourceSlug: 'blank',
        },
        {
            title: t(TemplateTextTransKeys.marketingStarter.title),
            description: t(TemplateTextTransKeys.marketingStarter.description),
            imageComponent: () => <MarketingStarterIcon />,
            designSourceSlug: 'marketing-starter',
        },
    ];

    const styles = {
        wrapper: css`
            display: flex;
            flex-direction: column;
            flex: 1;
        `,
        pageTitle: css`
            font-size: 1.5rem;
            line-height: 3rem;
        `,
        content: css`
            padding: 0 2rem;
        `,
    };

    return (
        <div className={cx(styles.wrapper)}>
            <ViewNavigationBar />
            <div className={cx(styles.content)}>
                <div>
                    <h1 className={cx(styles.pageTitle)}>
                        {t(TextTranslationKeys.newsletterView.templateSelector.pageTitle)}
                    </h1>
                    <p>
                        {t(TextTranslationKeys.newsletterView.templateSelector.pageDescription)}
                    </p>
                </div>

                <TemplateContainer>
                    { templates && templates.map((template) => (
                        <TemplateCard
                            key={template.title}
                            description={template.description}
                            imageComponent={template.imageComponent}
                            title={template.title}
                            designSourceSlug={template.designSourceSlug}
                        />
                    ))}
                </TemplateContainer>
            </div>
        </div>
    );
}
