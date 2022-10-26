<script>
import { navigate } from 'svelte-routing';
import { mdiArrowLeft, mdiCog, mdiDiceD20Outline, mdiMenu } from '@mdi/js';
import { isMaster } from '../stores'; 
import { fly, fade } from 'svelte/transition';



import Icon from "./Icon.svelte";
import Menu from "./Menu.svelte";
import Modal from './Modal.svelte';
import Scenes from '../lib/Scenes.svelte';
import Dice from '../lib/Dice.svelte';

export let backlink = "";
export let title = "OpenGMT";

let editScene = false;
let showDice = false;
let showMenu = false;
</script>

<div class="absolute flex flex-col h-full w-full">
    <header class="w-full bg-white border-b-2 flex flex-row items-center p-2">
        <slot name="header">
            <slot name="left">
                {#if backlink}
                <button on:click={() => navigate(backlink)}><Icon path={mdiArrowLeft} /></button>
                {:else}
                <button on:click={() => showMenu = true}><Icon path={mdiMenu} color="#777" /></button>
                {/if}
            </slot>
            
            <slot name="center">
                <h1 class="flex-1 text-center text-2xl" on:click={() => editScene = true}><slot name="title">{title}</slot></h1>
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
        {#if $isMaster && showMenu}
            <div class="absolute left-0 top-0 bg-black opacity-70 z-30 h-screen w-screen" on:click={() => showMenu = false} in:fade="{{ duration: 200 }}" out:fade="{{ duration: 200 }}"></div>
            <div id="leftMenu" class="fixed left-0 top-0 p-2 z-50 h-screen bg-white drop-shadow-xl" in:fly="{{ x: -200, duration: 200 }}" out:fly="{{ x: -200, duration: 200 }}">
                <div class="p-1"><button on:click={() => showMenu = !showMenu}><Icon path={mdiArrowLeft} color="#777" /></button></div>
                <div class="p-2 mr-8">
                <Menu />
                </div>
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

{#if editScene}
<Modal bind:isOpen={editScene}>
    <Scenes bind:isOpen={editScene} />
</Modal>
{/if}

{#if showDice}
<Dice bind:isOpen={showDice} />
{/if}