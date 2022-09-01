<script>
    import { session, scenes, currentScene, maps, creatures, currentPlayer } from "./stores";
    import { hubConnection } from "./hub";

    async function loadScene(scene)
    {
        const s = $scenes.find(ss => ss.id === scene.id);
        s.map = $maps.find(m => m.id === s.mapId);
        s.creatures = s.creatureIds.map(c => {
            return $creatures.find(cc => cc.id === c);
        });
        $currentScene = s;
        $currentPlayer = $currentScene.creatures[0];

        const sessionBody = { 
            id: 5477,
            sceneId: s.id, 
            created : "2000-01-01", 
            creatureStates : [
                {
                    creatureId: $currentPlayer.id,
                    x: $currentPlayer.x,
                    y: $currentPlayer.y,
                    initiative: -1,
                    visible: $currentPlayer.visible
                }
            ]
        };
        $session = await putObject("/api/session", sessionBody);
        hubConnection.invoke("LoadScene", s.id);
    }
</script>

<div class="h-full w-1/3 bg-white rounded-xl p-8 relative left-1/2 -translate-x-1/2">
    {#each $scenes as scene}
    <button class="p-2 border-2 w-full mb-2" on:click={() => loadScene(scene)}><b>{scene.name}</b><br />{scene.description}</button><br />
    {/each}
</div>

