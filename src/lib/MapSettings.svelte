<script>
    import Modal from "./Modal.svelte";

    export let showReach = true;
    export let footPerSquare = 5;

    let isOpen = false;
</script>

<Modal bind:isOpen={isOpen}>
    <div class="h-full w-full bg-white rounded-xl p-8">
        <input type="checkbox" bind:checked={showReach} /> Reichweite anzeigen
        <br />
        <br />
        Maßstab:<br />
        <input type="number" bind:value={footPerSquare} class="w-12 border-2" /> ft./Quadrat
        <br />
        <br />
        <button class="border-2 rounded-xl p-2 m-2 bg-black text-white" on:click={() => isOpen = false}>OK</button>
    </div>
</Modal>

<div class="m-0 absolute bottom-4 left-4 flex items-center">
    <button class="border-2 rounded-xl p-2 m-2 bg-black text-white" on:click={() => isOpen = true}>Einstellungen</button>
</div>