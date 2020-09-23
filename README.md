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

## Installing LifeCMS

LifeCMS is available on Windows, Mac OS X or Linux.

First download and install [Docker](https://www.docker.com/products/docker-desktop), a tool that makes installing and updating LifeCMS really simple.

Once Docker is installed and running, enter the following commands into a terminal to download the repository and bring the LifeCMS system up.

```bash
git clone https://github.com/MattRyder/LifeCMS.git

cd LifeCMS/

docker-compose up
```
