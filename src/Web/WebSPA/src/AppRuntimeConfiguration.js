import RuntimeConfigurationLoader from './RuntimeConfigurationLoader';

export default class AppRuntimeConfiguration extends RuntimeConfigurationLoader {
    static keys() {
        return {
            product_name: 'REACT_APP_RUNTIME_PRODUCT_NAME',
        };
    }
}
