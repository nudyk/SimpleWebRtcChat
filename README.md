# Simple Web Rtc Chat
Video chat over WebRtc
This video chat was written using:
- [Asp.Net Core 3(MVC)](https://github.com/dotnet/aspnetcore)
- [SignalR](https://github.com/SignalR/SignalR)
- [webrtc-adapter(wraper for WebRTC)](https://github.com/webrtc/adapter)
- [peerjs](https://github.com/peers/peerjs)
- [mustache.js](https://github.com/janl/mustache.js/)
- [jquery](https://github.com/jquery/jquery)
- [bootstrap](https://github.com/twbs/bootstrap)
- [Docker](https://github.com/docker)

Demo is available [here](https://simple-web-rtc-chat.herokuapp.com/)

# For deploy on [HEROKU](https://dashboard.heroku.com/) need:
## Create heroku app
* Go to Heroku website and log in or sign up if you don’t have account yet. 
* Then click on New and Create new app
* Enter the name you like and select the region (closest to you would be the best). 
Remember the name of the application – you will need it in the next steps. 
* Click on Create app button – your app is now ready.
## Deploy to Heroku
Goto to folder \src\SimpleWebRtcChat.Web and run commands in sequence:
* PS> heroku login
* PS> heroku container:login
* PS> heroku container:push web -a < heroku-app-name >
* PS> heroku container:release web -a < heroku-app-name >

An example of such commands can be found in the file:
[\src\SimpleWebRtcChat.Web\3 DeployDocker.cmd](https://github.com/nudyk/SimpleWebRtcChat/blob/master/src/SimpleWebRtcChat.Web/3%20DeployDocker.cmd)
Do not run this file. Commands must be executed sequentially.
