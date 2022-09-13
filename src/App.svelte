<script>
    import { Router, Route } from "svelte-routing";
    import { showPIP, sounds, isMaster, creatures, scenes, maps, currentScene } from "./stores";

    import Stage from "./Stage.svelte";
    import DmDash from "./DMDash.svelte";    

    import ProgressCircle from "./lib/ProgressCircle.svelte";
    import { get } from "./api";
    
    /*hubConnection.on("players", data => {
        if ($isMaster)
            return;
        const incomingPlayerData = JSON.parse(data);
        incomingPlayerData.forEach(d => 
        {
            const player = $session.creatureStates.find(c => c.id == d.id);
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
    });*/

    /*hubConnection.on("move", data => {
        if ($isMaster)
            return;
        
        const { id, x, y } = JSON.parse(data);
        const creature = $session.creatureStates.find(c => c.id === id);
        if (creature && (creature.x !== x || creature.y !== y)) {
            console.log("move creature", creature.name, "to", x, y);
            creature.x = x;
            creature.y = y;
            $currentScene = $currentScene;
            $currentPlayer = $currentPlayer;
        }
    });*/

    /*hubConnection.on("setCurrentPlayer", data =>
    {
        if ($isMaster)
            return;
        
        const { id } = JSON.parse(data);
        const creature = $currentScene.creatures.find(c => c.id === id);
        if (creature && creature.visible) {
            console.log("set current player", creature.name);
            $currentPlayer = creature;
        }
    });*/

    let baseData = Promise.all([
        (async () => {
            let loadedCreatures = await get("/api/creatures");
            loadedCreatures.forEach(p => {
                p.x = Math.floor(Math.random() * 20);
                p.y = Math.floor(Math.random() * 15);
                p.initiative = -1;
                p.visible = false;
            });
            $creatures = loadedCreatures;
        })(),

        (async () => {
            $maps = await get("/api/maps");
        })(),

        (async () => {
            $scenes = await get("/api/scenes");
        })(),

        (async () => {
            const soundFiles = await get("/api/sounds");
            $sounds = soundFiles.map((fileName) => {
                return {
                    url : fileName,
                    isPlaying : false,
                    audioFile : null,
                    playTime : "",
                    loops : false
                }
            });
        })()

    ]);
    
    let loader = (async () => {
            await baseData;
        })();

    async function handleKey(e)
    {
        if (e.key === "m" && e.ctrlKey) {
            e.preventDefault();
            $isMaster = !$isMaster;
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
</script>

<svelte:window on:keydown={handleKey}/>

{#await loader}
    <div class="text-center mt-8">
        <ProgressCircle />
        Lade Daten...
    </div>
{:then}
<Router>
    <Route path="/" component="{Stage}" />
    <Route path="dash" component="{DmDash}" />
</Router>

{#if $currentScene && $currentScene.id && $showPIP}
<iframe src={getPIPUrl()} style="zoom: 0.4;"  class="fixed right-2 bottom-2 z-50 h-screen w-screen border-2" title="PIP" />
{/if}
{/await}

