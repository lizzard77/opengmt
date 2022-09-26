<script>
import { currentScene } from "../stores";
import { putObject } from "../api";
import { mdiDeleteOutline, mdiGlasses } from "@mdi/js";
import Modal from "../components/Modal.svelte";
import StatBlock from "./StatBlock.svelte";
import Icon from "../components/Icon.svelte";

export let creature;

let showMonsterStats = false;

function showMonsterStatBlock(c = null)
{
    showMonsterStats = true;
}

async function removeCreature(c)
{
    $currentScene.creatures = $currentScene.creatures.filter(cc => cc.id !== c.id);
    await putObject("/api/scenes", $currentScene);
    $currentScene = $currentScene;
}
</script>

<div class="flex mt-1">
    <button on:click={() => showMonsterStatBlock(creature)}><Icon path={mdiGlasses} size={16} /></button>
    <button on:click={() => removeCreature(creature)}><Icon path={mdiDeleteOutline} size={16} /></button>
    <span class="ml-2">
    {creature.name} 
    </span>
</div>

{#if showMonsterStats}
<Modal bind:isOpen={showMonsterStats}>
    <StatBlock creature={creature} bind:isOpen={showMonsterStats} />
</Modal>
{/if}

<style>
    button {
        @apply mr-1 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }

</style>