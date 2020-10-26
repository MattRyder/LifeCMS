# Installing LifeCMS

LifeCMS is available on Windows, Mac OS X or Linux, using a technology called [Docker](https://docs.docker.com/get-docker). You don't need to have a deep understanding of Docker to install LifeCMS, just a few minutes of your time.

## Versioning

You can find the latest version of LifeCMS on [Docker Hub](https://hub.docker.com/u/lifecms). The default tag `latest` will always track the most recent stable release, to use a specific version, alter the `image` property inside the `docker-compose.yml` file.

For example:
```diff
identity.api:
-   image: lifecms_identity.api:latest
+   image: lifecms_identity.api:1.1
```

## Running the LifeCMS system

### Step 1: Download this repository

Once Docker is installed and running, enter the following commands into a terminal to download the repository:

```bash
git clone https://github.com/MattRyder/LifeCMS.git

cd LifeCMS/
```

### Step 2: Configure the system
TODO

### Step 3: Start the system

Once the system has been configured, you can start all required services by running the following command:

```bash
docker-compose up --detach
```

This single command will:

 - Start a MySQL database (for storing data)
 - Start the RabbitMQ event bus (for inter-service communication)
 - Run any pending migrations for each service
 - Starts the front-end application on http://lifecms.localhost

Navigate to http://lifecms.localhost and you'll see the system.
