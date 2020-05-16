import RuntimeConfigurationLoader from '../RuntimeConfigurationLoader';

export default class WebApiConfiguration extends RuntimeConfigurationLoader {
    static keys() {
        return {
            api_host: "REACT_APP_RUNTIME_WEB_API_HOST",
        };
    }
}
