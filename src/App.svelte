<script>
    import { Router, Route } from "svelte-routing";
    import { markers, sounds, isMaster, creatures, scenes, maps, session, currentMarker } from "./stores";

    import Stage from "./Stage.svelte";
    import Szene from "./Szene.svelte";    
    import Creatures from "./Creatures.svelte";    

    import ProgressCircle from "./components/ProgressCircle.svelte";
    import { get } from "./api";
    import { hubConnection } from "./hub";
    
    let baseData = Promise.all([
        (async () => {
            let loadedCreatures = await get("/api/creatures");
            loadedCreatures.forEach(p => {
                p.x = Math.floor(Math.random() * 20);
                p.y = Math.floor(Math.random() * 15);
                p.initiative = -1;
                p.visible = false;
            });
            $creatures = loadedCreatures;
        })(),

        (async () => {
            $maps = await get("/api/maps");
        })(),

        (async () => {
            $scenes = await get("/api/scenes");
        })()
    ]);
    
    let loader = (async () => {
            await baseData;
        })();

    async function handleKey(e)
    {
        if (e.key === "m" && e.ctrlKey) {
            e.preventDefault();
            $isMaster = !$isMaster;
        }
    }

</script>

<svelte:window on:keydown={handleKey}/>

{#await loader}
    <div class="text-center mt-8">
        <ProgressCircle />
        Lade Daten...
    </div>
{:then}
<Router>
    <Route path="/" component="{Stage}" />
    <Route path="scene" component="{Szene}" />
    <Route path="creatures" component="{Creatures}" />
</Router>


{/await}

