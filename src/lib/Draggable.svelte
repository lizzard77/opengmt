<script>
    import { draggable} from "../draggable";
</script>

<div use:draggable class="draggable">
    <slot></slot>
</div>

<style>
	.draggable {
		user-select: none;
        touch-action: none;
	}
</style>


