<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CallCitas.aspx.cs" Inherits="CasoMedico_AWebSDK_CallCitas" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
<%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>

<meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="ie=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <title>Video Llamada</title>
    <%--<link rel="stylesheet" href="./assets/common.css" />--%>
    <link href="assets/mate.css" rel="stylesheet" />
    <link href="assets/style.css" rel="stylesheet" />

</head>
<body class="agora-theme">
    <form id="form" class="row col l12 s12" runat="server">
        <div class="row container col l12 s12">
            <div class="col" style="min-width: 433px; max-width: 443px">
                <div class="card" style="margin-top: 0px; margin-bottom: 0px;">
                    <div class="row card-content" style="margin-bottom: 0px;">
                        <div class="row" style="margin: 0">
                            <div class="col s12">
                                <button class="btn btn-raised btn-primary waves-effect waves-light" id="join">Unirte</button>
                                <button class="btn btn-raised btn-primary waves-effect waves-light" id="leave">Salir</button>
                                </div>
                        </div>
                    </div>
                </div>
              
            </div>
            <div class="col s7">
                <div class="video-grid" id="video">
                    <div class="video-view">
                        <div id="local_stream" class="video-placeholder"></div>
                        <div id="local_video_info" class="video-profile hide"></div>
                        <div id="video_autoplay_local" class="autoplay-fallback hide"></div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="appid" runat="server" />
        <asp:HiddenField ID="appcertificate" runat="server" />
         <asp:HiddenField ID="channel" runat="server" />
         <asp:HiddenField ID="token" runat="server" />
    </form>
    
   <script src="vendor/jquery.min.js"></script>
   <script src="vendor/materialize.min.js"></script>
   <script src="AgoraRTCSDK-3.6.10.js"></script>
    <script>
      // rtc object
        var rtc = {
            client: null,
            joined: false,
            published: false,
            localStream: null,
            remoteStreams: [],
            params: {}
        };
        var _appid = document.getElementById('<%= appid.ClientID %>').value;
        var _channel = document.getElementById('<%= channel.ClientID %>').value;
        var _token = document.getElementById('<%= token.ClientID %>').value;

        // Options for joining a channel
        var options = {
            appID: _appid,
            channel: _channel,
            uid: null,
            token: _token
        }

        function Toastify(options) {
            M.toast({ html: options.text, classes: options.classes })
        }

        var Toast = {
            info: (msg) => {
                Toastify({
                    text: msg,
                    classes: "info-toast"
                })
            },
            notice: (msg) => {
                Toastify({
                    text: msg,
                    classes: "notice-toast"
                })
            },
            error: (msg) => {
                Toastify({
                    text: msg,
                    classes: "error-toast"
                })
            }
        }

    function addView (id, show) {
      if (!$("#" + id)[0]) {
        $("<div/>", {
          id: "remote_video_panel_" + id,
          class: "video-view",
        }).appendTo("#video")

        $("<div/>", {
          id: "remote_video_" + id,
          class: "video-placeholder",
        }).appendTo("#remote_video_panel_" + id)

        $("<div/>", {
          id: "remote_video_info_" + id,
          class: "video-profile " + (show ? "" :  "hide"),
        }).appendTo("#remote_video_panel_" + id)

        $("<div/>", {
          id: "video_autoplay_"+ id,
          class: "autoplay-fallback hide",
        }).appendTo("#remote_video_panel_" + id)
      }
    }
    function removeView (id) {
      if ($("#remote_video_panel_" + id)[0]) {
        $("#remote_video_panel_"+id).remove()
      }
    }

    function getDevices (next) {
      AgoraRTC.getDevices(function (items) {
        items.filter(function (item) {
          return ["audioinput", "videoinput"].indexOf(item.kind) !== -1
        })
        .map(function (item) {
          return {
          name: item.label,
          value: item.deviceId,
          kind: item.kind,
          }
        })
        var videos = []
        var audios = []
        for (var i = 0; i < items.length; i++) {
          var item = items[i]
          if ("videoinput" == item.kind) {
            var name = item.label
            var value = item.deviceId
            if (!name) {
              name = "camera-" + videos.length
            }
            videos.push({
              name: name,
              value: value,
              kind: item.kind
            })
          }
          if ("audioinput" == item.kind) {
            var name = item.label
            var value = item.deviceId
            if (!name) {
              name = "microphone-" + audios.length
            }
            audios.push({
              name: name,
              value: value,
              kind: item.kind
            })
          }
        }
        next({videos: videos, audios: audios})
      })
    }

 
    function handleEvents (rtc) {
      // Occurs when an error message is reported and requires error handling.
      rtc.client.on("error", (err) => {
        console.log(err)
      })
        // Ocurre cuando el usuario del mismo nivel deja el canal; 
        //por ejemplo, el usuario del mismo nivel llama a Client.leave.
      rtc.client.on("peer-leave", function (evt) {
        var id = evt.uid;
        console.log("id", evt)
        let streams = rtc.remoteStreams.filter(e => id !== e.getId())
        let peerStream = rtc.remoteStreams.find(e => id === e.getId())
        if(peerStream && peerStream.isPlaying()) {
          peerStream.stop()
        }
        rtc.remoteStreams = streams
        if (id !== rtc.params.uid) {
          removeView(id)
        }
        Toast.notice("peer leave")
        //console.log("peer-leave", id)
      })
      // Ocurre cuando se publica la transmisión local.
      rtc.client.on("stream-published", function (evt) {
        Toast.notice("stream published success")
        //console.log("stream-published")
      })
      // Occurs when the remote stream is added.
      rtc.client.on("stream-added", function (evt) {
        var remoteStream = evt.stream
        var id = remoteStream.getId()
        Toast.info("stream-added uid: " + id)
        if (id !== rtc.params.uid) {
            rtc.client.subscribe(remoteStream, function (err) {
                Toast.error("stream subscribe failed");
            console.log("stream subscribe failed", err)
          })
        }
        console.log("stream-added remote-uid: ", id)
      })
      // Occurs when a user subscribes to a remote stream.
      rtc.client.on("stream-subscribed", function (evt) {
        var remoteStream = evt.stream
        var id = remoteStream.getId()
        rtc.remoteStreams.push(remoteStream)
        addView(id)
        remoteStream.play("remote_video_" + id)
        Toast.info("stream-subscribed remote-uid: " + id)
        console.log("stream-subscribed remote-uid: ", id)
      })
      // Occurs when the remote stream is removed; for example, a peer user calls Client.unpublish.
      rtc.client.on("stream-removed", function (evt) {
        var remoteStream = evt.stream
        var id = remoteStream.getId()
        Toast.info("stream-removed uid: " + id)
        if(remoteStream.isPlaying()) {
          remoteStream.stop()
        }
        rtc.remoteStreams = rtc.remoteStreams.filter(function (stream) {
          return stream.getId() !== id
        })
        removeView(id)
        console.log("stream-removed remote-uid: ", id)
      })
      rtc.client.on("onTokenPrivilegeWillExpire", function(){
        // After requesting a new token
        // rtc.client.renewToken(token)
        console.log("onTokenPrivilegeWillExpire")
      })
      rtc.client.on("onTokenPrivilegeDidExpire", function(){
        // After requesting a new token
        // client.renewToken(token);
        console.log("onTokenPrivilegeDidExpire")
      })
    }

    
    function join (rtc, option) {
      if (rtc.joined) {
          Toast.error("Ya iniciaste la video llamada");
        return;
      }
      rtc.client = AgoraRTC.createClient({ mode: "rtc", codec: "h264" })

      rtc.params = option

      // handle AgoraRTC client event
      handleEvents(rtc)

      // init client

      rtc.client.init(option.appID, function () {
        console.log("init success")                  

        rtc.client.join(option.token ? option.token : null, option.channel, option.uid ? +option.uid : null, function (uid) {
          Toast.notice("Ingresando a videollamada: " + option.channel + " success, uid: " + uid)
          //console.log("join channel: " + option.channel + " success, uid: " + uid)
          rtc.joined = true

          rtc.params.uid = uid

          // create local stream
          rtc.localStream = AgoraRTC.createStream({
            streamID: rtc.params.uid,
            audio: true,
            video: true,
            screen: false,
            microphoneId: option.microphoneId,
            cameraId: option.cameraId
          })

            // inicializar flujo local. La función de devolución de llamada se ejecuta 
            // después de que se realiza la inicialización

          rtc.localStream.init(function () {
            console.log("init local stream success")
            // play stream with html element id "local_stream"
            rtc.localStream.play("local_stream")

            // publish local stream
            publish(rtc)
          }, function (err)  {
            Toast.error("stream init failed, please open console see more detail 1")
            console.error("init local stream failed ", err)
          })
        }, function(err) {
          Toast.error("client join failed, please open console see more detail 1")
          console.error("client join failed", err)
        })
      }, (err) => {
        Toast.error("client init failed, please open console see more detail 2")
        console.error(err)
      })
    }

    

    function leave (rtc) {
      if (!rtc.client) {
        Toast.error("¡Únete Primero!")
        return
      }
      if (!rtc.joined) {
        Toast.error("No te encuentras en la videollamada")
        return
      }
        /** Este método permite que un usuario abandone un canal **/

      rtc.client.leave(function () {
        // stop stream
        if(rtc.localStream.isPlaying()) {
          rtc.localStream.stop()
        }
        // close stream
        rtc.localStream.close()
        for (let i = 0; i < rtc.remoteStreams.length; i++) {
          var stream = rtc.remoteStreams.shift()
          var id = stream.getId()
          if(stream.isPlaying()) {
            stream.stop()
          }
          removeView(id)
        }
        rtc.localStream = null
        rtc.remoteStreams = []
        rtc.client = null
        console.log("client leaves channel success")
        rtc.published = false
        rtc.joined = false
        //Toast.notice("leave success")
      }, function (err) {
        console.log("channel leave failed")
        //Toast.error("leave success")
        console.error(err)
      })
    }

    // Esta función se ejecuta automáticamente cuando un documento está listo.
    $(function () {
      // Esto buscará todos los dispositivos y completará la interfaz de usuario para cada dispositivo. (Audio y video)
      getDevices(function (devices) {
        devices.audios.forEach(function (audio) {
          $("<option/>", {
            value: audio.value,
            text: audio.name,
          }).appendTo("#microphoneId")
        })
        devices.videos.forEach(function (video) {
          $("<option/>", {
            value: video.value,
            text: video.name,
          }).appendTo("#cameraId")
        })
        // 
        //resolutions.forEach(function (resolution) {
        //  $("<option/>", {
        //    value: resolution.value,
        //    text: resolution.name
        //  }).appendTo("#cameraResolution")
        //})
        M.AutoInit()
      })

  

      // This will start the join functions with all the configuration selected by the user.
      $("#join").on("click", function (e) {
        console.log("join")
          e.preventDefault();
          join(rtc, options)
      })
    
      // Leeaves the chanenl if someone clicks the leave button
      $("#leave").on("click", function (e) {
        console.log("leave")
        e.preventDefault()
          leave(rtc)
      })

    })
    </script>
</body>
</html>
