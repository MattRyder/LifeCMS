export default {
    common: {
        edit: 'commonEdit',
        save: 'commonSave',
        load: 'commonLoad',
        delete: 'commonDelete',
        post: 'commonPost',
        settings: 'commonSettings',
        details: 'commonDetails',
        update: 'commonUpdate',
        createdAt: 'commonCreatedAt',
        updatedAt: 'commonUpdatedAt',
    },
    confirm: {
        areYouSure: 'confirmAreYouSure',
        message: 'confirmMessage',
        ok: 'confirmOk',
        cancel: 'confirmCancel',
    },
    post: {
        noPostsAvailable: 'noPostsAvailable',
        create: {
            title: {
                placeholder: 'postCreateTitlePlaceholder',
            },
            text: {
                placeholder: 'postCreateTextPlaceholder',
            },
        },
    },
    campaign: {
        properties: {
            newsletterTemplate: 'campaignPropertiesNewsletterTemplate',
            userProfile: 'campaignPropertiesUserProfile',
            name: 'campaignPropertiesName',
            subjectLineText: 'campaignPropertiesSubjectLineText',
            previewText: 'campaignPropertiesPreviewText',
            scheduledDate: 'campaignPropertiesScheduledDate',
            useSubscriberTimeZone: 'campaignPropertiesUseSubTimeZone',
        },
    },
    userProfile: {
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
    },
    homeView: {
        pageTitle: 'homeViewPageTitle',
    },
    navigationMenu: {
        header: {
            content: 'navigationMenuHeaderContent',
            newsletters: 'navigationMenuHeaderNewsletters',
            settings: 'navigationMenuHeaderSettings',
        },
        item: {
            posts: 'navigationMenuItemPosts',
            campaigns: 'navigationMenuItemCampaigns',
            templates: 'navigationMenuItemTemplates',
            userProfiles: 'navigationMenuItemUserProfiles',
            login: 'navigationMenuItemLogin',
            logout: 'navigationMenuItemLogout',
        },
    },
    campaignView: {
        index: {
            pageTitle: 'campaignViewIndexTitle',
            createCampaign: 'campaignViewIndexCreateCampaign',
            scheduledFor: 'campaignViewIndexScheduledFor',
            createdAt: 'campaignViewIndexCreatedAt',
        },
        create: {
            pageTitle: 'campaignViewCreateTitle',
            form: {
                subscriberHint: 'campaignViewCreateSubscriberTimeZoneHint',
                scheduledDatePlaceholder: 'campaignViewCreateScheduledDatePlaceholder',
            },
        },
        detail: {
            pageTitle: 'campaignViewDetailTitle',
            subjectDetailTitle: 'campaignViewDetailSubjectDetailTitle',
        },
        updateSubject: {
            pageTitle: 'campaignViewUpdateSubject',
        },
        updateName: {
            pageTitle: 'campaignViewUpdateName',
        },
    },
    newsletterView: {
        dashboard: {
            pageTitle: 'newsletterViewDashboardPageTitle',
            menu: {
                templates: 'newsletterViewDashboardMenuTemplates',
                mailingLists: 'newsletterViewDashboardMenuMailingLists',
            },
        },
        pageTitle: 'newsletterViewPageTitle',
        createNewsletter: 'newsletterViewCreateNewsletter',
        listCaption: 'newsletterViewListCaption',
        listViewPreview: 'newsletterViewListViewPreview',
        editorTitleCreate: 'newsletterViewEditorTitleCreate',
        editorTitleEdit: 'newsletterViewEditorTitleEdit',
        previewPageTitle: 'newsletterPreviewPageTitle',
    },
    profileView: {
        connect: 'connect',
        editDetails: 'editDetails',
        menuItemPosts: 'menuItemPosts',
        menuItemPhotos: 'menuItemPhotos',
    },
    settingsView: {
        menu: {
            userProfiles: 'settingsViewMenuUserProfiles',
            newsletters: 'settingsViewMenuNewsletters',
        },
        userProfiles: {
            create: 'settingsViewUserProfileCreate',
            edit: 'settingsViewUserProfileEdit',
        },
    },
};
