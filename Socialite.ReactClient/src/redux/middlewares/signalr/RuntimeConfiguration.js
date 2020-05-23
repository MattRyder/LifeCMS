import RuntimeConfigurationLoader from '../../../RuntimeConfigurationLoader';

export default class RuntimeConfiguration extends RuntimeConfigurationLoader {
    static keys() {
        return {
            websocket_host: 'REACT_APP_RUNTIME_WEBSOCKET_HOST',
        };
    }
}
