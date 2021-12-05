open git bash or cmd @ the path where the dockerfile is

#Login to heroku 
heroku login (opens the browser)
und
heroku container:login (logs in to the container registry)

#build the app (creates an image) (-t: image name must be the same as the heroku app name)
docker build -t whistleblowersystem .

#push the container
heroku container:push -a whistleblowersystem web

#release the container
heroku container:release -a whistleblowersystem web