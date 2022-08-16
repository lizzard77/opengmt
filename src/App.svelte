<script>
    import * as signalR from "@microsoft/signalr";
    import { Router, Route, navigate } from "svelte-routing";
    import { showPIP, sounds, isMaster, hubConnection, creatures, scenes, maps, session, currentScene, currentPlayer } from "./stores";

    import AudioBoard from "./AudioBoard.svelte";
    import CharacterSheet from "./CharacterSheet.svelte";
    import Documents from "./Documents.svelte";
    import Dice  from "./lib/Dice.svelte";
    import Stage from "./Stage.svelte";
    import Scenes from "./Scenes.svelte";
    import ProgressCircle from "./lib/ProgressCircle.svelte";
    import GmMenu from "./lib/GMMenu.svelte";
    import Start from "./Start.svelte";
    
    let left = 0;
    let top = 0;

    $hubConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();
    $hubConnection.on("players", data => {
        if ($isMaster)
            return;
        const incomingPlayerData = JSON.parse(data);
        incomingPlayerData.forEach(d => 
        {
            const player = $currentScene.creatures.find(c => c.id == d.id);
            if (player)
            {
                player.x = d.x;
                player.y = d.y;
                player.visible = d.visible;
                player.initiative = d.initiative;
                player.light = d.light;
                player.damage = d.damage;
                player.hp = d.hp;
            }
        });
        $currentScene = $currentScene;
    });

    $hubConnection.on("move", data => {
        if ($isMaster)
            return;
        
        const { id, x, y } = JSON.parse(data);
        const creature = $currentScene.creatures.find(c => c.id === id);
        if (creature && (creature.x !== x || creature.y !== y)) {
            console.log("move creature", creature.name, "to", x, y);
            creature.x = x;
            creature.y = y;
            $currentScene = $currentScene;
            $currentPlayer = $currentPlayer;
        }
    });

    $hubConnection.on("setCurrentPlayer", data =>
    {
        if ($isMaster)
            return;
        
        const { id } = JSON.parse(data);
        const creature = $currentScene.creatures.find(c => c.id === id);
        if (creature && creature.visible) {
            console.log("set current player", creature.name);
            $currentPlayer = creature;
        }
    });

    $hubConnection.on("loadScene", data => {
        if ($isMaster)
            return;
        console.log("loadScene", data);
        loadScene(data);
    });
    
    let baseData = Promise.all([
        (async () => {
            await $hubConnection.start();
        })(),

        (async () => {
            let loadedCreatures = await fetch("/api/creatures").then(r => r.json());
            loadedCreatures.forEach(p => {
                p.x = Math.floor(Math.random() * 20);
                p.y = Math.floor(Math.random() * 15);
                p.initiative = -1;
                p.visible = false;
            });
            $creatures = loadedCreatures;
            console.log("creatures", $creatures);
        })(),

        (async () => {
            $maps = await fetch("/api/maps").then(r => r.json());
            console.log("maps", $maps);
        })(),

        (async () => {
            $scenes = await fetch("/api/scenes").then(r => r.json());
            console.log("scenes", $scenes);
        })(),

        (async () => {
            $sounds = await fetch("/api/sounds").then(r => r.json());
        })()

    ]);
    
    let loader = (async () => {
            await baseData;
            $session = await fetch("/api/session").then(r => r.json());
            console.log("session", $session);
            if ($session.SceneId)
                loadScene($session.SceneId);
        })();

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

    async function handleKey(e)
    {
        if (e.key === "m" && e.ctrlKey) {
            e.preventDefault();
            $isMaster = !$isMaster;        
            console.log("set master=", $isMaster);
            if ($isMaster)
                $hubConnection.invoke("SendPlayers", JSON.stringify($currentScene.creatures));
        }
    }

    function getPIPUrl()
    {
        const url = new URL(window.location.href);
        url.pathname = "";
        url.searchParams.set("pip", "true");
        return url.toString();
    }

    const url = new URL(window.location.href);
    const pip = url.searchParams.get("pip") === "true";

    $: if ($currentScene?.id) navigate("/game");
</script>

<svelte:window on:keydown={handleKey}/>

    {#await loader}
        <div class="text-center mt-8">
            <ProgressCircle />
            Lade Daten...
        </div>
    {:then}
        <Router>
            <div>
            <Route path="sheets" component="{CharacterSheet}" />
            <Route path="docs" component="{Documents}" />
            <Route path="audio" component="{AudioBoard}" />
            <Route path="scenes" component="{Scenes}" />
            <Route path="game" component="{Stage}" />
            <Route path="/" component="{Start}" />
            </div>
            
            {#if $currentScene && $currentScene.id}
                {#if $isMaster}
                <GmMenu />
                {/if}
                {#if !pip}
                <Dice />
                {/if}
                {#if $showPIP}
                <iframe src={getPIPUrl()} style="zoom: 0.4;"  class="fixed right-2 bottom-2 z-50 h-screen w-screen border-2" title="PIP" />
                {/if}
            {/if}
        </Router>
    {/await}

