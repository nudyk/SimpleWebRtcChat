rem Commands for publish on heroku.
rem Run by line in terminal or powersgell
heroku login
heroku container:login
heroku container:push web -a simple-web-rtc-chat
heroku container:release web -a simple-web-rtc-chat