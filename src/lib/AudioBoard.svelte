<script>
    import Upload from "./Upload.svelte";
    import { sounds } from "../stores";
    import { mdiRepeat, mdiPlay, mdiPause, mdiStop } from "@mdi/js";
    import Icon from "./Icon.svelte";

    setInterval(() => {
            $sounds.forEach(f => {
                if (f.audioFile)
                {
                    const m = "0" + Math.floor(f.audioFile.currentTime / 60);
                    const s = "0" + Math.floor(f.audioFile.currentTime % 60);
                    f.playTime = m.slice(-2) + ":" + s.slice(-2);
                }
            });
            $sounds = $sounds;
        }, 1000);

    function play(sound)
    { 
        const audioFile = new Audio(sound.url);
        audioFile.loop = sound.loops;
        audioFile.play();
        audioFile.addEventListener("ended", () => stop(sound));

        sound.audioFile = audioFile;
        sound.isPlaying = true;
        sound.playTime = "00:00";
    }

    function toggleLooping(o)
    {
        o.loops = !o.loops;
        if (o.audioFile)
            o.audioFile.loop = o.loops;
        $sounds = $sounds;
    }

    function pause(sound)
    { 
        sound.audioFile.pause();
        sound.isPlaying = !sound.isPlaying;
        $sounds = $sounds;
    }

    function resume(sound)
    { 
        sound.audioFile.play();
        sound.isPlaying = !sound.isPlaying;
        $sounds = $sounds;
    }

    function stop(sound)
    {
        sound.audioFile.pause();
        sound.isPlaying = false;
        sound.audioFile = null;
        $sounds = $sounds;
    }
</script>

<div class="flex mb-2">
<Upload folder="sounds" /><br />
</div>

<ul class="">
{#each $sounds as sound}
    <li class="flex mb-2">
        <button on:click={() => toggleLooping(sound)}><Icon path={mdiRepeat} color={sound.loops ? "green" : "silver"} /></button> 
        {#if sound.audioFile}
            {#if sound.isPlaying}
            <button class="p-2 rounded-lg bg-slate-200" on:click={() => pause(sound)}><Icon path={mdiPause} /></button>
            {:else}
            <button on:click={() => resume(sound)}><Icon path={mdiPlay} /></button>     
            {/if}

        <button class="p-2 rounded-lg bg-slate-200" on:click={() => stop(sound)}><Icon path={mdiStop} /></button>
        {sound.playTime}
        {:else}
        <button on:click={() => play(sound)}><Icon path={mdiPlay} /></button> 
        {/if}
        {sound.url.slice(sound.url.lastIndexOf('/')+1)}
        

    </li>
{/each}
</ul>

<style>
    button {
        @apply mr-2 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }
</style>