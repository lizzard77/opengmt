<script>
import { navigate } from 'svelte-routing';
import { mdiArrowLeft, mdiDotsVertical, mdiCog } from '@mdi/js';
import Icon from "./lib/Icon.svelte";
import Menu from "./lib/Menu.svelte";
import Modal from './lib/Modal.svelte';
import Scenes from './Scenes.svelte';

export let backlink = "";
export let title = "OpenGMT";

let chooseScene = false;
</script>

<div class="absolute flex flex-col h-full w-full">
    <header class="w-full bg-white border-b-2 flex flex-row items-center p-2">
        <slot name="header">
            <slot name="left">
                {#if backlink}
                <button on:click={() => navigate(backlink)}><Icon path={mdiArrowLeft} /></button>
                {:else}
                L
                {/if}
            </slot>
            
            <slot name="center">
                <h1 class="flex-1 text-center text-2xl" on:click={() => chooseScene = true}><slot name="title">{title}</slot></h1>
            </slot>
            
            <slot name="right">
                <button on:click={() => navigate("/settings")}>
                    <Icon path={mdiCog} color="#777" />
                </button>
            </slot>
        </slot>
    </header>

    <div class="md:flex flex-1 overflow-auto md:flex-row">
        <div id="leftMenu" class="hidden md:block">
            <Menu />
        </div>

        <slot />
    </div>

    <footer class="md:hidden">
        <slot name="footer">
            <Menu />
        </slot>
    </footer>
</div>

{#if chooseScene}
<Modal>
    <Scenes bind:isOpen={chooseScene} />
</Modal>
{/if}