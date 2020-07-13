LifeCMS
===

Overview
---
LifeCMS is a web-based application for safely storing life events,  curating engaging newsletters, and sharing them with a close audience.

LifeCMS was built from the ground-up as a secure system to store personal goals, successes, and memories.
A snappy post editor brings a reactive story workshop, crafted to help deliver the best in your writing. An easy-to-use photo studio that stores and organizes your holiday snapshots and one-in-a-lifetime moments with an intuitive interface.

What you share and who you share it with is a fundamental decision found within all LifeCMS functionality.
A person can have many different LifeCMS identities, which only require a nickname to create. An identity can group and share content with a specific set of people or provide context for a set of stories or articles.

[![CircleCI](https://circleci.com/gh/MattRyder/LifeCMS/tree/master.svg?style=shield)](https://circleci.com/gh/MattRyder/LifeCMS/tree/master)
![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/identity.api?label=docker%3A%20identity)
![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/contentcreation.api?label=docker%3A%20content-creation)
![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/email.api?label=docker%3A%20email)
![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/lifecms/web-spa?label=docker%3A%20web-spa)


## Installing LifeCMS

LifeCMS is available on Windows, Mac OS X or Linux.

First download and install [Docker](https://www.docker.com/products/docker-desktop), a tool that makes installing and updating LifeCMS really simple.

Once Docker is installed and running, enter the following commands into a terminal to setup the address of the system, download the repository and bring the LifeCMS system to life.

```bash
echo '127.0.0.1 contentcreation.lifecms.local identity.lifecms.local lifecms.local
' | sudo tee -a /etc/hosts

git clone https://github.com/MattRyder/LifeCMS.git

cd LifeCMS/

docker-compose up
```

Once all of the services are ready for use, LifeCMS will be available on your computer to use at:  http://lifecms.local