<script>
    import { currentScene } from "../stores";

    let working = false;
    let files;
    let browseInput;

    let status = "";
    export let folder;

    const uploadInternal = async() => {        
        working = true;
        status = "Sende Daten";
        const formData = new FormData();
        formData.append("folder", folder);
        formData.append("sceneId", $currentScene.id.toString());
        for (let i=0; i<files.length; i++)
            formData.append('files', files[i]);

        status = await fetch("/api/upload", {
          method: 'POST',
          body: formData
        }).then(r => r.text());

        working = false;
    }
    
    $: { 
        if (files && files.length) {
            uploadInternal();
        }
    };

</script>

{#if (!working)}
<input bind:this={browseInput} type="file" multiple bind:files style="display: none;" />
<button on:click={() => browseInput.click()}>Dateien hochladen</button>
{:else}
<progress />
{/if}

<p>{status}</p>

<style>
    button {
        @apply mr-2 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }
</style>