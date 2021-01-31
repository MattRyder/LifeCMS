import { getLifeCMSApi } from 'redux/LifeCMSApi';

export default async function FileUrnService(accessToken, fileUrn) {
    const client = getLifeCMSApi(accessToken);

    try {
        const { data: { data } } = await client.createFileUri(encodeURI(fileUrn));

        return data;
    } catch (error) {
        return null;
    }
}
