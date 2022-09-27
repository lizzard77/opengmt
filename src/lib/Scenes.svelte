<script>
    import { session, scenes, creatures, currentScene, markers } from "../stores";
    import { get, putObject } from "../api";
    import { loadSession } from "../session";
    export let isOpen = true;

    async function loadScene(scene)
    {
        $session.sceneId = scene.id;
        await putObject("/api/session", $session);        
        isOpen = false;
    }

    async function createScene()
    {
        var newscene = {
            creatures: $creatures.filter(c => c.type === "pc"),
            description: "",
            name: newSceneName
        };
        await putObject("/api/scenes", newscene);
        $scenes = await get("/api/scenes");
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
