<script>
    import { activeSection } from '../stores';
    import { navigate } from "svelte-routing";
    import { createEventDispatcher } from "svelte";
    import Icon from '../components/Icon.svelte';

    const dispatch = createEventDispatcher();
    
    export let icon = null;
    export let caption = "";
    export let sectionName = '';
    export let url = null;

    function go()
    {
        if (url)
        {
            $activeSection = sectionName; 
            navigate(url);
        }
        dispatch("click");
    }
</script>

<button class="navButton flex-1 p-2 bg-transparent border-0 text-gray-500 flex flex-col items-center md:p-1 md:flex-row {$activeSection === sectionName ? 'activeSection' : ''}" on:click={go}>
    {#if icon}
    <Icon path={icon} class="h-8 w-8 md:h-4 md:w-4" />
    {/if}
    <span class="md:ml-2">{caption}</span>
</button>

<style>
    :global(.navButton:hover) {
        background-color: #eee;
    }

    :global(.activeSection) {
        @apply border-slate-400 border-t-2 md:border-l-2 md:border-t-0 font-bold;
    }
</style>

