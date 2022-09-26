<script>
    import { activeSection, currentScene } from "./stores";
    import { mdiGlasses } from "@mdi/js";

    import Icon from "./components/Icon.svelte";
    import Modal from "./components/Modal.svelte";
    import { putObject } from "./api";
    import Upload from "./components/Upload.svelte";
    import AudioBoard from "./lib/AudioBoard.svelte";
    import Screen from "./components/Screen.svelte";
    import Draggable from "./lib/Draggable.svelte";
    import CreatureSelect from "./lib/CreatureSelect.svelte";
    import DashCreatureInfo from "./lib/DashCreatureInfo.svelte";
    import DashAssetInfo from "./lib/DashAssetInfo.svelte";
    import DashPcInfo from "./lib/DashPCInfo.svelte";

    $activeSection = "creatures";

    async function saveScene()
    {
        await putObject("/api/scenes", $currentScene);
        $currentScene = $currentScene;
    }

    let showCreatureSelect = false;

    async function addSelectedCreature(e)
    {
        $currentScene.creatures.push(e.detail);
        await saveScene();
    }
</script>

<Screen title={$currentScene.name}>
        <div class="p-4 min-h-fit md:h-full md:min-h-0 md:grid md:grid-cols-3 w-full">
            <div class="flex flex-col md:col-start-1 md:col-span-2">
                <div class="flex flex-row col-start-1 col-span-2">
                    <button class="flex items-center" on:click={() => showCreatureSelect = true}><Icon path={mdiGlasses} size={16} class="mr-2" /> Kreatur hinzufügen</button>
                </div>

                <div class="box ">
                    <h1>Charaktere</h1>

                    {#each $currentScene.creatures.filter(cc => cc.type === "pc") as c}
                    <DashPcInfo creature={c} />
                    {/each}
                </div>
            </div>

            <div class="md:col-start-3">
                <div class="box">
                    <h1>NPCs</h1>
                    <div class="flex flex-row">
                        <button>+</button>
                        <button>Dialoge</button>
                        <button>Schnellerzeugung</button>
                    </div>

                    {#each $currentScene.creatures.filter(cc => cc.type === "npc") as c}
                    <DashCreatureInfo creature={c} />
                    {/each}
                </div>

                <div class="box md:col-start-3">
                    <h1>Monster</h1>
                    {#each $currentScene.creatures.filter(cc => cc.type === "monster") as c}
                    <DashCreatureInfo creature={c} />
                    {/each}
                </div>
            </div>
        </div>
</Screen>

{#if showCreatureSelect}
<Modal bind:isOpen={showCreatureSelect}>
    <CreatureSelect on:creatureSelected={addSelectedCreature} bind:isOpen={showCreatureSelect} />
</Modal>
{/if}

<style>
    button {
        @apply mr-2 p-1 rounded-md text-sm bg-slate-200 border-0 flex
    }

    h1 {
        @apply text-lg font-bold mt-4 border-b-2 border-red-800 mb-2
    }

    [contenteditable] { @apply p-2 border-slate-200 border-2 }
    
    :focus { @apply bg-white }

    .box {
        @apply w-full pr-2;
    }
</style>

