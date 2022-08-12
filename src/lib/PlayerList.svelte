<script>
    import { createEventDispatcher } from "svelte";
    import { currentScene, currentPlayer } from "../stores";
    import { mdiSkull } from "@mdi/js";
    import CombatAction from "./CombatAction.svelte";
    import Icon from "./Icon.svelte";
    const dispatch = createEventDispatcher();
    
    let players = [];

    function refreshLists(sc)
    {
        if (sc)
        {
            sc.sort((a, b) => b.ini - a.ini);
            players = sc.filter(c => c.visible);
        }
    }

    $: refreshLists($currentScene.creatures);

    function setPlayer(p)
    {
        dispatch("centerMapToPlayer", p);
    }
</script>

<div class="fixed p-0 top-2 left-2 z-50 flex flex-col">
    <div class="absolute left-0 top-0 w-full h-full opacity-70 bg-white -z-40"></div>
    {#if players.length}
    <table class="m-2">
        <tr>
            <th class="text-left p-1">INI</th>
            <th class="text-left p-1">NAME</th>
            <th class="text-left p-1">HP</th>
            <th class="text-left p-1">EFFEKT</th>
        </tr>
        {#each players as p}
        <tr>
            <td class="p-1 text-right">
                {#if p.ini >= 0} 
                {p.ini}
                {/if}
            </td>
            <td class="p-1">
                <button on:click={() => setPlayer(p)} class="m-1 p-1" style="border-left: 8px solid {p.color}">
                    {#if p.hp-p.damage <= 0}
                    <del>{p.name}</del>
                    {:else}
                    {p.name}
                    {/if}
                </button>
            </td>
            <td class="p-1">
                {#if p.type == "pc"}
                {p.hp - p.damage}/{p.hp}
                {/if}
            </td>
            <td>
                {#if p.hp-p.damage <= 0}
                <Icon path={mdiSkull} color="red" />
                {/if}
            </td>
        </tr>
        {/each}

    </table>
    {/if}
</div>