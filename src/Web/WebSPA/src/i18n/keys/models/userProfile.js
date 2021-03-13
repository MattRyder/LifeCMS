export default {
    displayName: 'userProfileDisplayName',
    description: 'userProfileDescription',
    properties: {
        name: 'userProfilePropertiesName',
        emailAddress: 'userProfilePropertiesEmailAddress',
        occupation: 'userProfilePropertiesOccupation',
        location: 'userProfilePropertiesLocation',
        bio: 'userProfilePropertiesBio',
        avatarImage: 'userProfilePropertiesAvatarImage',
        headerImage: 'userProfilePropertiesHeaderImage',
    },
    create: {
        name: {
            label: 'userProfileCreateNameLabel',
            hint: 'userProfileCreateNameHint',
        },
        emailAddress: 'userProfileCreateEmailAddressLabel',
        occupation: 'userProfileCreateOccupationLabel',
        location: 'userProfileCreateLocationLabel',
        bio: 'userProfileCreateBioLabel',
        avatarImageUri: 'userProfileCreateAvatarImageUriLabel',
        headerImageUri: 'userProfileCreateHeaderImageUriLabel',
    },
    notification: {
        createSuccess: 'userProfileNotificationCreateSuccess',
        createFailure: 'userProfileNotificationCreateFailure',
    },
};
