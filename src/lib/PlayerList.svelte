<script>
    import { createEventDispatcher } from "svelte";
    import CombatAction from "./CombatAction.svelte";
    const dispatch = createEventDispatcher();
    
    export let players = [];
    export let player = 0;

    let isOpen = false;
    let playerName = players[player].name;
    let attacks = players[player].attacks || [];

    function setPlayer(index)
    {
        player = index;
        isOpen = false;
        playerName = players[player].name;
        attacks = players[player].attacks || [];
        dispatch("centerMap", { x: players[player].x, y: players[player].y });
    }

    function requestCenterMapToPlayer()
    {
        dispatch("centerMap", { x: players[player].x, y: players[player].y });
    }
    
</script>

{#if isOpen}
    <div class="absolute left-0 top-0 bg-black opacity-50 z-30 h-screen w-screen" on:click={() => isOpen = false}></div>
    <div class="absolute p-2 top-0 z-50 flex portrait:flex-col">
        {#each players as p,i}
        <button on:click={() => setPlayer(i)} class="m-2 p-2 rounded-lg bg-slate-200">{p.name} ({p.ini})</button>
        {/each}
    </div>
{:else}
    <div class="absolute p-2 top-0 z-10">
        <button on:click={() => isOpen = true} class="mr-2 p-2 rounded-lg bg-slate-200">{playerName} v</button>
        <button on:click={requestCenterMapToPlayer} class="mr-2 p-2 rounded-lg bg-slate-200">ZENTR</button>
        <CombatAction {attacks} on:roll />
    </div>
{/if}