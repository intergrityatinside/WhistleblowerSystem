Images löschen:
docker system prune -af

Starten
docker compose up

Neu builden + starten
 docker-compose up -d --no-deps --build app

 Nur builden
 docker build -t imagename .

 Image starten
 docker run -p PortHost:PortContainer (4000:80) imagename .
