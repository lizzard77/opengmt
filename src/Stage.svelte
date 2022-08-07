<script>
    import { hubConnection, currentPlayer, isMaster } from "./stores";
    
    import Draggable from "./lib/Draggable.svelte";
    import Map from "./lib/Map.svelte";
    import MapSettings from "./lib/MapSettings.svelte";
    import MapTools from "./lib/MapTools.svelte";
    import PlayerPanel from "./lib/PlayerPanel.svelte";
    import PlayerList from "./lib/PlayerList.svelte";

    let showReach = true;
    let w;
    let h;
    export let left = 0;
    export let top = 0;
    let squareSizeInPx;
    
    $hubConnection.on("centerMap", (l,t) => {
        if ($isMaster)
            return;
        console.log("centerMap", t, l);
        left = -((l * squareSizeInPx) - w / 2);
        top = -((t * squareSizeInPx) - h / 2);
    });
    

    function setMapCenter(e)
    {
        const creature = e.detail || $currentPlayer;
        if (!creature)
            return;

        left = -((creature.x * squareSizeInPx) - w / 2);
        top = -((creature.y* squareSizeInPx) - h / 2);
        $hubConnection.invoke('CenterMap', creature.x, creature.y);
    }

    // Reihenfolge - zu früh!!!
    /*const currentPlayerChanged = zoom.subscribe(v => {
        console.log('zoom subscription')
        setMapCenter();
    });*/
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
    {#if $isMaster}
    <div class="">
        <PlayerPanel on:centerMapToPlayer={setMapCenter} />
    </div>
    {/if}
</div>  