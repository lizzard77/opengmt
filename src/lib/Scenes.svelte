<script>
    import { scenes, creatures, currentCampaign, campaigns } from "../stores";

    export let isOpen = true;

    async function loadScene(scene)
    {
        $currentCampaign.currentSceneId = scene.id;
        await campaigns.save($currentCampaign);
        await currentCampaign.reload();
        //isOpen = false;
    }

    async function createScene()
    {
        var newscene = {
            creatures: $creatures.filter(c => c.type === "pc"),
            description: "",
            name: newSceneName
        };
        await scenes.create(newscene);
        $scenes = await scenes.loadAll();
    }
    
    let newSceneName = "";
</script>

<div class="h-full w-full bg-white rounded-xl p-8">
    Neue Szene: <input bind:value={newSceneName} class="border-2 mb-2" />
    <button class="border-2 rounded-xl p-1 bg-slate-200" on:click={() => createScene()}>Neu</button>
    <br />
    {#each $scenes as scene}
    <button class="p-2 border-2 mb-2 rounded-lg" on:click={() => loadScene(scene)}>{scene.name}</button><br />
    {/each}
</div>
