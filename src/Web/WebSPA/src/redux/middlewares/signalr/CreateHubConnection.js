import {
    JsonHubProtocol,
    HttpTransportType,
    HubConnectionBuilder,
    LogLevel,
} from '@microsoft/signalr';

const startConnection = (connection) => connection.start();

export default (hubUrl, accessToken) => {
    const protocol = new JsonHubProtocol();

    const transport =
        HttpTransportType.WebSockets |
        HttpTransportType.LongPolling;

    const options = {
        transport,
        logMessageContent: true,
        logger: LogLevel.Trace,
        // accessTokenFactory: () => accessToken,
    };

    const connection = new HubConnectionBuilder()
        .withUrl(hubUrl, options)
        .withHubProtocol(protocol)
        .build();

    connection.on(
        'RecieveMessage',
        (res) => console.log(`WebSocket: ${res}`)
    );

    connection.onclose(() => setTimeout(startConnection(connection), 1000));

    startConnection(connection);
};