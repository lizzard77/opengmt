<script>
import { navigate } from 'svelte-routing';
import { mdiArrowLeft, mdiCog, mdiDiceD20Outline } from '@mdi/js';
import { isMaster } from '../stores'; 

import Icon from "./Icon.svelte";
import Menu from "./Menu.svelte";
import Modal from './Modal.svelte';
import Scenes from '../lib/Scenes.svelte';
import Dice from '../lib/Dice.svelte';

export let backlink = "";
export let title = "OpenGMT";

let chooseScene = false;
let showDice = false;
</script>

<div class="absolute flex flex-col h-full w-full">
    <header class="w-full bg-white border-b-2 flex flex-row items-center p-2">
        <slot name="header">
            <slot name="left">
                {#if backlink}
                <button on:click={() => navigate(backlink)}><Icon path={mdiArrowLeft} /></button>
                {:else}
                Logo
                {/if}
            </slot>
            
            <slot name="center">
                <h1 class="flex-1 text-center text-2xl" on:click={() => chooseScene = true}><slot name="title">{title}</slot></h1>
            </slot>
            
            <slot name="right">
                <button on:click={() => navigate("/settings")}>
                    <Icon path={mdiCog} color="#777" />
                </button>
                <button on:click={() => showDice = true} class="ml-4">
                    <Icon path={mdiDiceD20Outline} color="#777" />
                </button>
            </slot>
        </slot>
    </header>

    <div class="md:flex flex-1 overflow-auto md:flex-row">
        {#if $isMaster}
        <div id="leftMenu" class="hidden md:block">
            <Menu />
        </div>
        {/if}
        <slot />
    </div>

    {#if $isMaster}
    <footer class="md:hidden">
        <slot name="footer">
            <Menu />
        </slot>
    </footer>
    {/if}
</div>

{#if chooseScene}
<Modal>
    <Scenes bind:isOpen={chooseScene} />
</Modal>
{/if}

{#if showDice}
<Dice bind:isOpen={showDice} />
{/if}