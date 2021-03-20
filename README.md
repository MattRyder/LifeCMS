LifeCMS
===

[
    ![CircleCI](https://circleci.com/gh/MattRyder/LifeCMS/tree/master.svg?style=shield)
](https://circleci.com/gh/MattRyder/LifeCMS/tree/master)
[
    ![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/identity.api?label=docker%3A%20identity)
](https://hub.docker.com/repository/docker/lifecms/identity.api)
[
    ![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/contentcreation.api?label=docker%3A%20content-creation)
](https://hub.docker.com/repository/docker/lifecms/contentcreation.api)
[
    ![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/email.api?label=docker%3A%20email)
](https://hub.docker.com/repository/docker/lifecms/email.api)
[
    ![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/web-spa?label=docker%3A%20web-spa)
](https://hub.docker.com/repository/docker/lifecms/web-spa)

<img
    src="docs/screenshots/lifecms-promo.webp"
    alt="lifecms-promo-gif">
<a href="https://youtu.be/1ng7bAJJ-XA" target="_blank">
    View this promotional video on YouTube
</a>

LifeCMS is a web-based application for recording events,  curating engaging newsletters, and sharing them with a close audience.

LifeCMS was built from the ground-up as a secure system to store personal goals, successes, and memories.
A snappy post editor brings a reactive story workshop, crafted to help deliver the best in your writing. An easy-to-use photo studio that stores and organizes your holiday snapshots and one-in-a-lifetime moments with an intuitive interface.

What you share and who you share it with is a fundamental decision found within all LifeCMS functionality.
A person can have many different LifeCMS identities, which only require a nickname to create. An identity can group and share content with a specific set of people or provide context for a set of stories or articles.

LifeCMS is multilingual, the platform is available in five languages:

* English (United Kingdom)
* Français (France)
* Español (España)
* Deutsch
* Italiano

## Can LifeCMS be self-hosted?

Yes, LifeCMS was built to be self-hosted, which is why this product is free, open-source, and comes with a maintained docker-compose.yml file to start the system with all required dependencies.

The only difference between the commercial deployment and a self-hosted deployment is that you will have to manage the infrastructure you run LifeCMS on.

For more information on self-hosting LifeCMS, [check out the installation guide](docs/installation.md).


## Technology

LifeCMS is an ASP.NET Core microservice product, using React to power the front-end user experiences.

A Dockerfile is provided in order to build each service, and a docker-compose.yml file has been produced to aid the operation and management of the product.

To find out more about the architecture of the platform, check out [ARCHITECTURE.md](./ARCHITECTURE.md)

## Licence

LifeCMS is open-sourced under the GNU Affero General Public License Version 3 (AGPLv3). You can find the terms of APGLv3 in [LICENCE.md](LICENCE.md).
