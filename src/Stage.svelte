<script>
    import Draggable from "./lib/Draggable.svelte";
    import Map from "./lib/Map.svelte";
    import MapSettings from "./lib/MapSettings.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
    import MapTools from "./lib/MapTools.svelte";
    import { scenes, currentScene, creatures, currentPlayer } from "./stores";

    let footPerSquare = 15;
    let showReach = true;
    let w;
    let h;
    let zoom;
    let left;
    let top;
    let squareSizeInPx;
    
    function setMapCenter(e)
    {
        console.log("square size in setMapCenter", squareSizeInPx);
        if (!$currentPlayer)
            return;
        left = -(($currentPlayer.x * squareSizeInPx) - w / 2);
        top = -(($currentPlayer.y* squareSizeInPx) - h / 2);
    }
</script>

<svelte:window bind:innerWidth={w} bind:innerHeight={h}/>

<div class="absolute left-0 top-0">
<main class="overflow-hidden relative p-0" style="height: {h}px; width: {w}px;">
    <Draggable bind:left bind:top>
        <Map {showReach} {zoom} bind:squareSizeInPx />
    </Draggable>
    
    <MapSettings bind:showReach bind:footPerSquare />
    <PlayerList on:centerMapToPlayer={setMapCenter} />
    <MapTools bind:zoom on:centerMapToPlayer={setMapCenter} />
</main>
</div>