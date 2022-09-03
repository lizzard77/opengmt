<script>
    import { fog, currentScene, currentPlayer, zoom, isMaster, squareSizeInPx, session } from "../stores";
    import { createEventDispatcher } from "svelte";
    import { hubConnection } from "../hub";    
    import { getState, updateState } from "../session";

    export let showReach = true;

    const dispatch = createEventDispatcher();
    let svg;
    let map = $currentScene.map;
    let imageWidth = 2048;
    let imageHeight = 1536;
    let oneFoot = 1;
    let states = [];
    let currentPlayerState = { x: 0, y: 0, initiative: 0, reach : 0,  size: 5, color: "pink"};

    $: {
        map = $currentScene.map;
        states = $session.creatureStates;
        currentPlayerState = getState($currentPlayer.id);
        if (map && svg) { 
            imageWidth = map.imageWidth * $zoom; 
            imageHeight = map.imageHeight * $zoom;
            $squareSizeInPx = imageWidth / map.widthInSquares;
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

            const current = getState(id);
            current.x = x;
            current.y = y;
            await updateState(current);    

            // don't await
            hubConnection.invoke("MovePlayer", JSON.stringify({ id, x, y }));
            //updateState($currentPlayer);
            $currentScene.creatures = $currentScene.creatures;
        }
    }

    async function click(e)
    {
        if (!$isMaster)
            return;
        
        const clickX =  e.offsetX / $squareSizeInPx;
        const clickY = e.offsetY / $squareSizeInPx;

        const anotherPlayer = $currentScene.creatures.filter(c => {
            const halb = (c.size * oneFoot) / 2;
            return (c.visible || $isMaster) && c.x >= clickX - halb && c.y >= clickY - halb && c.x < clickX + halb && c.y < clickY + halb;
        });
        if (anotherPlayer.length)
        {
            $currentPlayer = anotherPlayer[0];
            return;
        }

        $currentPlayer.x = clickX;
        $currentPlayer.y = clickY;
        const { id, x, y } = $currentPlayer;
        hubConnection.invoke("MovePlayer", JSON.stringify({ id, x, y }));
        $currentScene.creatures = $currentScene.creatures;

        const current = getState(id);
        current.x = x;
        current.y = y;
        await updateState(current);   
        console.log("update state", current) ;

        //dispatch("centerMapToPlayer");
    }
</script>

<svelte:window on:keydown={handleKey}/>

{#if map}
<svg xmlns="http://www.w3.org/2000/svg" bind:this={svg} viewBox="0 0 {map.widthInSquares} {map.heightInSquares}" 
    on:click={click}
    style="width: {imageWidth}px; height: {imageHeight}px;">

    <image href="{$isMaster && map.imageUrlDM ? map.imageUrlDM : map.imageUrl}" x="0" y="0" width={map.widthInSquares} height={map.heightInSquares} />

    {#each Array(map.widthInSquares) as _, i}
    <line x1="{i}" y1="0" x2="{i}" y2="{map.heightInSquares}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}
    {#each Array(map.heightInSquares) as _, i}
    <line x1="0" y1="{i}" x2="{map.widthInSquares}" y2="{i}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}

    {#if currentPlayerState}
        {#if showReach && currentPlayerState.visible}
        <circle cx="{currentPlayerState.x}" cy="{currentPlayerState.y}" r="{oneFoot * currentPlayerState.reach}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        <circle cx="{currentPlayerState.x}" cy="{currentPlayerState.y}" r="{oneFoot * 5}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        {/if}
        
        {#each $session.creatureStates.filter(c => $isMaster || c.visible) as p}
        <circle cx="{p.x}" cy="{p.y}" r="{oneFoot*p.size}" style="fill:{p.color}" />
        {/each}
    {/if}

    <defs>
        <mask id="hole">
            <rect width="{map.widthInSquares}" height="{map.widthInSquares}" fill="white"/>
          {#if $isMaster}
            <rect width="{map.widthInSquares}" height="{map.widthInSquares}" fill="#999"/>
          {/if}
          {#each $currentScene.creatures.filter(c => c.light) as p}
            {#if $isMaster}
            <circle r="{oneFoot * 40}" cx="{p.x}" cy="{p.y}" fill="#666"/>
            <circle r="{oneFoot * 20}" cx="{p.x}" cy="{p.y}" fill="#333"/>
            {:else}
            <circle r="{oneFoot * 40}" cx="{p.x}" cy="{p.y}" fill="darkgray"/>
            <circle r="{oneFoot * 20}" cx="{p.x}" cy="{p.y}" fill="black"/>
            {/if}
          {/each}
        </mask>
    </defs>

    {#if $fog}
    <rect x="0" y="0" width="{map.widthInSquares}" height="{map.widthInSquares}" style="fill: #100510;" mask="url(#hole)" />
    {/if}
</svg>
{/if}