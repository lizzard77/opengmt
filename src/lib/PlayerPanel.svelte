<script>				
    import { combat, isMaster, currentScene, currentMarker, session } from "../stores";
    import { mdiSkull } from "@mdi/js";
    import { hubConnection } from "../hub";
    import { getState, updateState } from "../session";

    import Icon from "./Icon.svelte"
	import CreatureInfo from "./CreatureInfo.svelte"
	
    function setPlayer(p)
    {
        $currentMarker = getState(p.id);
        //if ($isMaster)
        //    hubConnection.invoke("SetCurrentPlayer", JSON.stringify(p));
    }

    function updateInitiative()
    {
        console.log("updateInitiative");
        combatCreatures.sort((a, b) => b.initiative - a.initiative);
        //hubConnection.invoke("SendPlayers", JSON.stringify(combatCreatures));
        combatCreatures = combatCreatures;
    }

    function updateHP()
    {
        //hubConnection.invoke("SendPlayers", JSON.stringify(combatCreatures));
        combatCreatures = combatCreatures;
    }

    let combatCreatures = [];

    function startCombat()
    {
        updateCombattantList();
        $combat = !$combat;
        if (!$combat)
        {
            let tempc = $session.markers.filter(c => c.visible);
            tempc.forEach(c => c.initiative = -1);
            combatCreatures = tempc;
            tempc.forEach(async (c) => await updateState(c));
        }
        //hubConnection.invoke('SetCombat', $combat);
    }

    async function updateCombattantList()
    {
        if (!$session?.markers)
            return;
        let tempc = $session.markers.filter(c => c.visible);
        tempc.forEach(c => c.initiative = Math.floor(Math.random() * 20));
        tempc.sort((a, b) => b.initiative - a.initiative);
        combatCreatures = tempc;
        tempc.forEach(async (c) => await updateState(c));
    }

    $: updateCombattantList();
</script>

{#if $currentScene?.creatures?.length > 0}
    <div class="flex flex-col p-2">
        {#each $currentScene.creatures as p}
        <CreatureInfo creature={p} on:centerMapToPlayer/>
        {/each}

        <button on:click={startCombat} class="p-2 rounded-lg bg-slate-200 mt-8 mb-1">
            Kampf {$combat ? "beenden" : "starten"}
        </button>

        {#if $combat}
        <table>
            <tr>
                <th class="text-left p-1">INI</th>
                <th class="text-left p-1">NAME</th>
                <th class="text-left p-1">HP</th>
                <th class="text-left p-1">EFFEKT</th>
            </tr>
        {#each combatCreatures as p}
        <tr class="m-0 p-1" class:bg-blue-300={p.creatureId === $currentMarker.creatureId}>
            <td>
                <input class="w-12 text-center border-2" type="number" bind:value={p.initiative} on:change={updateInitiative} />
            </td>
            <td>
                <button on:click={() => setPlayer(p)} class="m-1 p-1" style="border-left: 8px solid {p.color}">
                    {#if p.hp-p.damage <= 0}
                    <del>{p.name}</del>
                    {:else}
                    {p.name}
                    {/if}
                </button>
            </td>
            <td>
                Max <input class="w-12 text-center border-2" type="number" bind:value={p.hp} on:change={updateHP} />
                Dmg <input class="w-12 text-center border-2" type="number" bind:value={p.damage} on:change={updateHP} />
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
{/if}

<style>
    button > * {
        vertical-align:middle;
    }
</style>