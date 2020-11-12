export default {
    common: {
        duplicate: 'commonDuplicate',
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
        displayName: 'campaignDisplayName',
        description: 'campaignDescription',
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
    template: {
        displayName: 'templateDisplayName',
        description: 'templateDescription',
        properties: {
            name: 'templatePropertiesName',
        },
    },
    userProfile: {
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
            createCampaignCta: 'campaignViewIndexCreateCampaignCta',

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
        editor: {
            title: 'newsletterViewEditorTitle',
            toolboxHelp: 'newsletterViewEditorToolboxHelp',
            mode: {
                basic: 'newsletterViewEditorModeBasic',
                advanced: 'newsletterViewEditorModeAdvanced',
            },
            toolbox: {
                row: 'newsletterViewEditorToolboxRow',
                freeText: 'newsletterViewEditorToolboxFreeText',
                columns: 'newsletterViewEditorToolboxColumns',
            },

        },
        index: {
            createTemplateCta: 'newsletterViewIndexCreateTemplateCta',
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
    userProfileView: {
        index: {
            createUserProfileCta: 'userProfileViewIndexCreateUserProfileCta',
        },
    },
};
