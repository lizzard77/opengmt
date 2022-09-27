<script>
    import { activeSection, isMaster, currentHandout } from "./stores";
    
    import Draggable from "./lib/Draggable.svelte";
    import PlayerPanel from "./lib/PlayerPanel.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
    import Modal from "./components/Modal.svelte";
    import { hubConnection } from "./hub";
    import Screen from "./components/Screen.svelte";

    $activeSection = "stage";
    
    hubConnection.on("setHandout", (a) => {
        console.log("setHandout", a);
        $currentHandout = JSON.parse(a);
    });

    let centerX = 0;
    let centerY = 0;
</script>

<Screen title="Karte">
    <div class="flex h-full w-screen overflow-hidden">
        <main class="flex-1 relative overflow-hidden">
            <Draggable showTools={true} bind:centerX bind:centerY />
        </main>
        {#if $isMaster}
        <PlayerPanel on:centerMapToPlayer={(e) => { console.log(e.detail); centerX = e.detail.x; centerY = e.detail.y; }} />
        {:else}
        <PlayerList />
        {/if}    
    </div>  
</Screen>

<Modal isOpen={$currentHandout !== ""} on:closed={() => $currentHandout = ""}>
    <div class="w-full p-4 bg-white">
        <img class="w-full" src={$currentHandout} alt="Handout" />
    </div>
</Modal>
