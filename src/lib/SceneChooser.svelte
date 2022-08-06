<script>
    import Modal from "./Modal.svelte";
    import { hubConnection, session, scenes, currentScene, maps, creatures, currentPlayer } from "../stores";
    export let isOpen = false;

    async function loadScene(s)
    {
        $currentScene.map = $maps.find(m => m.id === s.mapId);
        $currentScene.creatures = s.creatureIds.map(c => {
            return $creatures.find(cc => cc.id === c);
        });
        $currentPlayer = $currentScene.creatures[0];
        $currentScene = s;
        
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

<Modal bind:isOpen={isOpen}>
    <div class="h-full w-full bg-white rounded-xl p-8">
        {#each $scenes as scene}
        <button class="p-2 border-2" on:click={() => loadScene(scene)}>{scene.description}</button><br />
        {/each}
        <button class="border-2 rounded-xl p-2 m-2 bg-black text-white" on:click={() => isOpen = false}>Starten</button>
    </div>
</Modal>
<!--div class="m-0 absolute bottom-4 left-4 flex items-center">
    <button class="border-2 rounded-xl p-2 m-2 bg-black text-white" on:click={() => isOpen = true}>Szenen</button>
</div.-->