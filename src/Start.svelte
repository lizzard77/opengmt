<script>
import ProgressCircle from "./lib/ProgressCircle.svelte";
import SceneChooser from "./SceneChooser.svelte";
import { isMaster } from "./stores";
</script>

{#if !$isMaster}
    <div class="fixed top-20 left-1/2 -translate-x-1/2 text-center mt-8">
        <ProgressCircle />
        Warte auf Spielstart...
    </div>
{:else}
    <SceneChooser />
{/if}