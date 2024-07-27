# stop executing script if any error occurs
set -e

# start docker (macOS only)
if [ "$(uname)" = "Darwin" ]; then
  open -a Docker
  
  printf "Waiting for Docker to start."
  timeout=60
  start_time=$(date +%s)
  while ! docker ps > /dev/null 2>&1; do
    current_time=$(date +%s)
    elapsed_time=$((current_time - start_time))
    if [ $elapsed_time -gt $timeout ]; then
      echo "Docker took too long to start. Bye!"
      exit 1
    fi
      printf "."
      sleep 1
    done
fi

# build and run the docker container
docker compose build 
docker compose up -d


# open startup.html that contains links to all the services and tools
open startup.html
