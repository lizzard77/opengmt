<script>
    import * as signalR from "@microsoft/signalr";
    import { Router, Link, Route } from "svelte-routing";
    import { hubConnection, creatures, scenes, maps, session, currentScene, currentPlayer } from "./stores";

    import AudioBoard from "./AudioBoard.svelte";
    import CharacterSheet from "./CharacterSheet.svelte";
    import Documents from "./Documents.svelte";
    import Dice  from "./lib/Dice.svelte";
    import SceneChooser from "./lib/SceneChooser.svelte";
    import Stage from "./Stage.svelte";
    
    let isMaster = false;

    $hubConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();
    $hubConnection.on("players", data => {
        console.log("players", data);
    });
    $hubConnection.on("move", data => {
        const { id, x, y } = JSON.parse(data);
        const creature = $currentScene.creatures.find(c => c.id === id);
        if (creature && (creature.x !== x || creature.y !== y)) {
            console.log("move creature", creature.name, "to", x, y);
            creature.x = x;
            creature.y = y;
            
            $currentScene = $currentScene
            $currentPlayer = $currentPlayer
        }
    });
    $hubConnection.on("loadScene", data => {
        console.log("loadScene", data);
        loadScene(data);
    });

    
    
    let baseData = Promise.all([
        (async () => {
            await $hubConnection.start();
            //await hubConnection.invoke("SendPlayers", JSON.stringify($creatures));
        })(),

        (async () => {
            let loadedCreatures = await fetch("/api/creatures").then(r => r.json());
            loadedCreatures.forEach(p => {
                p.x = Math.floor(Math.random() * 20);
                p.y = Math.floor(Math.random() * 15);
                p.ini = Math.floor(Math.random() * 20);
            });
            loadedCreatures.sort((a, b) => b.ini - a.ini);
            $creatures = loadedCreatures;
        })(),

        (async () => {
            $maps = await fetch("/api/maps").then(r => r.json());
        })(),

        (async () => {
            $scenes = await fetch("/api/scenes").then(r => r.json());
        })()
    ]);
    
    let loader = (async () => {
            await baseData;
            $session = await fetch("/api/session").then(r => r.json());
            if ($session.SceneId)
                loadScene($session.SceneId);
        })();

    let showSceneChooser = false;

    function loadScene(id)
    {
        const s = $scenes.find(s => s.id === id);
        s.map = $maps.find(m => m.id === s.mapId);
        s.creatures = s.creatureIds.map(c => {
            return $creatures.find(cc => cc.id === c);
        });
        $currentScene = s;
        $currentPlayer = $currentScene.creatures[0];
    }
</script>

{#await loader}
    <div class="text-center">
        <progress /><br />
        Lade Daten...
    </div>
{:then}
    
    <input type="checkbox" bind:value={isMaster} /> Master
    <button on:click={() => showSceneChooser = true}>Szene auswählen</button>

    {#if $currentScene.id}
        <Router>
            {#if isMaster}
            <nav>
                <Link to="/">Stage</Link>
                <a href="/" target="_new">Stage (neues Fenster)</a>
                <Link to="sheets">Sheets</Link>
                <Link to="docs">Dokumente</Link>
                <Link to="audio">Audio</Link>
                
            </nav>
            {/if}
            <div>
                <Route path="sheets" component="{CharacterSheet}" />
                <Route path="docs" component="{Documents}" />
                <Route path="audio" component="{AudioBoard}" />
                <Route path="/"><Stage /></Route>
            </div>
        </Router>
        <Dice />
    {:else}
        <div class="text-center">
            <progress /><br />
            Warte auf Spielstart...
        </div>
    {/if}
    <SceneChooser bind:isOpen={showSceneChooser} />    
{/await}