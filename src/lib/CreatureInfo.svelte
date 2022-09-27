<script>
    import { createEventDispatcher } from "svelte";
    import { currentMarker, markers } from "../stores";
    import { mdiEyeOff, mdiEye, mdiTarget, mdiLampOutline, mdiLamp } from "@mdi/js";
    import { hubConnection } from "../hub";
    import { getState, updateState } from "../session";
    const dispatch = createEventDispatcher();

    import Icon from "../components/Icon.svelte";
    
    export let creature;

    let { id, name, color } = creature;
    let { visible, light } = getState(id);
    $:  {
        if (id)
        {
            let state = $markers?.find(c => c.creatureId === id);
            if (state)
            {
                visible = state.visible; 
                light = state.light;
            }
        }
    }

    async function setPlayer()
    {
        $currentMarker = getState(creature.id);
        await updateState($currentMarker);    
        console.log("new player: ", creature.id, $currentMarker);
        hubConnection.invoke("SetCurrentPlayer", JSON.stringify($currentMarker));
    }

    function centerPlayer()
    {
        dispatch("centerMapToPlayer", $currentMarker);
    }

    async function toggleVisible()
    {
        $currentMarker.visible = !$currentMarker.visible;
        if (!$currentMarker.visible)
            $currentMarker.light = false;
        await updateState($currentMarker);    
    }

    async function toggleLight()
    {
        $currentMarker.light = !$currentMarker.light;
        if ($currentMarker.light)
            $currentMarker.visible = true;
        await updateState($currentMarker);
    }

</script>
		
<div class="m-0 p-1" class:bg-blue-300={id === $currentMarker?.creatureId} on:click={setPlayer}>
    <button on:click={toggleVisible} class="p-2 rounded-lg bg-slate-200 ">
        <Icon size={20} path={visible ? mdiEye :mdiEyeOff} />
    </button>
    <button on:click={toggleLight} class="p-2 rounded-lg bg-slate-200 ">
        <Icon size={20} path={light ? mdiLamp : mdiLampOutline } />
    </button>
    <button class="p-2 w-40" style="border-left: 8px solid {color}">{name}</button>
    <button on:click={centerPlayer} class="p-2 rounded-lg bg-slate-200 ">
        <Icon size={20} path={mdiTarget} />
    </button>
</div>