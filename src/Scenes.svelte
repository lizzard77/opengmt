<script>
    import { hubConnection, session, scenes, currentScene, maps, creatures, currentPlayer } from "./stores";
    export let isOpen = true;

    async function loadScene(scene)
    {
        const s = $scenes.find(ss => ss.id === scene.id);
        s.map = $maps.find(m => m.id === s.mapId);
        s.creatures = s.creatureIds.map(c => {
            return $creatures.find(cc => cc.id === c);
        });
        $currentScene = s;
        $currentPlayer = $currentScene.creatures[0];

        const sessionBody = { SceneId: s.id, Updated : "2000-01-01" };
        $session = await fetch("/api/session", 
            { 
                method: "PUT",
                headers: {
                    'Accept': 'application/json, text/plain',
                    'Content-Type': 'application/json;charset=UTF-8'
                }, 
                body: JSON.stringify(sessionBody)
            }).then(r => r.json());

        $hubConnection.invoke("LoadScene", s.id);
        isOpen = false;
    }
</script>

    <div class="h-full w-full bg-white rounded-xl p-8">
        {#each $scenes as scene}
        <button class="p-2 border-2" on:click={() => loadScene(scene)}>{scene.description}</button><br />
        {/each}
        <button class="border-2 rounded-xl p-2 m-2 bg-black text-white" on:click={() => isOpen = false}>Starten</button>
    </div>
