$version = $args[0];
$imageName = 'ghcr.io/ginomessmer/tinf-who-is-bot';

docker build ../src --file ../src/TinfWhoIs.DiscordBot/Dockerfile --tag $imageName
docker tag $imageName ${imageName}:${version}
docker push ${imageName}:${version}