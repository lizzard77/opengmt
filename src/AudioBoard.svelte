<script>
    import { sounds } from "./stores";

    let playTime;
    let audioUrl;
    let audioFile;
    let playTimeTimer;

    function play(url)
    { 
        console.log(url)
        audioUrl = url;

        audioFile = new Audio(audioUrl);
        //audioFile.currentTime= 60;
        audioFile.play();

        playTimeTimer = setInterval(() => {
            playTime = Math.floor(audioFile.currentTime/60) + ":" + Math.floor(audioFile.currentTime%60);
        }, 1000);
    }

    function pause()
    { 
        audioFile.pause();
        clearInterval(playTimeTimer);
    }
</script>

<button on:click={play}>Play</button>
<button on:click={pause}>Pause</button>

<ul>
{#each $sounds as url}
    <li><button on:click={() => play(url)}>{url}</button></li>
{/each}
</ul>
