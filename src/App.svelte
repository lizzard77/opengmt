<script>
    import Draggable from "./lib/Draggable.svelte";
    import Dice, { runRoll } from "./lib/Dice.svelte";
    import Map from "./lib/Map.svelte";
    import MapSettings from "./lib/MapSettings.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
    import MapTools from "./lib/MapTools.svelte";
    import { onMount, setContext, getContext } from "svelte";

    

    let footPerSquare = 15;
    let showReach = true;

    const players= [
        {
            ini: 12,
            color: "green",
            name: "Galandrion",
            x: 10,
            y: 10,
            type: "pc",
            reach : 10,
            size: 2,
            attacks : [
                { name : "Dolch (Nahkampf)", range: "20", mod : "+5", dmg : "1W4+3" },
                { name : "Dolch (Wurf)", range: "60", mod : "+5", dmg : "1W4+3" },
                { name : "Guiding Bolt", range: "120ft", mod : "+3", dmg : "4W6", lvl : 1, slots : 1, components : "V, S", src : "PHB 248", url : "" }
            ]
        },
        {
            ini: 5,
            color: "red",
            name: "Junger grüner Drache",
            x: 5,
            y: 5,
            type: "npc",
            reach : 30,
            size: 5
        },
        {
            ini: 8,
            color: "blue",
            name: "Ork",
            x: 8,
            y: 8,
            type: "npc",
            reach : 10,
            size: 5
        }
    ]

    onMount(() => {
        players.forEach(p => {
            p.x = Math.floor(Math.random() * 20);
            p.y = Math.floor(Math.random() * 15);
            p.ini = Math.floor(Math.random() * 20);
        });
        players.sort((a, b) => b.ini - a.ini);
        players = players;
    });
    

    let player = 0;
    let w;
    let h;
    let zoom;
    let left;
    let top;
    let squareSizeInPx;
    let diceRoller;

    function setMapCenter(e)
    {
        left = -((e.detail.x * squareSizeInPx) - w / 2);
        top = -((e.detail.y* squareSizeInPx) - h / 2);
        console.log(squareSizeInPx, left,top)
        
    }

    function roll(e)
    {
        console.log(e.detail);
        runRoll(e.detail);
    }
</script>

<svelte:window bind:innerWidth={w} bind:innerHeight={h}/>

<main class="overflow-hidden relative p-0" style="height: {h}px; width: {w}px;">
    <Draggable bind:left bind:top>
        <Map {player} {players} {footPerSquare} {showReach} {zoom} bind:squareSizeInPx imageUrl="/src/assets/Galandrions Dorf.jpg" />
    </Draggable>
    
    <MapSettings bind:showReach bind:footPerSquare />
    <Dice />
    <PlayerList {players} bind:player on:centerMap={setMapCenter} on:roll={roll} />
    <MapTools bind:zoom />
</main>