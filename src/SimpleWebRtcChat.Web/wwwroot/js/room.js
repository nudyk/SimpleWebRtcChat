$(function() {
    var $videoCarusel = $("#app-video-carusel");
    var video_switch = true;
    var mic_switch = true;
    var videoMyself = document.querySelector('video.mine');
    var peer = null;
    var myStream = null;
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    var videos = [];
    var self = this;
    var existUsers = [];
    //var appVideoCarusel = new Vue({
    //    el: '#app-video-carusel',
    //    data: {
    //        videos: videos
    //    }
    //});
    $("#copy-to-clipboard").click(function() {
        var urlElement = document.querySelector('#room-url');
        urlElement.select();
        //urlElement.copyText.setSelectionRange(0, 99999); /*For mobile devices*/
        document.execCommand("copy");
        $('.toast.url-copyed').toast();
    });
    
    var getMyVideoStream = function(callback) {
        navigator.getUserMedia
        ({ video: video_switch, audio: mic_switch }, function (stream) {
            myStream = stream;
            videoMyself.srcObject = stream;
            callback(stream);
        }, function (err) {
            console.log('Failed to get local stream', err);
        });
    }
    
    var userJoin = function (roomUid, pearId, userName) {
        if (connection.connectionStarted) {
            connection.invoke("userJoin",roomUid, pearId, userName)
                .then(function(result) {
                    console.log("userJoin result", result);
                    existUsers = result;
                })
                .catch(function (err) {
                    console.error("userJoin", err);
                });
        }
    };

    var addVideoForRemoteUser = function(peerId, userName, remoteStream) {
        $("#peer_" + peerId).remove();
        var template = document.getElementById('tplVideo').innerHTML;
        var rendered = Mustache.render(template, { peerId: peerId, userName: userName });
        $videoCarusel.append(rendered);
        var videoSelector = "#video_" + userName;
        var attemtVounter = 60;
        var timer = setInterval(function() {
            var remoteVideo = document.querySelector(videoSelector);
            attemtVounter--;
            if (!remoteVideo && attemtVounter >= 0) {
                return;
            }
            clearInterval(timer);
            remoteVideo.srcObject = remoteStream;
        }, 500);
        return;
    };

    connection.start().then(function (p) {
        console.log("signalr connection.start", p);
        getMyVideoStream(function(stream) {
            peer = new Peer();//new Peer(chatOptions.uid);
            peer.on('call', function (call) {
                console.log("On call", arguments);
                call.answer(stream);
                call.on('stream', function (remoteStream) {
                    console.log("On call streem", remoteStream);
                    var users = existUsers.filter(function(el) { return el.peerId == call.peer });
                    if (users.length > 0) {
                        var u = users[0];
                        addVideoForRemoteUser(u.peerId, u.name, remoteStream);
                    }
                });
            });
        
            peer.on('open', function(peerId) {
                userJoin(chatOptions.uid, peerId, chatOptions.userName);
                console.log('peer open: ', arguments);
            });
            
            peer.on('connection', function(conn) {
                console.log('New connection: ', arguments);
                conn.on('data', function(data){
                    console.log(data);
                });
            });
            
            peer.on('close', function(conn) {
                console.log('peer close: ', arguments);
            });
            
            peer.on('disconnected', function(conn) {
                console.log('peer disconnected: ', arguments);
            });
            
            peer.on('error', function(conn) {
                console.error('peer error: ', arguments);
            });
        });
    }).catch(function (err) {
        return console.error(err.toString());
    });
    
    
    connection.on("onUserJoin", function (pearId, userName) {
        console.log("onUserJoin", arguments);
        var call = peer.call(pearId, myStream);
        call.on('stream',
            function(remoteStream) {
                console.log("Remote Stream", remoteStream);
                addVideoForRemoteUser(pearId, userName, remoteStream);
                //remoteVideo.srcObject = remoteStream;
            });
        var conn = peer.connect(peerId);
        conn.on('open', function(){
            // here you have conn.id
            conn.send({ peerId: peerId, userName: chatOptions.userName});
        });
    });
    connection.on("onUserDisconected", function(peerId, userName) {
        console.log("onUserDisconected", arguments);
        $("#peer_" + peerId).remove();
        //var $el = $("#peer_" + peerId);
        //$el.remove();

        //var videos = existVideos.filter(function(e) { e.peerId == peerId });
        //if (videos.length > 0) {
        //    var index = existVideos.indexOf(videos[0]);
        //    if (index > -1) {
        //        existVideos.splice(index, 1);
        //    }
        //}
    });
})