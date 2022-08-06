<script>
    import { createEventDispatcher } from "svelte";
    export let zoom = 1;
    const dispatch = createEventDispatcher();
    
    function toggleZoom()
    {
        const levels = [0.5,1,1.5];
        let index = levels.indexOf(zoom);
        index++;
        if (index > levels.length-1) index = 0;
        zoom = levels[index];
        dispatch("centerMapToPlayer");
    }
</script>

<div class="fixed p-2 top-0 right-0 z-10">
    <button on:click={toggleZoom} class="p-2 rounded-lg bg-slate-200 border-4">{zoom*100}%</button>
</div>
