<script>
    import { fog, hubConnection, currentPlayer, isMaster, squareSizeInPx } from "./stores";
    
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
    
    $hubConnection.on("centerMap", (l,t) => {
        if ($isMaster)
            return;
        console.log("centerMap", t, l);
        left = -((l * $squareSizeInPx) - w / 2);
        top = -((t * $squareSizeInPx) - h / 2);
    });

    $hubConnection.on("setFog", (a) => {
        if ($isMaster)
            return;
        console.log("setFog", a);
        $fog = a;
    });

    function setMapCenter(e)
    {
        console.log("setMapCenter", e.detail);
        const creature = e.detail || $currentPlayer;
        if (!creature)
            return;

        left = -((creature.x * $squareSizeInPx) - w / 2);
        top = -((creature.y* $squareSizeInPx) - h / 2);
        if (creature.visible)
            $hubConnection.invoke('CenterMap', creature.x, creature.y);
    }

</script>

<div class="flex h-screen w-screen overflow-hidden">
    <main class="flex-1 relative overflow-hidden" bind:clientWidth={w} bind:clientHeight={h}>
        <Draggable bind:left bind:top screenWidth={w} screenHeight={h}>
            <Map {showReach} on:centerMapToPlayer={setMapCenter} />
        </Draggable>
        
        <MapSettings bind:showReach />
        {#if !$isMaster}
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