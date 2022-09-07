<script>
    import { currentScene, session, fog, combat, currentMarker, isMaster, squareSizeInPx, currentHandout, zoom } from "./stores";
    import { Router, Route, navigate } from "svelte-routing";

    
    import Draggable from "./lib/Draggable.svelte";
    import Map from "./lib/Map.svelte";
    import MapSettings from "./lib/MapSettings.svelte";
    import MapTools from "./lib/MapTools.svelte";
    import PlayerPanel from "./lib/PlayerPanel.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
    import Modal from "./lib/Modal.svelte";
    import GmMenu from "./lib/GMMenu.svelte";
    import { hubConnection } from "./hub";

    let showReach = true;
    let w = 0;
    let h = 0;
    let left = 0;
    let top = 0;

    let handout = "";
    let showHandout = false;

    hubConnection.on("centerMap", (l,t) => {
        if ($isMaster)
            return;
        console.log("centerMap", t, l);
        left = -((l * $squareSizeInPx) - w / 2);
        top = -((t * $squareSizeInPx) - h / 2);
    });

    hubConnection.on("setFog", (a) => {
        if ($isMaster)
            return;
        console.log("setFog", a);
        $fog = a;
    });

    hubConnection.on("setCombat", (a) => {
        if ($isMaster)
            return;
        console.log("setCombat", a);
        $combat = a;
    });

    hubConnection.on("setHandout", (a) => {
        if ($isMaster)
            return;
        console.log("setHandout", a);
        $currentHandout = JSON.parse(a);
    });

    function setMapCenter(e)
    {
        const marker = e?.detail || $currentMarker;
        if (!marker)
            return;

        console.log("setMapCenter", marker);
        left = -((marker.x * $squareSizeInPx) - w / 2);
        top = -((marker.y* $squareSizeInPx) - h / 2);
        //if (marker.visible)
        //    hubConnection.invoke('CenterMap', marker.x, marker.y);
    }

    setMapCenter($currentMarker);

    $: if ($currentHandout !== handout)
    {
        handout = $currentHandout;
        showHandout = handout !== "";
    }

    const url = new URL(window.location.href);
    const pip = url.searchParams.get("pip") === "true";
    if (pip)
        $zoom = 0.5;
</script>

{#if $isMaster}
<GmMenu />
{/if}

<div class="flex h-screen w-screen overflow-hidden">
    <main class="flex-1 relative overflow-hidden" bind:clientWidth={w} bind:clientHeight={h}>
        <Draggable bind:left bind:top screenWidth={w} screenHeight={h}>
            <Map {showReach} on:centerMapToPlayer={setMapCenter} />
        </Draggable>
        {#if !pip}
            <MapSettings bind:showReach />
            {#if !$isMaster}
            <PlayerList on:centerMapToPlayer={setMapCenter} />
            {/if}
            <MapTools on:centerMapToPlayer={setMapCenter} />
        {/if}
    </main>

    {#if $isMaster}
    <div class="">
        <PlayerPanel on:centerMapToPlayer={setMapCenter} />
    </div>
    {/if}    

    <Modal bind:isOpen={showHandout}>
        <div class="w-full p-4 bg-white">
            <img class="w-full" src={handout} alt="Handout" />
        </div>
    </Modal>
</div>  