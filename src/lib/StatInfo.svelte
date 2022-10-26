<script>
    export let key;
    export let value;
    export let optionalValue = "";
    let edit;

    import { statsEditing } from '../stores';
</script>

{#if !$statsEditing}
    {#if value}
    <dt>{key}</dt>
    <dd>{value}</dd>
    <br />
    {/if}
{:else}
    <dt on:click={() => edit.focus()}>{key}</dt>
    <dd contenteditable bind:innerHTML={value} bind:this={edit}>
        {value || "-"}
        {#if optionalValue}
        , {optionalValue}
        {/if}
    </dd>
    <br />
{/if}


<style>
    dl { @apply text-red-800; }
    dt { @apply font-bold inline-block; }
    dd { @apply ml-2 inline-block; }
    [contenteditable] { @apply p-1; }
    :focus { @apply bg-white; }
</style>