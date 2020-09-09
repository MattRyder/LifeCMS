import {
    FETCH_USER_CAMPAIGNS_BEGIN,
    FETCH_USER_CAMPAIGNS_FAILURE,
    FETCH_USER_CAMPAIGNS_SUCCESS,
    DELETE_USER_CAMPAIGN_SUCCESS,
} from '../actions/CampaignActions';

const CampaignReducer = (state = {}, action) => {
    switch (action.type) {
    case FETCH_USER_CAMPAIGNS_BEGIN:
        return {
            ...state,
            [action.payload.userId]: {
                campaigns: [],
                loading: true,
                error: null,
            },
        };
    case FETCH_USER_CAMPAIGNS_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                campaigns: action.payload.campaigns,
                loading: false,
            },
        };
    case FETCH_USER_CAMPAIGNS_FAILURE:
        return {
            ...state,
            [action.payload.userId]: {
                loading: false,
                error: action.payload.error,
            },
        };
    case DELETE_USER_CAMPAIGN_SUCCESS: {
        const { userId, campaignId } = action.payload;
        return {
            ...state,
            [userId]: {
                ...state[userId],
                campaigns: state[userId]
                    .campaigns
                    .filter((campaign) => campaign.id !== campaignId),
            },
        };
    }
    default:
        return state;
    }
};

export default CampaignReducer;
