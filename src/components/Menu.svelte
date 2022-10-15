<script>
    import { mdiTheater, mdiDramaMasks, mdiChessPawn, mdiPackage } from "@mdi/js";
    import NavButton from "./NavButton.svelte";
</script>

<nav class="overflow-hidden bg-white flex flex-col w-auto">
    <NavButton url="/" sectionName="stage" caption="Stage" icon={mdiTheater} />
    <NavButton url="/scene" sectionName="scene" caption="Scenes" icon={mdiDramaMasks} /> 
    <NavButton url="/creatures" sectionName="creatures" caption="Creatures" icon={mdiChessPawn} /> 
    <NavButton url="/campaigns" sectionName="campaign" caption="Campaigns" icon={mdiPackage} /> 
</nav>

