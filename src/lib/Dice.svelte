<script>
    import { mdiDiceD20Outline, mdiDiceD4, mdiDiceD6, mdiDiceD8, mdiDiceD10, mdiDiceD12, mdiDiceD20 } from "@mdi/js";

    const dicePaths = {
        W4: mdiDiceD4,
        W6: mdiDiceD6,
        W8: mdiDiceD8,
        W10: mdiDiceD10,
        W12: mdiDiceD12,
        W20: mdiDiceD20
    };

    import Icon from './Icon.svelte';
    import { dice, roll2 } from "./dice";

    let selectedDice = [];
    let rolled = false;

    function roll()
    {
        selectedDice = roll2(selectedDice);
        rolled = true;
    }

    function reset()
    {
        rolled = false;
        selectedDice = [];
        dice.forEach(d => { d.values = []; d.modifier = 0; });
        isOpen=false;
    }

    function addDice(d)
    {
        const existing = selectedDice.find(dd=>dd.name === d.name);
        if (existing)
        {
            existing.number++;
        } else {
            selectedDice = [...selectedDice, {...d, number: 1}];
        }
        selectedDice =  selectedDice;    
    }


    let isOpen = false;
</script>

{#if isOpen}
<div class="absolute left-0 top-0 bg-black opacity-50 z-30 h-screen w-screen" on:click={() => isOpen = false}></div>
<div class="m-0 absolute bottom-4 right-4 flex items-center z-50 portrait:flex-col">
    {#if !rolled}
        <div class="border-2 rounded-xl p-2 m-2 flex bg-black portrait:flex-col">
            {#each dice as d}
            <div class="border-2 m-1 bg-white rounded-full w-16 h-16 grid place-items-center" on:click={() => addDice(d)}>{d.name}</div>
            {/each}
        </div>
        {#if selectedDice.length > 0}
        <div class="border-2 rounded-xl p-2 m-2 flex bg-slate-200">
            {#each selectedDice as d}
                <div class="border-2 rounded-lg w-16 h-16 grid place-items-center">
                    <span class="flex">{d.number} <Icon path={dicePaths[d.name]} size=24 /></span>
                    <input type="number" bind:value={d.modifier} class="w-10 bg-slate-100 text-center" />
                </div>
            {/each}
            <button class="border-2 rounded-lg w-16 h-16 text-center bg-slate-200" on:click={roll}>Roll</button>
        </div>
    {/if}
    {:else}
        <div class="border-2 rounded-xl p-4 m-4 flex bg-black portrait:flex-col">
            {#each selectedDice as d}
                <div class="border-2 rounded-lg h-16 flex justify-center items-center text-white pl-2 pr-2">
                    <div class="pr-4">{d.name}</div>
                    <div class="text-2xl">
                        {#each d.values as v,i}
                        {v}
                            {#if i < d.values.length - 1}
                            +
                            {/if}
                        {/each}

                        {#if d.modifier > 0}
                            + {d.modifier}
                        {:else if d.modifier < 0}
                            {d.modifier}
                        {/if}
                    </div>
                    <div class="text-4xl"> = {d.total+d.modifier}</div>
                </div>
            {/each}
            <button class="border-2 rounded-full p-1 text-center bg-slate-200" on:click={reset}>X</button>
        </div>
    {/if}
</div>
{:else}
    <div class="fixed bottom-2 right-2 flex items-center">
        <button on:click={() => isOpen = true} class="ml-3 p-2 rounded-lg bg-slate-200 border-4 mr-2"><Icon path={mdiDiceD20Outline} size=24 /></button>
    </div>
{/if}