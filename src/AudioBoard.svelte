<script>
    import YouTube from 'svelte-youtube';

    export let videoId = "_4OfDN6X9oc";

    let playerInstance = null;
    let playTime;

    const options = {
        height: '390',
        width: '640',
        //  see https://developers.google.com/youtube/player_parameters
        playerVars: {
            autoplay: 1
        }
    };

    function ready(event)
    {
        console.log("ready");
        playerInstance = event.detail.target;
        //playerInstance.playVideo();
        setInterval(() => {
            playTime = Math.floor(playerInstance.getCurrentTime());
            //console.log(playTime);
        }, 1000);

        
    }

    
    function play(){ playerInstance.playVideo(); }
    function pause(){ playerInstance.pauseVideo(); }
</script>

<div class="relative overflow-hidden">
    <div class="absolute -bottom-[300vh] overflow-hidden">
        <YouTube {videoId} {options} on:ready={ready} />
    </div>
</div>

<button on:click={play}>Play</button>
<button on:click={pause}>Pause</button>
{playTime}