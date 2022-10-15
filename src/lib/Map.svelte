<script>
    import { currentScene, currentMarker, isMaster, squareSizeInPx, markers } from "../stores";
    import { getState, updateState } from "../session";
    import { hubConnection } from "../hub";

    export let showReach = true;
    export let zoom = 1.0;
    export let fog = false;

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
        svg.focus();
        const clickX =  e.offsetX / $squareSizeInPx;
        const clickY = e.offsetY / $squareSizeInPx;

        const anotherPlayer = $markers.filter(c => {
            const halb = (c.size * oneFoot) / 2;
            return (c.visible || $isMaster) && c.x >= clickX - halb && c.y >= clickY - halb && c.x < clickX + halb && c.y < clickY + halb;
        });
        if (anotherPlayer.length)
        {
            $currentMarker = anotherPlayer[0];
            await hubConnection.invoke("SetCurrentPlayer", JSON.stringify($currentMarker));
            return;
        }

        $currentMarker.x = clickX;
        $currentMarker.y = clickY;
        const { creatureId, x, y } = $currentMarker;

        const current = getState(creatureId);
        if (current)
        {
            current.x = x;
            current.y = y;
            console.log("send updated state", current);
            await updateState(current);
            $currentMarker = $currentMarker;
        }
    }
</script>

{#if map}
<svg xmlns="http://www.w3.org/2000/svg" bind:this={svg} viewBox="0 0 {map.widthInSquares} {map.heightInSquares}" tabindex="-1"
    on:click={click} on:keydown={handleKey} 
    style="width: {imageWidth}px; height: {imageHeight}px;">

    <image href="{$isMaster && map.imageUrlDM ? map.imageUrlDM : map.imageUrl}" x="0" y="0" width={map.widthInSquares} height={map.heightInSquares} />

    {#each Array(map.widthInSquares) as _, i}
    <line x1="{i}" y1="0" x2="{i}" y2="{map.heightInSquares}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}
    {#each Array(map.heightInSquares) as _, i}
    <line x1="0" y1="{i}" x2="{map.widthInSquares}" y2="{i}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: {map.gridOpacity||1};" />
    {/each}

    {#if $currentMarker && $currentMarker.visible}
        {#if showReach}
        <circle cx="{$currentMarker.x}" cy="{$currentMarker.y}" r="{oneFoot * ($currentMarker.size + 30)}" style="fill:transparent;stroke:rgb(255,0,0);opacity:1;stroke-width:0.01" />
        <circle cx="{$currentMarker.x}" cy="{$currentMarker.y}" r="{oneFoot * ($currentMarker.size + 5) / 2}" style="fill:white;stroke:rgb(0,255,255);opacity:0.6;stroke-width:0.01" />

        {/if}
        <circle cx="{$currentMarker.x}" cy="{$currentMarker.y}" r="{oneFoot * $currentMarker.size/2}" style="fill:transparent;stroke:rgb(0,0,255);opacity:1;stroke-width:0.05" />
    {/if}

    {#each $markers.filter(c => $isMaster || c.visible) as p}
    <circle cx="{p.x}" cy="{p.y}" r="{oneFoot*p.size/2}" style="fill:{p.color}" />
    {/each}

    <defs>
        <mask id="hole">
            {#if $isMaster}
            <rect width="{map.widthInSquares}" height="{map.widthInSquares}" fill="#777"/>
            {:else}
            <rect width="{map.widthInSquares}" height="{map.widthInSquares}" fill="white"/>
            {/if}

            {#each $markers.filter(c => c.light) as p}
                <circle r="{oneFoot * 60}" cx="{p.x}" cy="{p.y}" fill="black"/>
            {/each}

            {#if !$isMaster}
                {#each $markers.filter(c => c.light) as p}
                    <circle r="{oneFoot * 60}" cx="{p.x}" cy="{p.y}" fill="#444"/>
                {/each}
                {#each $markers.filter(c => c.light) as p}
                    <circle r="{oneFoot * 30}" cx="{p.x}" cy="{p.y}" fill="black"/>
                {/each}
            {/if}
        </mask>
    </defs>

    {#if fog}
    <rect x="0" y="0" width="{map.widthInSquares}" height="{map.widthInSquares}" style="fill: #100510;" mask="url(#hole)" />
    {/if}
</svg>
{/if}