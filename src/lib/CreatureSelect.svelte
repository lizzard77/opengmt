<script>
    import { creatures, currentScene } from "../stores";
    import { createEventDispatcher } from "svelte";

    const dispatch = createEventDispatcher();

    export let creatureList = $creatures.filter(c => !$currentScene.creatures.find(cc => cc.id === c.id));
    export let isOpen = false;
</script>

<h1>Hinzufügen</h1>

<ul>
    {#each creatureList as c}
    <li on:click={() => { isOpen = false; dispatch("creatureSelected", c); } }>{c.name}</li>
    {/each}
</ul>
