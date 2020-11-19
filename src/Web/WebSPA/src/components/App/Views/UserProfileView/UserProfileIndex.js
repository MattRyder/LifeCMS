import React from 'react';
import { useSelector } from 'react-redux';
import { ReactComponent as UserProfileIllustration } from 'assets/illustrations/user-profile-illustration.svg';
import { fetchUserProfiles } from '../../../../redux/actions/UserProfileActions';
import { useContentApi, useUser, useTranslations } from '../../../../hooks';
import UserProfileListViewRowComponent from '../../../UserProfile/UserProfileListViewRowComponent';
import Table from '../../../Util/Table/Table';
import ListView from '../ListView';
import IntroView from '../IntroView';

const newUserProfileRoute = '/user-profiles/new';

const UserProfileIndexList = ({ collection }) => {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <ListView
            title={t(TextTranslationKeys.settingsView.menu.userProfiles)}
            ctaLinkTo={newUserProfileRoute}
            ctaText={t(TextTranslationKeys.settingsView.userProfiles.create)}
        >
            <Table
                headings={[
                    t(TextTranslationKeys.userProfile.properties.name),
                    t(TextTranslationKeys.userProfile.properties.occupation),
                    t(TextTranslationKeys.userProfile.properties.location),
                ]}
                rowComponent={UserProfileListViewRowComponent}
                collection={collection}
                accessibilityDescription={t(TextTranslationKeys.userProfileView.index.listCaption)}
            />
        </ListView>
    );
};

const UserProfileIntro = () => {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <IntroView
            ctaTo={newUserProfileRoute}
            ctaText={t(TextTranslationKeys.userProfileView.index.createUserProfileCta)}
            resourceDescription={t(TextTranslationKeys.userProfile.description)}
            resourceTitle={t(TextTranslationKeys.userProfile.displayName)}
        >
            <UserProfileIllustration />
        </IntroView>
    );
};

export default function UserProfileIndex() {
    const { accessToken, userId } = useUser();

    const userProfileState = useSelector(
        (state) => state.userProfile[userId],
    );

    const hasUserProfiles = userProfileState.userProfiles
        && userProfileState.userProfiles.length > 0;

    useContentApi(
        () => fetchUserProfiles(accessToken, userId),
        accessToken,
        userId,
    );

    return hasUserProfiles ? (
        <UserProfileIndexList
            collection={userProfileState && userProfileState.userProfiles}
        />
    ) : <UserProfileIntro />;
}
