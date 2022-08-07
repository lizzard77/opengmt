<script>
    import { hubConnection, currentScene, currentPlayer, zoom, isMaster } from "../stores";

    export let showReach = true;
    export let squareSizeInPx = 0;

    let svg;
    let map = $currentScene.map;
    let imageWidth = 2048;
    let imageHeight = 1536;
    let oneFoot = 1;

    $: {
        map = $currentScene.map;
        if (map && svg) { 
            imageWidth = map.imageWidth * $zoom; 
            imageHeight = map.imageHeight * $zoom;
            squareSizeInPx = imageWidth / map.widthInSquares;
            console.log("square size in Map", squareSizeInPx);
            oneFoot = 1/map.footPerSquare;
        }
    }

    async function handleKey(e)
    {
        let handled = false;

        if (e.code === "ArrowLeft")
        {
            $currentPlayer.x = Math.max($currentPlayer.x - 0.25,0);
            handled = true;
        };
        if (e.code === "ArrowRight")
        {
            $currentPlayer.x = Math.max($currentPlayer.x + 0.25,0);
            handled = true;
        };
        if (e.code === "ArrowUp")
        {
            $currentPlayer.y = Math.max($currentPlayer.y - 0.25,0);
            handled = true;
        };
        if (e.code === "ArrowDown")
        {
            $currentPlayer.y = Math.max($currentPlayer.y + 0.25,0);
            handled = true;
        };

        if (handled)
        {
            e.preventDefault();
            const { id, x, y } = $currentPlayer;
            // don't await
            $hubConnection.invoke("MovePlayer", JSON.stringify({ id, x, y }));
            $currentScene.creatures = $currentScene.creatures;
        }
    }

    function handleMouse(e)
    {
        $currentPlayer.x = e.offsetX / squareSizeInPx;
        $currentPlayer.y = e.offsetY /squareSizeInPx;
        const { id, x, y } = $currentPlayer;
        $hubConnection.invoke("MovePlayer", JSON.stringify({ id, x, y }));
        $currentScene.creatures = $currentScene.creatures;
    }
</script>

<svelte:window on:keydown={handleKey}/>

{#if map}
<svg xmlns="http://www.w3.org/2000/svg" bind:this={svg} viewBox="0 0 {map.widthInSquares} {map.heightInSquares}" 
    on:click={handleMouse}
    style="width: {imageWidth}px; height: {imageHeight}px;">
    <image href="{map.imageUrl}" x="0" y="0" width={map.widthInSquares} height={map.heightInSquares} />
    {#each Array(map.widthInSquares) as _, i}
    <line x1="{i}" y1="0" x2="{i}" y2="{map.heightInSquares}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}
    {#each Array(map.heightInSquares) as _, i}
    <line x1="0" y1="{i}" x2="{map.widthInSquares}" y2="{i}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}

    {#if $currentPlayer}
        {#if showReach}
        <circle cx="{$currentPlayer.x}" cy="{$currentPlayer.y}" r="{oneFoot*$currentPlayer.reach}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        <circle cx="{$currentPlayer.x}" cy="{$currentPlayer.y}" r="{oneFoot*5}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        {/if}
        
        {#each $currentScene.creatures.filter(c => $isMaster || c.visible) as p}
        <circle cx="{p.x}" cy="{p.y}" r="{oneFoot*p.size}" style="fill:{p.color}" />
        {/each}
    {/if}
</svg>
{/if}