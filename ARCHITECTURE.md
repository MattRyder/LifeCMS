# Architecture of LifeCMS

This file contains a summary of the codebase in order to help new contributors
understand how LifeCMS is put together. All efforts are made to keep this consistent,
but we don't live in a perfect world - if you spot any inconsistencies, please raise
an issue ticket and this file will be updated based on your filing.

- [Architecture of LifeCMS](#architecture-of-lifecms)
- [Directory Structure](#directory-structure)

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
