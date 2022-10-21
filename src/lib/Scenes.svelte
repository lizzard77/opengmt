<script>
    import { scenes, creatures, currentCampaign, campaigns } from "../stores";

    export let isOpen = true;
    
    async function loadScene(scene)
    {
        $currentCampaign.currentSceneId = scene.id;
        await campaigns.save($currentCampaign);
        await currentCampaign.reload();
        isOpen = false;
    }

    async function createScene()
    {
        var newscene = {
            creatures: $creatures.filter(c => c.type === "pc"),
            description: "",
            name: "New Scene"
        };
        await scenes.create(newscene);
        $scenes = await scenes.loadAll();
    }
</script>

<div class="h-full w-full bg-white rounded-xl p-8">
    <div>
        {#await campaigns.loadAll() then _}
            {#each $campaigns as c}
                <button class="p-2 rounded-md" class:bg-slate-200={$currentCampaign.id === c.id} on:click={() => { currentCampaign.load(c.id); isOpen = false; }}>{c.name}</button>
            {/each}
        {/await}
    </div>
    <div class="mt-4 flex">
        <button class="p-2 border-2 rounded-md bg-slate-200 mr-2" on:click={() => createScene()}>Neue Szene</button>
        {#each $scenes as scene}
        <button class="p-2 border-2 rounded-md mr-2" on:click={() => loadScene(scene)}>{scene.name}</button><br />
        {/each}
    </div>
    
    <div class="text-right">
        <button class="bg-green-500 text-white p-2 rounded-md">Laden</button>
    </div>
</div>
