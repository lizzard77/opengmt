<script>
    import { currentPlayer, zoom } from "./stores";
    
    import Draggable from "./lib/Draggable.svelte";
    import Map from "./lib/Map.svelte";
    import MapSettings from "./lib/MapSettings.svelte";
    import MapTools from "./lib/MapTools.svelte";
    import PlayerPanel from "./lib/PlayerPanel.svelte";
    import PlayerList from "./lib/PlayerList.svelte";

    let showReach = true;
    let w;
    let h;
    let left;
    let top;
    let squareSizeInPx;
    export let isMaster;
    
    function setMapCenter(e)
    {
        console.log("square size in setMapCenter", squareSizeInPx);
        if (!$currentPlayer)
            return;
        left = -(($currentPlayer.x * squareSizeInPx) - w / 2);
        top = -(($currentPlayer.y* squareSizeInPx) - h / 2);
    }

    // Reihenfolge - zu früh!!!
    const currentPlayerChanged = zoom.subscribe(v => {
        console.log('zoom subscription')
        setMapCenter();
    });
</script>

<div class="flex h-screen w-screen overflow-hidden">
    <main class="flex-1 relative overflow-hidden" bind:clientWidth={w} bind:clientHeight={h}>
        <Draggable bind:left bind:top screenWidth={w} screenHeight={h}>
            <Map {showReach} bind:squareSizeInPx />
        </Draggable>
        
        <MapSettings bind:showReach />
        {#if !isMaster}
        <PlayerList on:centerMapToPlayer={setMapCenter} />
        {/if}
        <MapTools on:centerMapToPlayer={setMapCenter} />
    </main>
    {#if isMaster}
    <div class="w-1/5">
        <PlayerPanel on:centerMapToPlayer={setMapCenter} />
    </div>
    {/if}
</div>  