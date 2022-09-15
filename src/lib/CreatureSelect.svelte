<script>
    import { creatures, currentScene } from "../stores";
    import { createEventDispatcher } from "svelte";

    import Screen from "../Screen.svelte";
    import Modal from "./Modal.svelte";

    const dispatch = createEventDispatcher();

    export let creatureList = $creatures.filter(c => !$currentScene.creatures.find(cc => cc.id === c.id));
    export let isOpen = false;
</script>

    <ul>
        {#each creatureList as c}
        <li on:click={() => { dispatch("creatureSelected", c); isOpen=false;} }>{c.name}</li>
        {/each}
    </ul>
