<script>
	export let left = 0;
	export let top = 0;
	
	let moving = false;
    let pos = { x: 0, y: 0 };
    let content;
    let screenHeight;
    let screenWidth;
	
	function onMouseDown(e) {
        console.log("down");
        moving = true;
	}
	
	function onMouseMove(e) {
		if (moving) {
            console.log("move");
            left += e.movementX;
			top += e.movementY;
		}
	}
	
	function onMouseUp() {
        console.log("up");
		moving = false;
	}

    function onTouchStart(e)
    {
        console.log("touch start");
        moving = true;
        pos.x = e.touches[0].clientX;
        pos.y = e.touches[0].clientY;
    }

    function onTouchMove(e)
    {
        //console.log("touch move", e.touches[0]);
        left += e.touches[0].clientX - pos.x;
        top += e.touches[0].clientY - pos.y;

        if (left>0) left = 0;
        if (top>0) top = 0;

        var maxX= screenWidth -content.firstChild.clientWidth;
        var maxY = screenHeight - content.firstChild.clientHeight;
        if (left<maxX) left = maxX;
        if (top<maxY) top=maxY;
        

        pos.x = e.touches[0].clientX;
        pos.y = e.touches[0].clientY;
    }

    function onTouchEnd()
    {
        console.log("touch end");
        moving = false;
    }
</script>

<section on:mousedown={onMouseDown} on:touchstart={onTouchStart} style="left: {left}px; top: {top}px;" class="draggable">
    <div bind:this={content}>
	    <slot></slot>
    </div>
</section>

<svelte:window on:mouseup={onMouseUp} on:mousemove={onMouseMove} on:touchmove={onTouchMove} bind:innerHeight={screenHeight} bind:innerWidth={screenWidth} />

<style>
	.draggable {
		user-select: none;
		position: absolute;
	}
</style>

