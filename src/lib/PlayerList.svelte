<script>
    export let players = [];
    export let player = 0;
</script>

<div class="absolute p-2 top-0 z-10">
    {#each players as p,i}
    <button on:click={() => player = i} class="p-2 rounded-lg bg-slate-200 border-4" class:border-red-400={i === player}>{p.name}</button>
    {/each}    
</div>
