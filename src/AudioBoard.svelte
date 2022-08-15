<script>
    import { sounds, audioFile } from "./stores";

    let playTime;
    let audioUrl;
    let playTimeTimer;

    function play(url)
    { 
        console.log(url)
        audioUrl = url;

        $audioFile = new Audio(audioUrl);
        //audioFile.currentTime= 60;
        $audioFile.play();

        playTimeTimer = setInterval(() => {
            playTime = Math.floor($audioFile.currentTime/60) + ":" + Math.floor($audioFile.currentTime%60);
        }, 1000);
    }

    function pause()
    { 
        $audioFile.pause();
        clearInterval(playTimeTimer);
    }

    function stop()
    {
        $audioFile.pause();
        $audioFile = null;
    }
</script>

{#if $audioFile}
<button on:click={pause} class="p-2 rounded-lg bg-slate-200">Pause</button>
{playTime}
{/if}

<ul>
{#each $sounds as url}
    <li><button on:click={() => play(url)} class="p-1">{url}</button></li>
{/each}
</ul>
