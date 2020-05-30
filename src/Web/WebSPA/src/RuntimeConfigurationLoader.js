export default class RuntimeConfigurationLoader {
    static json() {
        return (process.env.NODE_ENV === 'production')
            ? this.readRuntimeConfig()
            : this.readEnv();
    }

    static readEnv() {
        const envData = [];

        Object.entries(this.keys()).map(([runtimeKey, envVar]) =>
            envData[runtimeKey] = process.env[envVar]);

        return envData;
    }

    static readRuntimeConfig() {
        const envData = [];

        Object.entries(this.keys()).map(([runtimeKey]) =>
            envData[runtimeKey] = window.runtime[runtimeKey]);

        return envData;
    }
}
