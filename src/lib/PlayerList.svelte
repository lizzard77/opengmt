<script>
    import { createEventDispatcher } from "svelte";
    import { scenes, currentScene, currentPlayer } from "../stores";
    import CombatAction from "./CombatAction.svelte";
    const dispatch = createEventDispatcher();
    
    let isOpen = false;
    
    function setPlayer(p)
    {
        $currentPlayer = p;
        isOpen = false;
        requestCenterMapToPlayer();
    }

    function requestCenterMapToPlayer()
    {
        dispatch("centerMapToPlayer");
    }
    
</script>

{#if isOpen}
    <div class="fixed left-0 top-0 bg-black opacity-50 z-30 h-screen w-screen" on:click={() => isOpen = false}></div>
    <div class="fixed p-2 top-0 z-50 flex portrait:flex-col">
        {#each $currentScene.creatures as p}
        <button on:click={() => setPlayer(p)} class="m-2 p-2 rounded-lg bg-slate-200" style="border-left: 8px solid {p.color}">{p.name} ({p.ini})</button>
        {/each}
    </div>
{:else}
    <div class="fixed p-2 top-0 z-10">
        <button on:click={() => isOpen = true} class="mr-2 p-2 rounded-lg bg-slate-200">{$currentPlayer.name} v</button>
        <button on:click={requestCenterMapToPlayer} class="mr-2 p-2 rounded-lg bg-slate-200">ZENTR</button>
        <CombatAction on:roll />
    </div>
{/if}