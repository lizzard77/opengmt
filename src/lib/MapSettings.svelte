<script>
    import { mdiCog } from "@mdi/js";
    import Icon from './Icon.svelte';
    import Modal from "./Modal.svelte";

    export let showReach = true;
    
    let isOpen = false;
</script>

<Modal bind:isOpen={isOpen}>
    <div class="h-full w-full bg-white rounded-xl p-8">
        <input type="checkbox" bind:checked={showReach} /> Reichweite anzeigen
        <br />
        <br />
        <button class="border-2 rounded-xl p-2 m-2 bg-black text-white" on:click={() => isOpen = false}>OK</button>
    </div>
</Modal>

<div class="fixed bottom-2 left-2 flex items-center">
    <button on:click={() => isOpen = true} class="mr-2 p-2 rounded-lg bg-slate-200 border-4"><Icon path={mdiCog} size=24 /></button>
</div>