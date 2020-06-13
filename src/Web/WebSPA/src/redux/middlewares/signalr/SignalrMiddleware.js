import {
    JsonHubProtocol,
    HttpTransportType,
    HubConnectionBuilder,
} from '@microsoft/signalr';

const startConnection = (connection) => connection.start();

const getConnection = (hubUrl) => {
    const protocol = new JsonHubProtocol();

    // eslint-disable-next-line no-bitwise
    const transport = HttpTransportType.WebSockets | HttpTransportType.LongPolling;

    const options = {
        transport,
        logMessageContent: true,
    };

    return new HubConnectionBuilder()
        .withUrl(hubUrl, options)
        .withHubProtocol(protocol)
        .build();
};

const registerEvents = (store, connection, eventCallbacks) => {
    eventCallbacks.forEach(({ eventName, eventCallback }) => {
        connection.on(eventName, (...args) => {
            eventCallback
                .call(null, ...args)
                .call(store, store.getState.bind(store), store.dispatch.bind(store));
        });
    });
};

export default ({
    eventCallbacks, url, onStart = () => { },
}) => (store) => {
    const connection = getConnection(url);

    registerEvents(store, connection, eventCallbacks);

    connection.onclose(() => setTimeout(startConnection(connection), 1000));

    startConnection(connection)
        .then(() => onStart())
        .catch((err) => console.log(`Signalr Error: ${err}`));

    return (next) => (action) => (
        typeof action === 'function'
            ? action(
                store.dispatch.bind(store),
                store.getState.bind(store),
                connection.invoke.bind(connection),
            )
            : next(action)
    );
};
