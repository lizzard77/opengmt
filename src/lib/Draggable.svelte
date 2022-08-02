<script>
    import { afterUpdate } from "svelte";
    
	export let left = 0;
	export let top = 0;
	
	let moving = false;
    let pos = { x: 0, y: 0 };

    let screenHeight = 0;
    let screenWidth = 0;
    let contentHeight;
    let contentWidth;
	let maxX = 0; 
    let maxY = 0;

    afterUpdate(()=> {
        maxX = screenWidth - contentWidth;
        maxY = screenHeight - contentHeight;

        if (left > 0 || screenWidth > contentWidth) left = 0;
        if (top > 0 || screenHeight > contentHeight) top = 0;
        if (left < maxX && contentWidth > screenWidth) left = maxX;
        if (top < maxY && contentHeight > screenHeight) top=maxY;
    });
        
	function onMouseDown(e) {
        console.log("down");
        moving = true;
	}
	
	function onMouseMove(e) {
		if (moving) {
            //console.log("move");
            left += e.movementX;
			top += e.movementY;
		}
	}
	
	function onMouseUp(e) {
        //console.log("up");
		moving = false;
	}

    function onTouchStart(e)
    {
        moving = true;
        pos.x = e.touches[0].clientX;
        pos.y = e.touches[0].clientY;
    }

    function onTouchMove(e)
    {
        left += e.touches[0].clientX - pos.x;
        top += e.touches[0].clientY - pos.y;
        pos.x = e.touches[0].clientX;
        pos.y = e.touches[0].clientY;
    }

    function onTouchEnd()
    {
        //console.log("touch end");
        moving = false;
    }
</script>

<section on:mousedown={onMouseDown} on:touchstart={onTouchStart} style="left: {left}px; top: {top}px;" class="draggable" bind:clientWidth={contentWidth} bind:clientHeight={contentHeight}>
    <slot></slot>
</section>

<svelte:window on:mouseup={onMouseUp} on:mousemove={onMouseMove} on:touchmove={onTouchMove} on:touchend={onTouchEnd} bind:innerHeight={screenHeight} bind:innerWidth={screenWidth} />

<style>
	.draggable {
		user-select: none;
		position: absolute;
	}
</style>

