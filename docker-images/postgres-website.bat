docker build -t postgres-website .\postgres-website
docker run -p 5432:5432 --name postgres-website-dev postgres-website
