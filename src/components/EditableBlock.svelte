<script>
    import { createEventDispatcher } from "svelte";

    const dispatch = createEventDispatcher();

    export let content;
    let editing = false;
    let field;

    function save()
    {
        dispatch("contentChanged", content);
        editing = false;
    }
    $: {
        const defaultText = "<i>Klick hier zum Eingeben</i>";
        if (!content) content = defaultText;
        if (field) field.focus();
        if (field && content === defaultText) content = "";
    }
</script>

{#if !editing}
<p on:click={() => editing = true}>{@html content}</p>
{:else}
<p contenteditable bind:this={field} bind:innerHTML={content} on:blur={save} class="p-1"></p>
{/if}