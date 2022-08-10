<script>
	import Icon from "./Icon.svelte"
					
    import { createEventDispatcher } from "svelte";
    import { combat, isMaster, currentScene, currentPlayer, hubConnection } from "../stores";
    import { mdiEyeOff, mdiEye, mdiTarget, mdiLampOutline, mdiLamp } from "@mdi/js";
    import { mdiSkull } from "@mdi/js";

    const dispatch = createEventDispatcher();
    
    function setPlayer(p)
    {
        $currentPlayer = p;
        centerPlayer(p);
        if ($isMaster)
            $hubConnection.invoke("SetCurrentPlayer", JSON.stringify(p));
    }

    function centerPlayer(p)
    {
        if ($isMaster || p.visible)            
            dispatch("centerMapToPlayer", p);
    }

    function toggleVisible(p)
    {
        p.visible = !p.visible;
        $hubConnection.invoke("SendPlayers", JSON.stringify([ p ]));
        $currentScene = $currentScene;
        updateCombattantList();
        
    }

    function toggleLight(p)
    {
        p.light = !p.light;
        if (p.light)
            p.visible = true;
        $hubConnection.invoke("SendPlayers", JSON.stringify([ p ]));
        $currentScene = $currentScene;
    }

    function updateInitiative()
    {
        console.log("updateInitiative");
        combatCreatures.sort((a, b) => b.ini - a.ini);
        $hubConnection.invoke("SendPlayers", JSON.stringify(combatCreatures));
        combatCreatures = combatCreatures;
    }

    function updateHP()
    {
        $hubConnection.invoke("SendPlayers", JSON.stringify(combatCreatures));
        combatCreatures = combatCreatures;
    }

    let combatCreatures = [];

    function startCombat()
    {
        updateCombattantList();
        $combat = !$combat;
        if (!$combat)
        {
            let tempc = $currentScene.creatures.filter(c => c.visible);
            tempc.forEach(c => c.ini = -1);
            combatCreatures = tempc;
            $hubConnection.invoke("SendPlayers", JSON.stringify(combatCreatures));
        }
    }

    function updateCombattantList()
    {
        let tempc = $currentScene.creatures.filter(c => c.visible);
        tempc.forEach(c => c.ini = Math.floor(Math.random() * 20));
        tempc.sort((a, b) => b.ini - a.ini);
        combatCreatures = tempc;
        $hubConnection.invoke("SendPlayers", JSON.stringify(combatCreatures));
    }

    $: updateCombattantList();
</script>

    <div class="flex flex-col p-2">
        {#each $currentScene.creatures as p}
        <div class="m-0 p-1" class:bg-blue-300={p.id === $currentPlayer.id} on:click={() => setPlayer(p)}>
            <button on:click={() => toggleVisible(p)} class="p-2 rounded-lg bg-slate-200 ">
                <Icon size={20} path={p.visible ? mdiEye :mdiEyeOff} />
            </button>
            <button on:click={() => toggleLight(p)} class="p-2 rounded-lg bg-slate-200 ">
                <Icon size={20} path={p.light ? mdiLamp : mdiLampOutline } />
            </button>
            <button class="p-2 w-40" style="border-left: 8px solid {p.color}">{p.name}</button>
            <button on:click={() => centerPlayer(p)} class="p-2 rounded-lg bg-slate-200 ">
                <Icon size={20} path={mdiTarget} />
            </button>
        </div>
        {/each}

        <button on:click={startCombat} class="p-2 rounded-lg bg-slate-200 mt-8 mb-1">
            Kampf {$combat ? "beenden" : "starten"}
        </button>

        {#if combat}
        <table>
            <tr>
                <th class="text-left p-1">INI</th>
                <th class="text-left p-1">NAME</th>
                <th class="text-left p-1">HP</th>
                <th class="text-left p-1">EFFEKT</th>
            </tr>
        {#each combatCreatures as p}
        <tr class="m-0 p-1" class:bg-blue-300={p.id === $currentPlayer.id}>
            <td>
                <input class="w-12 text-center border-2" type="number" bind:value={p.ini} on:change={updateInitiative} />
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


    <style>
        button > * {
            vertical-align:middle;
        }
    </style>