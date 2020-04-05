import { addDecorator } from '@storybook/react';
import { I18nProviderWrapper } from './wrappers/I18nProviderWrapper';
import ProviderWrapper from '../src/Provider';
import configureStore from '../src/redux/AppStore';

const store = configureStore();


addDecorator((storyFn) => (
    <div>
        <ProviderWrapper store={store}>
            <I18nProviderWrapper i18n={i18n}>
                {storyFn()}
            </I18nProviderWrapper>
        </ProviderWrapper>
    </div>
));