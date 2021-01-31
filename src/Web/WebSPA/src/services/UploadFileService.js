import axios from 'axios';
import { getLifeCMSApi } from 'redux/LifeCMSApi';

export default async function UploadFileService(
    accessToken,
    filename,
    fileType,
    fileBlobUrl,
) {
    const lifecmsApi = getLifeCMSApi(accessToken);

    const fileBlob = await (await fetch(fileBlobUrl)).blob();

    try {
        const {
            data: {
                data: {
                    requestUrl,
                    requestUrn,
                },
            },
        } = await lifecmsApi.createPresignUrl({
            Filename: filename,
            Content_Type: fileType,
        });

        const { status } = await axios.put(requestUrl, fileBlob, {
            headers: {
                'Content-Type': fileType,
            },
        });

        if (status === 200) {
            return requestUrn;
        }

        return null;
    } catch (error) {
        console.error(error);
    }
}
