<script>
    import { mdiTheater, mdiDramaMasks, mdiChessPawn } from "@mdi/js";
    import NavButton from "./NavButton.svelte";
</script>

<nav class="w-screen overflow-hidden bg-white flex flex-row md:flex-col md:w-auto md:mr-4 md:m-4 md:p-2 md:rounded-md">
    <NavButton url="/" sectionName="stage" caption="Stage" icon={mdiTheater} />
    <NavButton url="/scene" sectionName="scene" caption="Scenes" icon={mdiDramaMasks} /> 
    <NavButton url="/creatures" sectionName="creatures" caption="Creatures" icon={mdiChessPawn} /> 
</nav>
