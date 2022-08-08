<script>
    import YouTube from "./lib/YouTube.svelte";
    export let videoId = "_4OfDN6X9oc";
    let player;

    function ready()
    {
        console.log("ready");
        player.seek(60);
        player.play();
    }
</script>

<YouTube {videoId} bind:this={player} on:Ready={ready} />
<button on:click={player.play}>Play</button>
<button on:click={player.pause}>Pause</button>
