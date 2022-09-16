<script>
    import { currentScene, currentMarker, isMaster, squareSizeInPx, session } from "../stores";
    import { getState, updateState } from "../session";
    import MapTools from "./MapTools.svelte";

    export let showReach = true;
    export let zoom = 1.0;
    export let fog = true;

    let svg;
    let map = $currentScene.map;
    let imageWidth = 2048;
    let imageHeight = 1536;
    let oneFoot = 1;
    
    $: {
        map = $currentScene?.map;
        if (map && svg) { 
            imageWidth = map.imageWidth * zoom; 
            imageHeight = map.imageHeight * zoom;
            $squareSizeInPx = imageWidth / map.widthInSquares;
            oneFoot = 1/map.footPerSquare;
        }
    }

    async function handleKey(e)
    {
        let handled = false;
        
        if (e.code === "ArrowLeft")
        {
            $currentMarker.x = Math.max($currentMarker.x - 0.25,0);
            handled = true;
        };
        if (e.code === "ArrowRight")
        {
            $currentMarker.x = Math.max($currentMarker.x + 0.25,0);
            handled = true;
        };
        if (e.code === "ArrowUp")
        {
            $currentMarker.y = Math.max($currentMarker.y - 0.25,0);
            handled = true;
        };
        if (e.code === "ArrowDown")
        {
            $currentMarker.y = Math.max($currentMarker.y + 0.25,0);
            handled = true;
        };

        if (handled)
        {
            e.preventDefault();
            const { id, x, y } = $currentMarker;

            const current = getState(id);
            current.x = x;
            current.y = y;
            await updateState(current);    
            $currentMarker = $currentMarker;
        }
    }

    async function click(e)
    {
        if (!$isMaster)
            return;
    
        svg.focus();
        const clickX =  e.offsetX / $squareSizeInPx;
        const clickY = e.offsetY / $squareSizeInPx;

        const anotherPlayer = $session.markers.filter(c => {
            const halb = (c.size * oneFoot) / 2;
            return (c.visible || $isMaster) && c.x >= clickX - halb && c.y >= clickY - halb && c.x < clickX + halb && c.y < clickY + halb;
        });
        if (anotherPlayer.length)
        {
            $currentMarker = anotherPlayer[0];
            return;
        }

        $currentMarker.x = clickX;
        $currentMarker.y = clickY;
        const { creatureId, x, y } = $currentMarker;

        const current = getState(creatureId);
        current.x = x;
        current.y = y;
        console.log("send updated state", current);
        await updateState(current);
        $currentMarker = $currentMarker;
    }
</script>

{#if map}
<svg xmlns="http://www.w3.org/2000/svg" bind:this={svg} viewBox="0 0 {map.widthInSquares} {map.heightInSquares}" 
    tabindex="-1"
    on:click={click} on:keydown={handleKey} 
    style="width: {imageWidth}px; height: {imageHeight}px;">

    <image href="{$isMaster && map.imageUrlDM ? map.imageUrlDM : map.imageUrl}" x="0" y="0" width={map.widthInSquares} height={map.heightInSquares} />

    {#each Array(map.widthInSquares) as _, i}
    <line x1="{i}" y1="0" x2="{i}" y2="{map.heightInSquares}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}
    {#each Array(map.heightInSquares) as _, i}
    <line x1="0" y1="{i}" x2="{map.widthInSquares}" y2="{i}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}

    {#if $currentMarker}
        {#if showReach && $currentMarker.visible}
        <circle cx="{$currentMarker.x}" cy="{$currentMarker.y}" r="{oneFoot * $currentMarker.reach}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        <circle cx="{$currentMarker.x}" cy="{$currentMarker.y}" r="{oneFoot * 5}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        {/if}
    {/if}

    {#if $session?.markers}
        {#each $session.markers.filter(c => $isMaster || c.visible) as p}
        <circle cx="{p.x}" cy="{p.y}" r="{oneFoot*p.size}" style="fill:{p.color}" />
        {/each}
    {/if}

    <defs>
        <mask id="hole">
            <rect width="{map.widthInSquares}" height="{map.widthInSquares}" fill="white"/>
            {#if $isMaster}
            <rect width="{map.widthInSquares}" height="{map.widthInSquares}" fill="#999"/>
            {/if}
            {#if $session?.markers}
                {#each $session.markers.filter(c => c.light) as p}
                {#if $isMaster}
                <circle r="{oneFoot * 40}" cx="{p.x}" cy="{p.y}" fill="#666"/>
                <circle r="{oneFoot * 20}" cx="{p.x}" cy="{p.y}" fill="#333"/>
                {:else}
                <circle r="{oneFoot * 40}" cx="{p.x}" cy="{p.y}" fill="darkgray"/>
                <circle r="{oneFoot * 20}" cx="{p.x}" cy="{p.y}" fill="black"/>
                {/if}
            {/each}
            {/if}
        </mask>
    </defs>

    {#if fog}
    <rect x="0" y="0" width="{map.widthInSquares}" height="{map.widthInSquares}" style="fill: #100510;" mask="url(#hole)" />
    {/if}
</svg>
{/if}