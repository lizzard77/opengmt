<script>
    import { draggable } from "../draggable";
    import { squareSizeInPx } from "../stores";

    import Map from "./Map.svelte";
    import MapTools from "./MapTools.svelte";

    export let showTools = false;
    export let zoom = 1.0;
    export let fog = true;
    export let showReach = true;    
    export let centerX = -1;
    export let centerY = -1;
</script>

<div use:draggable={{ centerX, centerY, scale : $squareSizeInPx }} class="draggable111">
    <Map bind:showReach bind:fog bind:zoom />
</div>

{#if showTools}
<MapTools bind:fog bind:zoom />
{/if}

<style>
	.draggable {
		user-select: none;
        touch-action: none;
	}
</style>


