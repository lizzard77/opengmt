<script>
    import { sounds, currentScene, soundPlayers } from "../stores";
    import { mdiRepeat, mdiPlay, mdiPause, mdiStop } from "@mdi/js";
    import Icon from "../components/Icon.svelte";

    function play(sound)
    { 
        soundPlayers.add(sound);
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

    setInterval(() => $soundPlayers = $soundPlayers, 500);
</script>

<ul class="">
    {#each $soundPlayers as player}
    <div class="flex mb-2">
        {#if player.isPlaying()}
        <button class="p-2 rounded-md bg-slate-200" on:click={() => player.toggle()}><Icon path={mdiPause} size={16} /></button>
        {:else}
        <button class="p-2 rounded-md bg-slate-200" on:click={() => player.toggle()}><Icon path={mdiPlay} size={16} /></button>
        {/if}
        {player.name}
        {player.playTime}
    </div>
    {/each}
{#each $sounds as sound}
    <li class="flex mb-2">
        <button on:click={() => toggleLooping(sound)}><Icon path={mdiRepeat} color={sound.loops ? "green" : "silver"} size={16} /></button> 
        {#if sound.audioFile}
            {#if sound.isPlaying}
            <button class="p-2 rounded-md bg-slate-200" on:click={() => pause(sound)}><Icon path={mdiPause} size={16} /></button>
            {:else}
            <button on:click={() => resume(sound)}><Icon path={mdiPlay} size={16} /></button>     
            {/if}

            <button class="p-2 rounded-md bg-slate-200" on:click={() => stop(sound)}><Icon path={mdiStop} size={16} /></button>
            {sound.playTime}
        {:else}
        <button on:click={() => play(sound)}><Icon path={mdiPlay} size={16} /></button> 
        {/if}

        <span class="ml-2">{sound.name}</span>
    </li>
{/each}
</ul>

<style>
    button {
        @apply ml-1 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }
</style>