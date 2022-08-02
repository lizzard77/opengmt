<script>
    import Draggable from "./lib/Draggable.svelte";
    import Dice from "./lib/Dice.svelte";
    import Map from "./lib/Map.svelte";
    import MapSettings from "./lib/MapSettings.svelte";
    import PlayerList from "./lib/PlayerList.svelte";
import MapTools from "./lib/MapTools.svelte";

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
        },
        {
            ini: 8,
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

    let player = 0;
    let w;
    let h;
    let zoom;

    $: console.log(w,h)
</script>

<svelte:window bind:innerWidth={w} bind:innerHeight={h}/>

<main class="overflow-hidden relative p-0" style="height: {h}px; width: {w}px;">
    <Draggable>
        <Map {player} {players} {footPerSquare} {showReach} {zoom} imageUrl="/src/assets/Galandrions Dorf.jpg" />
    </Draggable>
    
    <MapSettings bind:showReach bind:footPerSquare />
    <Dice />
    <PlayerList {players} bind:player />
    <MapTools bind:zoom />
</main>