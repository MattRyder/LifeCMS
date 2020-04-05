import React from 'react';
import { addParameters, addDecorator, configure } from '@storybook/react';
import i18n, { supportedLocales } from '../src/i18n';
import { I18nProviderWrapper } from './wrappers/I18nProviderWrapper';
import { withI18n } from 'storybook-addon-i18n';
import ProviderWrapper from '../src/Provider';
import configureStore, { history } from '../src/redux/AppStore';
import userManager from '../src/openid/UserManager';

const store = configureStore();

addDecorator((storyFn) => (
  <I18nProviderWrapper i18n={i18n}>
    {storyFn()}
  </I18nProviderWrapper>
));

addDecorator((storyFn) => (
  <ProviderWrapper store={store} history={history} userManager={userManager}>
    {storyFn()}
  </ProviderWrapper>
));

addDecorator(withI18n);

function loadStories() {
  require('../src/stories/index');
}

addParameters({
  i18n: {
    provider: I18nProviderWrapper,
    providerProps: {
      i18n
    },
    supportedLocales
  }
});

configure(loadStories, module);
