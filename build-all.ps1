Write-host "Building process started..."  -ForegroundColor DarkGreen

docker-compose down
docker-compose build --no-cache
docker-compose up -d

Write-host "Building process completed."  -ForegroundColor DarkGreen