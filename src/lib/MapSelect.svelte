<script>
    import { maps, currentScene } from "../stores";
    import { createEventDispatcher } from "svelte";

    const dispatch = createEventDispatcher();

    export let isOpen = false;
</script>

<h1>Hinzufügen</h1>

<ul>
    {#each $maps as c}
    <li on:click={() => { isOpen = false; dispatch("mapSelected", c); } }>{c.name}</li>
    {/each}
</ul>
