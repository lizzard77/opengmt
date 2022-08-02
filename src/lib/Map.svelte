<script>
    export let players = [];
    export let footPerSquare = 10;
    export let showReach = true;
    export let player = 0;
    export let imageUrl = "";

    let imageWidthBase = 2048;
    let imageHeightBase = 1536;

    export let imageWidth = 2048;
    export let imageHeight = 1536;
    export let zoom = 1;

    let svg;
    export let width = 20;
    let height = 15;
    let oneFoot = 1;
    
    let squareSizeInPx = 0;
    //$: { squareSizeInPx = svg?.clientWidth||1 / width; console.log("square Size in Px", squareSizeInPx); }
            
    $: {
        if (svg) { 
            /*if (!imageHeightBase || !imageWidthBase)
            {
                imageHeightBase = svg.clientWidth;
                imageWidthBase = svg.clientHeight;
            }*/
            imageWidth = imageWidthBase * zoom; 
            imageHeight = imageHeightBase * zoom;
            //height = Math.floor(svg.clientHeight / squareSizeInPx); 
            squareSizeInPx = imageWidth / width;
        }
        oneFoot = 1/footPerSquare;
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
        //console.log(e)
        players[player].x = e.offsetX / squareSizeInPx; // Math.max((e.offsetX)/squareSizeInPx, 0);
        players[player].y = e.offsetY /squareSizeInPx; // Math.max((e.offsetX)/squareSizeInPx, 0);
        //console.log(players[player]);
        
    }
</script>

<svelte:window on:keydown={handleKey}/>

<div class="absolute left-0 -top-0 m-0">
    <svg xmlns="http://www.w3.org/2000/svg" bind:this={svg} viewBox="0 0 {width} {height}" 
        on:click={handleMouse}
        style="width: {imageWidth}px; height: {imageHeight}px;">
        <image href="{imageUrl}" x="0" y="0" width="{width}" height="{height}" />
        {#each Array(width) as _, i}
        <line x1="{i}" y1="0" x2="{i}" y2="{height}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: 0.3;" />
        {/each}
        {#each Array(height) as _, i}
        <line x1="0" y1="{i}" x2="{width}" y2="{i}" style="stroke:rgb(255,255,255);stroke-width:0.01;opacity: 0.3;" />
        {/each}

        {#if showReach}
        <circle cx="{players[player].x}" cy="{players[player].y}" r="{oneFoot*players[player].reach}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        <circle cx="{players[player].x}" cy="{players[player].y}" r="{oneFoot*5}" style="fill:white;stroke:rgb(100,100,100);opacity:0.4;stroke-width:0.01" />
        {/if}
        {#each players as p}
        <circle cx="{p.x}" cy="{p.y}" r="{oneFoot*p.size}" style="fill:{p.color}" />
        {/each}
    </svg>
</div>