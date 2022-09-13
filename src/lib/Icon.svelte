<script>
export let path = "";	
export let size = 24;
export let color = "#000000";
let classNames = "";
export { classNames  as class };
</script>
			
<svg class="inline-block p-0 m-0 fill-current {classNames}" {...$$restProps} viewBox="0 0 24 24" style="width: {size}px; height: {size}px;">
    <path d={path} fill={color} />
</svg>
