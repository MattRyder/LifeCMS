# Architecture of LifeCMS

This file contains a summary of the codebase in order to help new contributors
understand how LifeCMS is put together. All efforts are made to keep this
consistent, but we don't live in a perfect world - if you spot any
inconsistencies, please raise an issue ticket and this file will be updated
based on your filing.

- [Architecture of LifeCMS](#architecture-of-lifecms)
- [Directory Structure](#directory-structure)
- [Service Overview](#service-overview)
  - [EventBus](#eventbus)
  - [Services](#services)
    - [ContentCreation](#contentcreation)
    - [Identity](#identity)
    - [Email](#email)
  - [Web](#web)
    - [WebSPA](#webspa)
- [Environment Variables](#environment-variables)

# Directory Structure

| File / Directory             | Description                                                                               |
| ---------------------------- | ----------------------------------------------------------------------------------------- |
| .circleci/                   | Tooling required for the CircleCI Continuous Integration.                                 |
| docker/                      | Tooling required to operate the docker-compose.yml.                                       |
| docs/                        | Documentation about the project, word from the devs in human-readable form.               |
| src/                         | Here's where you'll find the source code to LifeCMS.                                      |
| src/EventBus                 | LifeCMS' EventBus code, used to send messages between microservices.                      |
| src/Services/ContentCreation | The microservice responsible for user-generated content.                                  |
| src/Service/Email            | The microservice responsible for email dispatch and delivery.                             |
| src/Services/Identity        | The microservice that operates an an OpenID Connect-compliant identity management server. |
| src/Web/WebSPA               | The main front-end application for user interaction.                                      |

# Service Overview

## EventBus

[...]

## Services

### ContentCreation

LifeCMS provides an API for various resources of user-generated content to be
created, modified and updated. These resources enable users to manage audiences,
JSON-based templates and other data that pertains to operation of the service.

ContentCreation is architected with a Domain-Driven Design style, the project is
comprised of multiple tiers which house discrete segments of the project's
functionality. 

Business logic is stored in the ContentCreation.Domain project, where each main
resource inherits from IAggregateRoot, a single discrete unit that the project
operates on. 

Classes and functionality required to provide data access and persistence are
found in the ContentCreation.Infrastructure, writing aggregates to disk and
dispatching messages to the EventBus.

The interactable API is found in ContentCreation.API, an ASP.NET Core WebAPI
used to serve data to external services and users. CQRS is employed to leverage
the architecture benefits of separate commands and queries, these classes are
seen in the Application directory of the ContentCreation.API project.

### Identity

LifeCMS provides an OpenID Connect compliant authentication server, in the form
of the Identity project. This server enables users to register, login and manage
their authentication options as well as providing Single-Sign On (SSO) for
third-party services, such as Azure Active Directory and Google Accounts.

The project is comprised of multiple tiers which house discrete segments of the
project's functionality. 

The project relies on IdentityServer4 to provide the framework for the OpenID
Connect implementation.

Classes and functionality required to provide data access and persistence are
found in the ContentCreation.Infrastructure, writing user authentication models
to disk and dispatching messages to the EventBus.

The interactable API is found in ContentCreation.API, an ASP.NET Core project
used to serve front-end experiences to external services and users. The project 
has a separate ReactJS client-facing application embedded into the project for
powering the UI, found in ClientApp/.

### Email

A newsletter-editing application obviously depends on its reliability and
security of its email distribution system, and so, LifeCMS has a microservice
project designed to support the reliable delivery of all email messages sent 
within the system.

The project is comprised of multiple tiers which house discrete segments of the
project's functionality.

Business logic is stored in the Email.Domain project.

Classes and functionality required to provide data access and persistence are
found in the Email.Infrastructure, writing aggregates to disk and dispatching
messages to the EventBus.

The Email.API project contains a RabbitMQ persistent connection worker, that
listens for events raised by internal services and processes the messages that
it has subscribed to. 

## Web

### WebSPA

[...]

# Environment Variables

This section lists environment variables that are used directly or indirectly
by LifeCMS. These variables will also be found in other sections of this
repository.

The LifeCMS system is built to be fully whitelabelled, and developers are
encouraged to provide various options to allow a user to alter the look-and-feel
of the system, or apply changes to how the functionality operates.

| Environment Variable                             | Description                                                                                   | Example                                                       |
| ------------------------------------------------ | --------------------------------------------------------------------------------------------- | ------------------------------------------------------------- |
| LIFECMS_CONTENTCREATION_EMAIL_FROM_EMAIL_ADDRESS | The email address to use when sending email from the ContentCreation service.                 | no-reply@lifecms.localhost                                    |
| LIFECMS_CONTENTCREATION_EMAIL_FROM_DISPLAY_NAME  | The display name to use when sending email from the ContentCreation service.                  | NOREPLY                                                       |
| LIFECMS_CERTIFICATE_SIGNING_CREDENTIAL           | X509 certificate key material used to sign the authentication credentials.                    | MIIQHAIBAn[...]CAggA (redacted due to length)                 |
| LIFECMS_EVENTBUS_QUEUENAME                       | The name of the RabbitMQ queue used by the system.                                            | localhost.lifecms.queue                                       |
| LIFECMS_RABBITMQ_USERNAME                        | Username of the user to access the RabbitMQ instance.                                         | rabbitmq_user                                                 |
| LIFECMS_RABBITMQ_PASSWORD                        | Password of the user to access the RabbitMQ instance.                                         | rabbitmq_password                                             |
| LIFECMS_ORIGIN_CONTENTCREATION                   | Origin of the ContentCreation service, used for CORS whitelisting.                            | http://contentcreation.lifecms.localhost                      |
| LIFECMS_ORIGIN_WEBSPA                            | Origin of the WebSPA service, used for CORS whitelisting.                                     | http://lifecms.localhost                                      |
| LIFECMS_VHOST_CONTENTCREATION                    | Hostname of the ContentCreation service.                                                      | contentcreation.lifecms.localhost                             |
| LIFECMS_VHOST_IDENTITY                           | Hostname of the Identity service.                                                             | identity.lifecms.localhost                                    |
| LIFECMS_VHOST_WEBSPA                             | Hostname of the WebSPA service.                                                               | app.lifecms.localhost                                         |
| LIFECMS_VHOST_EMAIL                              | Hostname of the Email service.                                                                | email.lifecms.localhost                                       |
| LIFECMS_EMAIL_PROVIDER                           | One of the following values: Smtp, SendGrid. Set up all LIFECMS_EMAIL_* keys for your option. | Smtp                                                          |
| LIFECMS_EMAIL_SMTP_HOST                          | The email address to use when sending emails through SMTP.                                    | noreply@lifecms.localhost                                     |
| LIFECMS_EMAIL_SMTP_PORT                          | The display name to use when sending emails through SMTP.                                     | 25                                                            |
| LIFECMS_EMAIL_SENDGRID_APIKEY                    | The API key to be used when working with the SendGrid API.                                    | SG.ngeVfQFYQlKU0ufo8x5d1A.EXAMPLE-09kqeF8tAmbihYzrnopKc-1s5cr |
| LIFECMS_EMAIL_SENDGRID_FROM_EMAIL                | The email address to use when sending emails through SendGrid.                                | noreply@lifecms.localhost                                     |
| LIFECMS_EMAIL_SENDGRID_FROM_NAME                 | The display name to use when sending emails through SendGrid.                                 | NOREPLY                                                       |
| LIFECMS_IDENTITY_EMAIL_FROM_EMAIL_ADDRESS        | The email address to use when sending email from the Identity service.                        | no-reply@lifecms.localhost                                    |
| LIFECMS_IDENTITY_SERVER_AUTHORITY                | Origin of the Identity server used as the signing authority                                   | http://identity.lifecms.localhost                             |
