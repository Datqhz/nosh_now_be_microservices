# NoshNow Backend

This project writen to handle data for NoshNow and NoshNow Management application, here is [FE repo](https://github.com/Datqhz/nosh-now-fe).

## Tech Stacks
1. **Programing language**: C#
2. **Framework/library**: .NET
3. **Database**: PostgreSQL

## Getting started
### Prerequesite
1. Docker desktop - step by step to install [here](https://docs.docker.com/engine/install/)
2. DataGrip/DBeaver

### Installations
1. **Setup database**
    - **Run `db` service**
        ```
        docker-compose up db
        ```
    - **Run script**
        1. Connect to database:  
        Use DataGrip or DBeaver or any extension can connect to Postgres database
        2. Run script:  
        Run 2 script in order `script01.sql` to `seed01.sql`
2. **Build and run**
    ```
    docker-compose up -d
    ```
    Now, your server is running on localhost.