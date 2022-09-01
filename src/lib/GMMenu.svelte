<script>
    import Icon from "./Icon.svelte";
    import { mdiTheater, mdiFileDocument, mdiMusicNote, mdiDramaMasks, mdiHumanGreeting, mdiFitToScreenOutline, mdiDoor } from "@mdi/js";

    import { Router, Link, Route } from "svelte-routing";
    import { showPIP, isMaster } from "../stores";
</script>

<nav class="z-50 fixed flex flex-col top-1/2 -translate-y-1/2 bg-gray-100 p-2 rounded-r-md">
    <Link to="/" title="Stage" class="p-2 bg-slate-200 rounded-lg"><Icon path={mdiTheater} size=24 /></Link>
    <Link to="dash" class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiDramaMasks} size=24 /></Link>
    <Link to="audio" class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiMusicNote} size=24 /></Link>
    <button on:click={() => $showPIP = !$showPIP} class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiFitToScreenOutline} size=24 /></button>
    <button on:click={() => $isMaster = !$isMaster} class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiDoor} size=24 /></button>
</nav>