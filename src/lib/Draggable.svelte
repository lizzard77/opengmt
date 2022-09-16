<script>
    import { draggable } from "../draggable";
    import { activeSection, combat, currentMarker, isMaster, squareSizeInPx, currentHandout } from "../stores";

    import Map from "./Map.svelte";
    import MapTools from "./MapTools.svelte";

    export let showTools = false;
    let zoom = 1.0;
    let fog = true;
    let showReach = true;
    let w = 0;
    let h = 0;
    let left = 0;
    let top = 0;

    

    function setMapCenter(e)
    {
        const marker = e?.detail || $currentMarker;
        if (!marker)
            return;

        console.log("setMapCenter", marker);
        //draggable.setOffsets(-((marker.x * $squareSizeInPx) - w / 2), -((marker.y* $squareSizeInPx) - h / 2));
    }

    setMapCenter();
</script>

<div use:draggable class="draggable" bind:clientHeight={h} bind:clientWidth={w}>
    <Map {showReach} on:centerMapToPlayer={setMapCenter} {fog} {zoom} />
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


