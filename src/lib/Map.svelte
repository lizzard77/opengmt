<script>
    export let players = [];
    export let showReach = true;
    export let player = 0;
    export let zoom = 1;
    export let map;
    export let squareSizeInPx = 0;

    let svg;
    let imageWidth = 2048;
    let imageHeight = 1536;
    let oneFoot = 1;

    $: {
        if (svg) { 
            imageWidth = map.imageWidth * zoom; 
            imageHeight = map.imageHeight * zoom;
            squareSizeInPx = imageWidth / map.widthInSquares;
            console.log(squareSizeInPx);
        }
        oneFoot = 1/map.footPerSquare;
    }

    function handleKey(e)
    {
        if (e.code === "ArrowLeft")
        {
            players[player].x = Math.max(players[player].x - 0.25,0);
            e.preventDefault();
        };
        if (e.code === "ArrowRight")
        {
            players[player].x = Math.max(players[player].x+0.25,0);
            e.preventDefault();
        };
        if (e.code === "ArrowUp")
        {
            players[player].y = Math.max(players[player].y-0.25,0);
            e.preventDefault();
        };
        if (e.code === "ArrowDown")
        {
            players[player].y = Math.max(players[player].y+0.25,0);
            e.preventDefault();
        };
    }

    function handleMouse(e)
    {
        players[player].x = e.offsetX / squareSizeInPx;
        players[player].y = e.offsetY /squareSizeInPx;
    }
</script>

<svelte:window on:keydown={handleKey}/>

<svg xmlns="http://www.w3.org/2000/svg" bind:this={svg} viewBox="0 0 {map.widthInSquares} {map.heightInSquares}" 
    on:click={handleMouse}
    style="width: {imageWidth}px; height: {imageHeight}px;">
    <image href="{map.imageUrl}" x="0" y="0" width={map.widthInSquares} height={map.heightInSquares} />
    {#each Array(map.widthInSquares) as _, i}
    <line x1="{i}" y1="0" x2="{i}" y2="{map.heightInSquares}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: 0.5;" />
    {/each}
    {#each Array(map.heightInSquares) as _, i}
    <line x1="0" y1="{i}" x2="{map.widthInSquares}" y2="{i}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: 0.5;" />
    {/each}

    {#if players[player]}
        {#if showReach}
        <circle cx="{players[player].x}" cy="{players[player].y}" r="{oneFoot*players[player].reach}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        <circle cx="{players[player].x}" cy="{players[player].y}" r="{oneFoot*5}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        {/if}
        
        {#each players as p}
        <circle cx="{p.x}" cy="{p.y}" r="{oneFoot*p.size}" style="fill:{p.color}" />
        {/each}
    {/if}
</svg>
