<script>
	import Icon from "./Icon.svelte"
					
    import { createEventDispatcher } from "svelte";
    import { isMaster, currentScene, currentPlayer, hubConnection } from "../stores";
    import { mdiEyeOff, mdiEye } from "@mdi/js";

    const dispatch = createEventDispatcher();
    
    function setPlayer(p)
    {
        $currentPlayer = p;
        requestCenterMapToPlayer();
    }

    function requestCenterMapToPlayer()
    {
        dispatch("centerMapToPlayer");
    }

    function toggleVisible(p)
    {
        p.visible = !p.visible;
        if ($isMaster)
            $hubConnection.invoke("SendPlayers", JSON.stringify([ p ]));
        $currentScene = $currentScene;
    }
    
</script>

    <div class="flex flex-col">
        {#each $currentScene.creatures as p}
        <div class="m-2 p-2" class:bg-blue-300={p.id === $currentPlayer.id}>
            <button on:click={() => setPlayer(p)} class="p-2 rounded-lg bg-slate-200" style="border-left: 8px solid {p.color}">{p.name}</button>
            <button on:click={() => toggleVisible(p)} class="p-2 rounded-lg bg-slate-200 ">
                <Icon size={20} path={p.visible ? mdiEye :mdiEyeOff} />
            </button>
            {p.ini}
        </div>
        {/each}
    </div>


    <style>
        button > * {
            vertical-align:middle;
        }
    </style>