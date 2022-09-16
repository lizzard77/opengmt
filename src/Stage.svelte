<script>
    import { activeSection, combat, currentMarker, isMaster, squareSizeInPx, currentHandout } from "./stores";
    
    import Draggable from "./lib/Draggable.svelte";
    import PlayerPanel from "./lib/PlayerPanel.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
    import Modal from "./lib/Modal.svelte";
    import { hubConnection } from "./hub";
    import Screen from "./Screen.svelte";

    let handout = "";
    let showHandout = false;

    $activeSection = "stage";
    
    /*hubConnection.on("centerMap", (l,t) => {
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
    });*/

    hubConnection.on("setHandout", (a) => {
        if ($isMaster && !pip)
            return;
        console.log("setHandout", a);
        $currentHandout = JSON.parse(a);
    });


    //setMapCenter($currentMarker);

    $: if ($currentHandout !== handout)
    {
        handout = $currentHandout;
        showHandout = handout !== "";
    }

    const url = new URL(window.location.href);
    const pip = url.searchParams.get("pip") === "true";
    //if (pip)
    //    zoom = 0.5;
</script>

<Screen title="Karte">
    <div class="flex h-full w-screen overflow-hidden">
        <main class="flex-1 relative overflow-hidden">
            <Draggable showTools={true} />
            {#if !pip}
                <PlayerList />
            {/if}
        </main>

        {#if $isMaster && !pip}
        <div class="">
            <PlayerPanel />
        </div>
        {/if}    

        <Modal bind:isOpen={showHandout}>
            <div class="w-full p-4 bg-white">
                <img class="w-full" src={handout} alt="Handout" />
            </div>
        </Modal>
    </div>  
</Screen>