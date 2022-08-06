<script>
    import * as signalR from "@microsoft/signalr";
import AudioBoard from "./AudioBoard.svelte";
import CharacterSheet from "./CharacterSheet.svelte";
import Documents from "./Documents.svelte";

    import Dice  from "./lib/Dice.svelte";
    import SceneChooser from "./lib/SceneChooser.svelte";
    import Stage from "./Stage.svelte";

    import { creatures, scenes, maps, stage } from "./stores";

    const hubConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();
        hubConnection.on("players", data => {
            console.log("Got players data: ", data);
        });
    
    let loader = Promise.all([
        (async () => {
            await hubConnection.start();
            await hubConnection.invoke("SendPlayers", JSON.stringify($creatures));
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
    ])    
</script>

{#await loader}
Lade
{:then}
    <SceneChooser />
    <Stage />
    <CharacterSheet />
    <Documents />
    <AudioBoard />
    <Dice />
{/await}