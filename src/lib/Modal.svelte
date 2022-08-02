<script>
    export let isOpen = true;
</script>

{#if isOpen}
<div class="absolute top-[10%] left-[10%] w-[80%] h-[80%] z-50">
    <slot />
</div>
<div class="absolute left-0 top-0 bg-black opacity-50 z-30 h-screen w-screen" on:click={() => isOpen = false}></div>
{/if}
