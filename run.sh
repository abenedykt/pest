# stop executing script if any error occurs
set -e

# build and run the docker container
docker compose build 
docker compose up -d


# open startup.html that contains links to all the services and tools
open startup.html
