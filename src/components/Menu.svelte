<script>
    import { mdiTheater, mdiDramaMasks, mdiDiceD20Outline, mdiChessPawn } from "@mdi/js";
    import Dice from "../lib/Dice.svelte";
    import NavButton from "./NavButton.svelte";

    let showDice = false;
</script>

<nav class="w-screen overflow-hidden bg-white flex flex-row md:flex-col md:w-auto md:mr-4 md:m-4 md:p-2 md:rounded-md">
    <NavButton url="/" sectionName="stage" caption="Stage" icon={mdiTheater} />
    <NavButton url="/scene" sectionName="scene" caption="Scenes" icon={mdiDramaMasks} /> 
    <NavButton url="/creatures" sectionName="creatures" caption="Creatures" icon={mdiChessPawn} /> 
    <NavButton sectionName={null} on:click={() => showDice = true} icon={mdiDiceD20Outline} caption="Dice" />
</nav>

<Dice bind:isOpen={showDice} />
