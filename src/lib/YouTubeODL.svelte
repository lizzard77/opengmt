<script context="module">
    let iframeApiReady = false;

    /*
    window.onYouTubeIframeAPIReady = () =>
    {
        console.log("3")    
        window.dispatchEvent(new Event("iframeApiReady"));
    }*/
    
    window.addEventListener("iframeApiReady", function (e) {
        console.log("4");
        iframeApiReady = true;
    });

    var tag = document.createElement("script");
    tag.src = "https://www.youtube.com/iframe_api";
    var firstScriptTag = document.getElementsByTagName("script")[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
</script>

<svelte:window on:onYouTubeIframeAPIReady={() => console.log("ytfar")}/>

<script>
    import { createEventDispatcher } from "svelte";
    import { afterUpdate } from "svelte";
    const dispatch = createEventDispatcher();

    export let videoId;
    export let playTime;

    let player;
    let divId = "player_opengmt_1";

    export function play() 
    {
        player.playVideo();
    }

    export function pause() 
    {
        player.pauseVideo();
    }

    export function seek(pos)
    {
        player.seekTo(pos, true);
    }


    afterUpdate(() => {
        console.log("3");
        if (iframeApiReady) {
            console.log("33");
            player = new YT.Player(divId, {
                    height: "400",
                    width: "300",
                    videoId,
                    events: {
                        onReady: playerIsReady,
                        onStateChange: playerStateChange,
                    },
                });
            }
        });

    function playerStateChange({ data }) 
    {
        dispatch("PlayerStateChange", data);
        //console.log(data);
        let strReturn = "";
        if (data == -1) {
            strReturn = "(unstarted)";
        }
        if (data == 0) {
            strReturn = "(ended)";
        }
        if (data == 1) {
            strReturn = "(playing)";
            //setInterval(() => {
            //    playTime = player.getCurrentTime();
            //playTime = !player?.getCurrentTime ? 0.0 : player.getCurrentTime();
            //}, 1000);
        }
        if (data == 2) {
            strReturn = "(paused)";
        }
        if (data == 3) {
            strReturn = "(buffering)";
        }
        if (data == 5) {
            strReturn = "(video cued).";
        }
        dispatch("PlayerStateChangeString", strReturn);
    }

    function playerIsReady() 
    {
        dispatch("Ready");
        
        
    }
</script>


<div id="player_opengmt_1" class="absolute right-2 top-2"></div>