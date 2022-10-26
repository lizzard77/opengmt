<script>
    import { statsEditing, currentScene } from '../stores';
    import StatInfo from "./StatInfo.svelte";
    import { mdiClose, mdiPencil } from "@mdi/js";
    import Icon from "../components/Icon.svelte";

    export let creature = {};
    export let isOpen = false;

    let { name, hpMax, hpTemporary, armorClass, walkingSpeed, additionalSpeed,
        skillsText, damageResistances, conditionImmunities, senses, languages, 
        challenge, xp, proficiencyBonus, specialTraits, actions, abilities } = creature;

    if (!abilities)
        abilities =  { "STR": 10, "DEX" : 10, "CON" : 10, "INT" : 10, "WIS" : 10, "CHA" : 10 };

    function getModifier(v)
    {
        const mod = v ? Math.floor((v-10)/2) : 0;
        return mod > 0 ? "+" + mod : mod;
    }

    async function save()
    {
        const creatureUpdate ={ ...creature, name, hpMax, hpTemporary, armorClass, walkingSpeed, additionalSpeed,
            skillsText, damageResistances, conditionImmunities, senses, languages, 
        challenge, xp, proficiencyBonus, specialTraits, actions, abilities };

        await fetch("/api/creatures", 
            { 
                method: "PUT",
                headers: {
                    'Accept': 'application/json, text/plain',
                    'Content-Type': 'application/json;charset=UTF-8'
                }, 
                body: JSON.stringify(creatureUpdate)
            });

            const creas = $currentScene.creatures.filter(c => c.id !== creatureUpdate.id);
            $currentScene.creatures = [ ...creas, creatureUpdate ];

            isOpen = false;
    }
</script>

<div class="flex flex-col flex-wrap p-8" style="background-image: url('/assets/paper.jpg');">
    <div class="absolute right-2 top-2 flex">
    {#if !$statsEditing}
    <button class=" bg-slate-200 p-2 rounded-md" on:click={() => $statsEditing = !$statsEditing}><Icon path={mdiPencil} /></button>

    {/if}
        <button on:click={() => isOpen = false} class="rounded-md bg-slate-200 p-2 ml-2"><Icon path={mdiClose} /></button>
    </div>

    <div>
        {#if $statsEditing}
        <h1 class="font-bold text-3xl text-red-800 font-serif uppercase first-letter:text-4xl" contenteditable bind:innerHTML={name}>{name}</h1>
        {:else}
        <h1 class="font-bold text-3xl text-red-800 font-serif uppercase first-letter:text-4xl">{name}</h1>
        {/if}
        <p class="italic">Some other Info, unaligned</p>
    </div>

    <hr />
    
    <dl>
        <StatInfo key="Rüstungsklasse" bind:value={armorClass} />
        <StatInfo key="Trefferpunkte" bind:value={hpMax} />
        <StatInfo key="Geschwindigkeit" bind:value={walkingSpeed} optionalValue={additionalSpeed} />
    </dl>

    <hr />

    <table>
        <tr>
            <td><b>STÄ</b><br /><span contenteditable bind:innerHTML={abilities.STR}>{abilities.STR}</span> ({getModifier(abilities?.STR)})</td>
            <td><b>GES</b><br /><span contenteditable bind:innerHTML={abilities.DEX}>{abilities.DEX}</span> ({getModifier(abilities?.DEX)})</td>
            <td><b>KON</b><br /><span contenteditable bind:innerHTML={abilities.CON}>{abilities.CON}</span> ({getModifier(abilities?.CON)})</td>
            <td><b>INT</b><br /><span contenteditable bind:innerHTML={abilities.INT}>{abilities.INT}</span> ({getModifier(abilities?.INT)})</td>
            <td><b>WEI</b><br /><span contenteditable bind:innerHTML={abilities.WIS}>{abilities.WIS}</span> ({getModifier(abilities?.WIS)})</td>
            <td><b>CHA</b><br /><span contenteditable bind:innerHTML={abilities.CHA}>{abilities.CHA}</span> ({getModifier(abilities?.CHA)})</td>
    </table>

    <hr />

    <dl>
        <StatInfo key="Fertigkeiten" bind:value={skillsText} />
        <StatInfo key="Schadensresistenz" bind:value={damageResistances} />
        <StatInfo key="Zustandsimmunität" bind:value={conditionImmunities} />
        <StatInfo key="Sinne" bind:value={senses} />
        <StatInfo key="Sprachen" bind:value={languages} />
        <StatInfo key="Herausforderungsgrad" bind:value={challenge} />
        <StatInfo key="XP" bind:value={xp} />
    </dl>

    <hr />

    <p contenteditable bind:innerHTML={specialTraits}></p>
    
    <h2 class="font-bold text-lg text-red-800 mt-4">Aktionen</h2>
    <p contenteditable bind:innerHTML={actions}></p>
</div>

<div class="mt-4 text-right">
    <button on:click={save} class="rounded-md bg-slate-200 p-2" disabled={!$statsEditing}>Speichern</button>
</div>


<style>
    dl { @apply text-red-800; }
    dt { @apply font-bold inline-block; }
    dd { @apply ml-2 inline-block; }
    hr { @apply bg-red-800 h-1 mt-4 mb-4; }
    td { @apply text-center; }

    [contenteditable]:focus { @apply p-1; }
    :focus { @apply bg-white; }

    b { @apply text-red-800; }
</style>
