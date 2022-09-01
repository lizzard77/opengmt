<script>
    import { createEventDispatcher } from "svelte";
    import { currentPlayer, session } from "../stores";
    import { mdiEyeOff, mdiEye, mdiTarget, mdiLampOutline, mdiLamp } from "@mdi/js";
    import { hubConnection } from "../hub";
    import { getState, updateState } from "../session";
    const dispatch = createEventDispatcher();

    import Icon from "./Icon.svelte";
    
    export let creature;

    let state = getState(creature.id);
    $: if ($session) { state = getState(creature.id); visible = state.visible; light = state.light; }

    const { id, name, color } = creature;
    let { visible, light } = state;

    function setPlayer()
    {
        $currentPlayer = creature;
        hubConnection.invoke("SetCurrentPlayer", JSON.stringify(creature));
    }

    function centerPlayer()
    {
        dispatch("centerMapToPlayer", creature);
    }

    async function toggleVisible()
    {
        const current = getState(id);
        current.visible = !current.visible;
        await updateState(current);    
    }

    async function toggleLight()
    {
        const current = getState(id);
        current.light = !current.light;
        if (current.light)
            current.visible = true;
        await updateState(current);
    }

</script>
		
<div class="m-0 p-1" class:bg-blue-300={id === $currentPlayer.id} on:click={setPlayer}>
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