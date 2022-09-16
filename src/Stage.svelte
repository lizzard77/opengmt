<script>
    import { activeSection, combat, currentMarker, isMaster, squareSizeInPx, currentHandout } from "./stores";
    
    import Draggable from "./lib/Draggable.svelte";
    import Map from "./lib/Map.svelte";
    import PlayerPanel from "./lib/PlayerPanel.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
    import Modal from "./lib/Modal.svelte";
    import { hubConnection } from "./hub";
    import Screen from "./Screen.svelte";
    import MapTools from "./lib/MapTools.svelte";

    let showReach = true;
    let w = 0;
    let h = 0;
    let left = 0;
    let top = 0;
    let zoom = 0.5;
    let fog = true;

    let handout = "";
    let showHandout = false;

    $activeSection = "stage";
    
    hubConnection.on("centerMap", (l,t) => {
        if ($isMaster)
            return;
        console.log("centerMap", t, l);
        left = -((l * $squareSizeInPx) - w / 2);
        top = -((t * $squareSizeInPx) - h / 2);
    });

    hubConnection.on("setFog", (a) => {
        if ($isMaster && !pip)
            return;
        console.log("setFog", a);
        fog = a;
    });

    hubConnection.on("setCombat", (a) => {
        if ($isMaster)
            return;
        console.log("setCombat", a);
        $combat = a;
    });

    hubConnection.on("setHandout", (a) => {
        if ($isMaster && !pip)
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
        zoom = 0.5;

</script>

<Screen title="Karte">
    <div class="flex h-full w-screen overflow-hidden">
        <main class="flex-1 relative overflow-hidden">
            <Draggable>
                <Map {showReach} on:centerMapToPlayer={setMapCenter} {fog} {zoom} />
            </Draggable>
            <MapTools bind:fog bind:zoom />
            {#if !pip}
                <PlayerList on:centerMapToPlayer={setMapCenter} />
            {/if}
        </main>

        {#if $isMaster && !pip}
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
</Screen>