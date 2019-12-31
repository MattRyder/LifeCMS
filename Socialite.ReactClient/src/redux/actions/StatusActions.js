import SocialiteApi from "../SocialiteApi";

export const FETCH_USER_STATUSES_BEGIN = "FETCH_USER_STATUSES_BEGIN";
export const FETCH_USER_STATUSES_SUCCESS = "FETCH_USER_STATUSES_SUCCESS";
export const FETCH_USER_STATUSES_FAILURE = "FETCH_USER_STATUSES_FAILURE";

export const fetchUserStatusesBegin = () => ({
    type: FETCH_USER_STATUSES_BEGIN
});

export const fetchUserStatusesSuccess = statuses => ({
    type: FETCH_USER_STATUSES_SUCCESS,
    payload: { statuses },
});

export const fetchUserStatusesFailure = error => ({
    type: FETCH_USER_STATUSES_FAILURE,
    payload: { error }
});

export const fetchStatuses = () => {
    var socialiteApi = new SocialiteApi(process.env.REACT_APP_API_HOST, process.env.REACT_APP_API_KEY)

    return dispatch => {
        dispatch(fetchUserStatusesBegin());

        return socialiteApi
            .getStatuses()
            .catch(error => {
                dispatch(fetchUserStatusesFailure(error));
            })
            .then(response => {
                const statuses = response.data;

                dispatch(fetchUserStatusesSuccess(statuses));

                return statuses;
            });
    }
};