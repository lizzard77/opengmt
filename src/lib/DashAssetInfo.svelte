<script>
    import { hubConnection } from "../hub";
    import { currentHandout } from "../stores";
    import { mdiPlay, mdiStop } from "@mdi/js";
    import Icon from "../components/Icon.svelte";

    export let asset;

    function setHandout(uri)
    {
        $currentHandout = uri;
        hubConnection.invoke('SetHandout', JSON.stringify(uri));
    }
</script>

<div class="mt-1 flex">
    {#if $currentHandout === asset.uri}
    <button on:click={() => setHandout("")}><Icon path={mdiStop} size={16} /></button>
    {:else}
    <button on:click={() => setHandout(asset.uri)}><Icon path={mdiPlay} size={16} /></button>
    {/if}
    <span class="ml-2">
    {asset.name}
    </span>
</div>

<style>
    button {
        @apply ml-1 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }
</style>