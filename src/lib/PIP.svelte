<script>
    import { showPIP, sounds, isMaster, creatures, scenes, maps, currentScene } from "../stores";

    $showPIP = true;
    
    function getPIPUrl()
    {
        const url = new URL(window.location.href);
        url.pathname = "";
        url.searchParams.set("pip", "true");
        return url.toString();
    }
</script>

{#if $currentScene && $currentScene.id && $showPIP}
<iframe src={getPIPUrl()} style="zoom: 0.4;"  class="block w-full h-full border-2" title="PIP" />
{/if}