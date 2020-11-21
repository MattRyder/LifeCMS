import axios from 'axios';
import { getLifeCMSApi } from 'redux/LifeCMSApi';

export const FETCH_PRESIGN_URL_SUCCESS = 'FETCH_PRESIGN_URL_SUCCESS';
export const FETCH_PRESIGN_URL_FAILURE = 'FETCH_PRESIGN_URL_FAILURE';

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
                data: presignedUrl,
            } = {},
        } = await lifecmsApi.createPresignUrl({
            Filename: filename,
            Content_Type: fileType,
        });

        const { status } = await axios.put(presignedUrl, fileBlob, {
            headers: {
                'Content-Type': fileType,
            },
        });

        if (status === 200) {
            const { origin, pathname } = new URL(presignedUrl);

            return new URL(pathname, origin).href;
        }
    } catch (error) {
        console.error(error);
        // dispatch(fetchPresignUrlFailure(error.message));
    }
}
