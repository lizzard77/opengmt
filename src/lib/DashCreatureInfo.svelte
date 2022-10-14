<script>
import { mdiDeleteOutline, mdiGlasses } from "@mdi/js";
import { createEventDispatcher } from "svelte";
import Modal from "../components/Modal.svelte";
import StatBlock from "./StatBlock.svelte";
import Icon from "../components/Icon.svelte";

const dispatch = createEventDispatcher();

export let creature;
let showStats = false;
</script>

<div class="flex mt-1">
    <button on:click={() => showStats = true}><Icon path={mdiGlasses} size={16} /></button>
    <button on:click={() => dispatch("removeCreature", creature)}><Icon path={mdiDeleteOutline} size={16} /></button>
    <span class="ml-2">
    {creature.name} 
    </span>
</div>

{#if showStats}
<Modal bind:isOpen={showStats}>
    <StatBlock creature={creature} bind:isOpen={showStats} />
</Modal>
{/if}

<style>
    button {
        @apply mr-1 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }

</style>