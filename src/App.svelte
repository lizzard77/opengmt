<script>
    import { get } from "./api";
    import { Router, Route } from "svelte-routing";
    import { userName, currentCampaign, creatures, scenes, maps } from "./stores";

    import Stage from "./Stage.svelte";
    import Szene from "./Szene.svelte";    
    import Creatures from "./Creatures.svelte";    
    import ProgressCircle from "./components/ProgressCircle.svelte";
    import Setup from "./Setup.svelte";
    import Login from "./Login.svelte";
    
    let needInit = false;
    
    let baseData = Promise.all([
        creatures.loadAll(),
        maps.loadAll(),
        scenes.loadAll()
    ]);
    
    let loader = (async () => {
        await baseData;
        await get("/api/players", (e) => needInit = true);
        await get("/api/campaigns", (e) => needInit = true);
        console.log(needInit)
    })();
</script>

{#await loader}
    <div class="text-center mt-8">
        <ProgressCircle />
        Lade Daten...
    </div>
{:then}

{#if !needInit && $userName && $currentCampaign}
<Router>
    <Route path="/" component="{Stage}" />
    <Route path="scene" component="{Szene}" />
    <Route path="creatures" component="{Creatures}" />
</Router>
{:else if needInit}
<Setup />
{:else }
<Login />
{/if}

{/await}

