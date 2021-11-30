#Login to heroku 
heroku login (opens the browser)
und
heroku container:login (logs in to the container registry)

#build the app (creates an image)
docker build -t whistleblowersystem .

#push the container
heroku container:push -a whistleblowersystem web

#release the container
heroku container:release -a whistleblowersystem web