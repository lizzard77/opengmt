<script>
    import { createEventDispatcher } from "svelte";
    import { scenes, currentScene, currentPlayer } from "../stores";
    import CombatAction from "./CombatAction.svelte";
    const dispatch = createEventDispatcher();
    
    let isOpen = true;
    let players = $currentScene.creatures
        .filter(c => c.type === "pc")
        .sort((a, b) => a.name.localeCompare(b.name));
    let npcs = $currentScene.creatures
        .filter(c => c.type !== "pc")
        .sort((a, b) => a.name.localeCompare(b.name));


    function setPlayer(p)
    {
        dispatch("centerMapToPlayer", p);
    }
</script>

{#if isOpen}
    <!--div class="fixed left-0 top-0 bg-black opacity-50 z-30 h-screen w-screen" on:click={() => isOpen = false}></div-->
    <div class="fixed p-0 top-2 left-2 z-50 flex flex-col">
        <div class="absolute left-0 top-0 w-full h-full opacity-50 bg-white -z-40"></div>
        <h2 class="bg-white p-1 text-center">Spieler</h2>
        {#each players as p}
        <button on:click={() => setPlayer(p)} class="m-2 mb-1 p-2 rounded-lg bg-slate-200" style="border-left: 8px solid {p.color}">{p.name}</button>
        {/each}
        <h2 class="bg-white p-1 text-center mt-2">NPCs</h2>
        {#each npcs as p}
        <button on:click={() => setPlayer(p)} class="m-2 mb-1 p-2 rounded-lg bg-slate-200" style="border-left: 8px solid {p.color}">{p.name}</button>
        {/each}
    </div>
{:else}
    <div class="fixed p-2 top-0 z-10">
        <button on:click={() => isOpen = true} class="mr-2 p-2 rounded-lg bg-slate-200">{$currentPlayer.name} v</button>
        <button on:click={() => console.log("do something or nothing???")} class="mr-2 p-2 rounded-lg bg-slate-200">ZENTR</button>
        <CombatAction on:roll />
    </div>
{/if}