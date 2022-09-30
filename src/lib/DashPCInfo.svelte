<script>
import { currentScene, currentCampaign } from "../stores";
import { get, putObject } from "../api";
import { mdiDeleteOutline, mdiGlasses } from "@mdi/js";
import Modal from "../components/Modal.svelte";
import StatBlock from "./StatBlock.svelte";
import Icon from "../components/Icon.svelte";

export let creature;

let showMonsterStats = false;

function showPcStatBlock(c = null)
{
    showMonsterStats = true;
}

async function removeCreature(c)
{
    let creatures = $currentScene.creatures.filter(cc => cc.id !== c.id);
    const update = { ...$currentScene, creatures };
    await putObject("/api/scenes", update);
    $currentCampaign = await get("/api/campaigns/" + $currentCampaign.id);
}

function getPassive(ability)
{
    const score = creature.abilities?.hasOwnProperty(ability) ? creature.abilities[ability] : 10;
    return 10 + getModifier(ability) + creature.proficiencyBonus;
}

function getModifier(ability)
{
    const score = creature.abilities?.hasOwnProperty(ability) ? creature.abilities[ability] : 10;
    return Math.trunc((score-10)/2);
}

</script>

<div class="flex flex-row text-xs mb-4">
    <div class="flex flex-col">
        <button class="mb-1" on:click={() => showPcStatBlock(creature)}><Icon path={mdiGlasses} size={16} /></button>
        <button on:click={() => removeCreature(creature)}><Icon path={mdiDeleteOutline} size={16} /></button>
    </div>

    <div class="p-4 border-2 rounded-xl ml-2 mr-2 flex-1">
        <div class="flex mb-2">
            <div class="grow">
                <span class="text-xl font-bold">{creature.name}</span><br />
                Lvl 1 | Zwerg | Kämpfer
            </div>
            <div class="text-xs text-right mr-4 ml-4 grow">
                
            </div>
            <div class="flex flex-col text-center ">
                <div class="p-1 border-2 rounded-md flex flex-col">
                    <span class="text-xl font-bold">{creature.hpMax - creature.damage}/{creature.hpMax}</span>
                    <span class="uppercase">Treffer-<br/>punkte</span>                
                </div>
                <div class="grow border-2 rounded-md p-1 text-center sm:hidden"><span class="text-xl font-bold">{getModifier("DEX")}</span><br /><span  class="uppercase">Inititative</span></div>
            </div>
        </div>

        <div class="flex text-center">
            <div class="border-2 rounded-md mr-1 p-1"><span class="text-xl font-bold">{getPassive("WIS")}</span><br /><span  class="uppercase">Pass.<br />Wahrnehmung</span></div>
            <div class="border-2 rounded-md mr-1 p-1"><span class="text-xl font-bold">{getPassive("INT")}</span><br /><span  class="uppercase">Pass.<br />Nachf.</span></div>
            <div class="grow"></div>
            <div class="border-2 rounded-md p-1 ml-8"><span class="text-xl font-bold">{creature.armorClass}</span><br /><span  class="uppercase">Rüstungs-<br />klasse</span></div>
            <div class="hidden sm:block border-2 rounded-md p-1 ml-1"><span class="text-xl font-bold">{getModifier("DEX")}</span><br /><span  class="uppercase">Inititative</span></div>
        </div>

        <div class="mt-2">
            <b>Fertigkeiten:</b> {creature.skillsText}<br />
            <b>Eigenschaften:</b> {@html creature.specialTraits}<br />
            <b>Sinne:</b> {creature.senses}
        </div>
    </div>
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