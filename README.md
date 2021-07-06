# FireFinder
A layer over the ugly json response for http://intranet.ooelfv.at written in ASP.NET CORE

Demo: https://firefinder.azurewebsites.net/api/index.html#/Data

## Documentation
Swagger an Redoc available

## Install

### Debian 10

sudo apt-get install -y dotnet-sdk-5.0

dotnet build

docker build -t firefinder:1 .

docker run -d -p 5001:80 firefinder:1