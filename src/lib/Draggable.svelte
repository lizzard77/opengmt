<script>
    import { afterUpdate } from "svelte";
    import { isMaster, currentPlayer, hubConnection, squareSizeInPx, currentScene } from '../stores';
    
	export let left = 0;
	export let top = 0;
	
	let moving = false;
    let wasmoving = false;
    let pos = { x: 0, y: 0 };

    export let screenHeight = 0;
    export let screenWidth = 0;
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
        //console.log("down");
        moving = true;
	}
	
	function onMouseMove(e) {
		if (moving) {
            wasmoving = true;
            //console.log("move", left, top)
            left += e.movementX;
			top += e.movementY;
		}
	}
	
	function onMouseUp(e) 
    {
        console.log("up");
        if (wasmoving)
            window.addEventListener('click', captureClick, true);  // <-- This registeres this listener for the capture phase instead of the bubbling phase!
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
        if (moving)
        {
            wasmoving = true;
            left += e.touches[0].clientX - pos.x;
            top += e.touches[0].clientY - pos.y;
            pos.x = e.touches[0].clientX;
            pos.y = e.touches[0].clientY;
        }
    }

    function onTouchEnd(e)
    {
        console.log("touch end");
        if (wasmoving)
            window.addEventListener('click', captureClick, true);  // <-- This registeres this listener for the capture phase instead of the bubbling phase!
            
        moving = false;
    }

    function captureClick(e) {
        wasmoving = false;
        e.stopPropagation(); // Stop the click from being propagated.
        console.log('click captured');
        window.removeEventListener('click', captureClick, true); // cleanup
    }
</script>

<section on:mousedown={onMouseDown} on:touchstart={onTouchStart} style="left: {left}px; top: {top}px;" class="draggable" bind:clientWidth={contentWidth} bind:clientHeight={contentHeight}>
    <slot></slot>
</section>

<svelte:window on:mouseup={onMouseUp} on:mousemove={onMouseMove} on:touchmove={onTouchMove} on:touchend={onTouchEnd} />

<style>
	.draggable {
		user-select: none;
		position: absolute;
        touch-action: none;
	}
</style>

