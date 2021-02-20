export default {
    common: {
        add: 'commonAdd',
        back: 'commonBack',
        duplicate: 'commonDuplicate',
        edit: 'commonEdit',
        save: 'commonSave',
        load: 'commonLoad',
        delete: 'commonDelete',
        post: 'commonPost',
        settings: 'commonSettings',
        details: 'commonDetails',
        update: 'commonUpdate',
        clear: 'commonClear',
        upload: 'commonUpload',
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
    audience: {
        displayName: 'audienceDisplayName',
        description: 'audienceDescription',
        properties: {
            name: 'audiencePropertiesName',
        },
    },
    subscriber: {
        properties: {
            name: 'subscriberPropertiesName',
            emailAddress: 'subscriberPropertiesEmailAddress',
            subscribedAt: 'subscriberPropertiesSubscribedAt',
            unsubscribedAt: 'subscriberPropertiesUnsubscribedAt',
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
    fileUploader: {
        cta: 'fileUploaderCta',
    },
    editableText: {
        placeholder: 'editableTextPlaceholder',
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
        greeting: 'homeViewGreeting',
        sinceLastWeek: 'homeViewSinceLastWeek',
        activeUserProfileTitle: 'homeViewActiveUserProfileTitle',
        activeUserProfileCta: 'homeViewActiveUserProfileCta',
        statistics: {
            campaignCreated: 'homeViewStatisticsCampaignCreated',
            userProfilesCreated: 'homeViewStatisticsUserProfilesCreated',
            templatesCreated: 'homeViewStatisticsTemplatesCreated',
        },
    },
    navigationMenu: {
        header: {
            content: 'navigationMenuHeaderContent',
            newsletters: 'navigationMenuHeaderNewsletters',
            settings: 'navigationMenuHeaderSettings',
        },
        item: {
            home: 'navigationMenuItemHome',
            posts: 'navigationMenuItemPosts',
            audiences: 'navigationMenuItemAudiences',
            campaigns: 'navigationMenuItemCampaigns',
            templates: 'navigationMenuItemTemplates',
            userProfiles: 'navigationMenuItemUserProfiles',
            login: 'navigationMenuItemLogin',
            logout: 'navigationMenuItemLogout',
        },
    },
    audienceView: {
        create: {
            pageTitle: 'audienceViewCreatePageTitle',
            subscriberUploader: {
                title: 'audienceViewCreateSubscriberUploaderTitle',
                downloadCSV: 'audienceViewCreateSubscriberUploaderDownloadCSV',
                importCSV: 'audienceViewCreateSubscriberUploaderImportCSV',
                fileModalTitle: 'audienceViewCreateSubscriberUploaderFileModalTitle',
            },
        },
        addSubscribers: {
            pageTitle: 'audienceViewAddSubscribersPageTitle',
        },
        intro: {
            ctaText: 'audienceViewIntroCtaText',
        },
        index: {
            pageTitle: 'audienceViewIndexPageTitle',
            createAudience: 'audienceViewIndexCreateAudience',
            addSubscribers: 'audienceViewIndexCreateAddSubscribers',
        },
        details: {
            pageTitle: 'audienceViewDetailsPageTitle',
            subscriberList: {
                title: 'audienceViewDetailsSubscriberListTitle',
                previousPageButton: 'audienceViewDetailSubscriberListPreviousPage',
                nextPageButton: 'audienceViewDetailSubscriberListNextPage',
                pageText: 'audienceViewDetailSubscriberListPageText',
                notConfirmedText: 'audienceViewDetailSubscriberListNotConfirmedText',
            },
            noSubscribersNotice: {
                title: 'audienceViewDetailsNoSubscribersNoticeTitle',
                cta: 'audienceViewDetailsNoSubscribersNoticeCta',
            },
        },
        updateName: {
            pageTitle: 'audienceViewUpdateNamePageTitle',
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
                image: 'newsletterViewEditorToolboxImage',
            },
            imageAttribute: {
                fileModalLabel: 'newsletterViewEditorImageAttributeFileModalLabel',
            },
        },
        index: {
            createTemplateCta: 'newsletterViewIndexCreateTemplateCta',
        },
        templateSelector: {
            pageTitle: 'newsletterViewTemplateSelectorPageTitle',
            pageDescription: 'newsletterViewTemplateSelectorPageDescription',
            templates: {
                blank: {
                    title: 'newsletterViewTemplateSelectorTemplatesBlankTitle',
                    description: 'newsletterViewTemplateSelectorTemplatesBlankDescription',
                },
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
    subscriberConfirmView: {
        title: 'subscriberConfirmViewTitle',
        text: 'subscriberConfirmViewText',
        loadingText: 'subscriberConfirmViewLoadingText',
        warningText: 'subscriberConfirmViewWarningText',
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
            listCaption: 'userProfileViewIndexListCaption',
        },
        create: {
            avatarModalTitle: 'userProfileViewCreateAvatarModalTitle',
            headerModalTitle: 'userProfileViewCreateHeaderModalTitle',
        },
    },
};
