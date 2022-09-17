<script>
    import { mdiTheater, mdiDramaMasks, mdiDiceD20Outline } from "@mdi/js";
    import Dice from "./Dice.svelte";
    import NavButton from "./NavButton.svelte";

    let showDice = false;
</script>

<nav class="w-screen overflow-hidden bg-white flex flex-row md:flex-col md:w-auto md:mr-4 md:m-4 md:p-2 md:rounded-md">
    <NavButton url="/" sectionName="stage" caption="Stage" icon={mdiTheater} />
    <NavButton url="/dash" sectionName="dash" caption="Dashboard" icon={mdiDramaMasks} /> 
    <NavButton sectionName={null} on:click={() => showDice = true} icon={mdiDiceD20Outline} caption="Würfel" />
</nav>

<Dice bind:isOpen={showDice} />
