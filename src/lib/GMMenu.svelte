<script>
    import Icon from "./Icon.svelte";
    import { mdiTheater, mdiFileDocument, mdiMusicNote, mdiDramaMasks, mdiHumanGreeting } from "@mdi/js";

    import { Router, Link, Route } from "svelte-routing";
</script>

<nav class="z-50 fixed flex flex-col top-1/2 -translate-y-1/2 bg-gray-100 p-2 rounded-r-md">
    <Link to="/" title="Stage" class="p-2 bg-slate-200 rounded-lg"><Icon path={mdiTheater} size=24 /></Link>
    <Link to="scenes" class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiDramaMasks} size=24 /></Link>
    <Link to="sheets" class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiHumanGreeting} size=24 /></Link>
    <Link to="docs" class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiFileDocument} size=24 /></Link>
    <Link to="audio" class="mt-2 p-2 bg-slate-200 rounded-lg"><Icon path={mdiMusicNote} size=24 /></Link>
</nav>