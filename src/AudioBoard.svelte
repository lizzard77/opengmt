<script>
    import Upload from "./lib/Upload.svelte";
    import { sounds, audioFile } from "./stores";

    let playTime;
    let playTimeTimer;

    function play(url)
    { 
        $audioFile = new Audio(url);
        //audioFile.currentTime= 60;
        $audioFile.loop = true;
        $audioFile.play();
        $audioFile.addEventListener("ended", stop);

        playTimeTimer = setInterval(() => {
            if ($audioFile)
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

<div class="flex">
<Upload folder="sounds" /><br />
{#if $audioFile}
<button on:click={pause} class="p-2 rounded-lg bg-slate-200">Pause</button>
<button on:click={stop} class="p-2 rounded-lg bg-slate-200">Stopp</button>
{playTime}
{/if}
</div>

<ul>
{#each $sounds as url}
    <li class="flex"><button on:click={() => play(url)} class="p-1">{url}</button> <input type="checkbox" />Loop</li>
{/each}
</ul>

<style>
    button {
        @apply mr-2 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }
</style>