<script>
	import Icon from "./Icon.svelte"
					
    import { createEventDispatcher } from "svelte";
    import { isMaster, currentScene, currentPlayer, hubConnection } from "../stores";
    import { mdiEyeOff, mdiEye, mdiTarget } from "@mdi/js";

    const dispatch = createEventDispatcher();
    
    function setPlayer(p)
    {
        $currentPlayer = p;
        if ($isMaster)
            $hubConnection.invoke("SetCurrentPlayer", JSON.stringify(p));
    }

    function centerPlayer(p)
    {
        dispatch("centerMapToPlayer", p);
    }

    function toggleVisible(p)
    {
        p.visible = !p.visible;
        $hubConnection.invoke("SendPlayers", JSON.stringify([ p ]));
        $currentScene = $currentScene;
    }

    function updateInitiative()
    {
        console.log("updateInitiative");
        $currentScene.creatures.sort((a, b) => b.ini - a.ini);
        $hubConnection.invoke("SendPlayers", JSON.stringify($currentScene.creatures));
        $currentScene = $currentScene;
    }
    
</script>

    <div class="flex flex-col p-2">
        {#each $currentScene.creatures as p}
        <div class="m-0 p-1" class:bg-blue-300={p.id === $currentPlayer.id}>
            <button on:click={() => setPlayer(p)} class="p-2 w-40 rounded-lg bg-slate-200" style="border-left: 8px solid {p.color}">{p.name}</button>
            <button on:click={() => toggleVisible(p)} class="p-2 rounded-lg bg-slate-200 ">
                <Icon size={20} path={p.visible ? mdiEye :mdiEyeOff} />
            </button>
            <button on:click={() => centerPlayer(p)} class="p-2 rounded-lg bg-slate-200 ">
                <Icon size={20} path={mdiTarget} />
            </button>
            <input class="w-10 text-center" type="number" bind:value={p.ini} on:change={updateInitiative} />
        </div>
        {/each}
    </div>


    <style>
        button > * {
            vertical-align:middle;
        }
    </style>