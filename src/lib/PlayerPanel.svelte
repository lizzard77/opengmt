<script>
    import { createEventDispatcher } from "svelte";
    import { currentScene, currentPlayer } from "../stores";
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
    
</script>

    <div class="p-2 flex flex-col">
        {#each $currentScene.creatures as p}
        <button on:click={() => setPlayer(p)} class="m-2 p-2 rounded-lg bg-slate-200"  style="border-left: 8px solid {p.color}">{p.name} ({p.ini})</button>
        {/each}
    </div>
