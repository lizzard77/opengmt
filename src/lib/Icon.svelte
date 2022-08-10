<script>
export let path = "";	
export let size = 24;
export let color = "#000000";
</script>
			
<div style="height: 100%; vertical-align: middle;">
    <svg class="inline-block p-0 m-0" style="width:{size}px;height:{size}px" viewBox="0 0 24 24">
        <path fill={color} d={path} />
    </svg>
</div>