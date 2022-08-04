<script>
    import * as signalR from "@microsoft/signalr";

    import Draggable from "./lib/Draggable.svelte";
    import Dice  from "./lib/Dice.svelte";
    import Map from "./lib/Map.svelte";
    import MapSettings from "./lib/MapSettings.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
    import MapTools from "./lib/MapTools.svelte";
    import { onMount } from "svelte";

    let footPerSquare = 15;
    let showReach = true;
    let players= [];
    let maps = [];

    let player = -1;
    let map = -1;

    let w;
    let h;
    let zoom;
    let left;
    let top;
    let squareSizeInPx;

    const hubConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();
        hubConnection.on("players", data => {
            console.log("Got players data: ", data);
        });
    
    let loader = Promise.all([
        (async () => {
            await hubConnection.start();
            await hubConnection.invoke("SendPlayers", JSON.stringify(players));
        })(),

        (async () => {
            players = await fetch("/api/creatures").then(r => r.json());
            players.forEach(p => {
                p.x = Math.floor(Math.random() * 20);
                p.y = Math.floor(Math.random() * 15);
                p.ini = Math.floor(Math.random() * 20);
            });
            players.sort((a, b) => b.ini - a.ini);

            players = players;
            player = 0;
        })(),

        (async () => {
            maps = await fetch("/api/maps").then(r => r.json());
            maps = maps;
            map = 0;
        })()
    ])    
    
    onMount(async () => {

        
    });
    
    function setMapCenter(e)
    {
        left = -((e.detail.x * squareSizeInPx) - w / 2);
        top = -((e.detail.y* squareSizeInPx) - h / 2);
        console.log(squareSizeInPx, left,top)    
    }
</script>

<svelte:window bind:innerWidth={w} bind:innerHeight={h}/>

{#await loader}
Lade
{:then}
<main class="overflow-hidden relative p-0" style="height: {h}px; width: {w}px;">
    <Draggable bind:left bind:top>
        <Map {players} {player} {showReach} {zoom} bind:squareSizeInPx map={maps[map]} />
    </Draggable>
    
    <MapSettings bind:showReach bind:footPerSquare />
    <Dice />
    <PlayerList {players} bind:player on:centerMap={setMapCenter} />
    <MapTools bind:zoom />
</main>
{/await}