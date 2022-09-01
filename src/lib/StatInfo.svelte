<script>
    export let key;
    export let value;
    export let optionalValue = "";
    let edit;
</script>

<dt on:click={() => edit.focus()}>{key}</dt>
<dd contenteditable bind:innerHTML={value} bind:this={edit}>
    {value || "-"}
    {#if optionalValue}
    , {optionalValue}
    {/if}
</dd>
<br />

<style>
    dl { @apply text-red-800 }
    dt { @apply font-bold inline-block }
    dd { @apply ml-2 inline-block }
    [contenteditable] { @apply p-1 }
    :focus { @apply bg-white }
</style>