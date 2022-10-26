<script>
    import { currentCampaign, activeSection, currentScene, scenes, handouts } from "./stores";

    import Modal from "./components/Modal.svelte";
    import Upload from "./components/Upload.svelte";
    import AudioBoard from "./lib/AudioBoard.svelte";
    import Screen from "./components/Screen.svelte";
    import DashAssetInfo from "./lib/DashAssetInfo.svelte";
    import MapSelect from "./lib/MapSelect.svelte";
    import EditableBlock from "./components/EditableBlock.svelte";
    import DashPcInfo from "./lib/DashPCInfo.svelte";
    import DashCreatureInfo from "./lib/DashCreatureInfo.svelte";
    import CreatureSelect from "./lib/CreatureSelect.svelte";
    import Icon from "./components/Icon.svelte";
    import { mdiGlasses } from "@mdi/js";
    import Map from "./lib/Map.svelte";
    
    $activeSection = "scene";

    let showMapSelect = false;
    let showCreatureSelect = false;

    let { name, magicItems, strongStart, scenesAndEncounters, secretsAndHints, phantasticLocations, id } = $currentScene;
    $: {
        if (id !== $currentScene.id)
            ({ name, magicItems, strongStart, scenesAndEncounters, secretsAndHints, phantasticLocations, id } = $currentScene);
    } 
    
    async function saveScene(fields)
    {
        const update = { ...$currentScene, ...fields }; 
        await scenes.save(update);
        await currentCampaign.reload();
    }

    async function setMap(e)
    {
        const update = { ...$currentScene, mapId : e.detail.id, map : e.detail };
        await scenes.save(update);
        await currentCampaign.reload();
    }

    
    async function addSelectedCreature(e)
    {
        $currentScene.creatures.push(e.detail);
        await scenes.save($currentScene);
        await currentCampaign.reload();
    }

    async function removeSelectedCreature(e)
    {
        let creatures = $currentScene.creatures.filter(cc => cc.id !== e.detail.id);
        const update = { ...$currentScene, creatures };
        await scenes.save(update);
        await currentCampaign.reload();
    }
</script>

<Screen title={$currentCampaign.name + " - " + $currentScene.name}>
    <div class="w-full h-full">
        <div class="flex flex-row p-2">
            <Upload folder="handouts" /><br />
            <button on:click={() => showMapSelect = true}>Map</button>
            <button class="flex items-center" on:click={() => showCreatureSelect = true}><Icon path={mdiGlasses} size={16} class="mr-2" /> Kreatur hinzufügen</button>
        </div>

        <div class="p-2 min-h-fit grid grid-cols-3">
            <div class="col-start-1">
                <div class="box flex-grow basis-[400px]" >
                    <h1>Map</h1>
                    <div class="flex-1 relative w-full h-96 overflow-hidden">
                        <Map fog={false} zoom={0.5} fit={true} />
                    </div>
                </div>

                <div class="box col-start-1">
                    <h1>Starker Start</h1>
                    <EditableBlock content={strongStart} on:contentChanged={(e) => saveScene({ strongStart: e.detail })} />
                </div>

                <div class="box col-start-1">
                    <h1>Szenen und Begegnungen</h1>
                    <EditableBlock content={scenesAndEncounters} on:contentChanged={(e) => saveScene({ scenesAndEncounters: e.detail })} />
                </div>

                <div class="box col-start-1">
                    <h1>Geheimnisse und Hinweise</h1>
                    <EditableBlock content={secretsAndHints} on:contentChanged={(e) => saveScene({ secretsAndHints: e.detail })} />
                </div>

                <div class="box col-start-1">
                    <h1>Fantastische Orte</h1>
                    <EditableBlock content={phantasticLocations} on:contentChanged={(e) => saveScene({ phantasticLocations: e.detail })} />                
                </div>
            </div>

            <div class="col-start-2">
                <div class="box">
                    <h1>Charaktere</h1>
                    {#each $currentScene.creatures.filter(cc => cc.type === "pc") as c}
                    <DashPcInfo creature={c} on:removeCreature={removeSelectedCreature} />
                    {/each}
                </div>

                <div class="box">
                    <h1>NPCs</h1>
                    {#each $currentScene.creatures.filter(cc => cc.type === "npc") as c}
                    <DashCreatureInfo creature={c} on:removeCreature={removeSelectedCreature} />
                    {/each}
                </div>
    
                <div class="box">
                    <h1>Monster</h1>
                    {#each $currentScene.creatures.filter(cc => cc.type === "monster") as c}
                    <DashCreatureInfo creature={c} on:removeCreature={removeSelectedCreature} />
                    {/each}
                </div>
            </div>

            <div class="col-start-3">
                <div class="box">
                    <h1>Magische Gegenstände</h1>
                    <EditableBlock content={magicItems} on:contentChanged={(e) => saveScene({ magicItems: e.detail })} />                
                </div>

                <div class="box">
                    <h1>Handouts</h1>
                    {#each $handouts as asset}
                    <DashAssetInfo {asset} />
                    {/each}
                </div>

                <div class="box">
                    <h1>Audio</h1>
                    <AudioBoard />
                </div>
            </div>
        </div>
    </div>
</Screen>

{#if showMapSelect}
<Modal bind:isOpen={showMapSelect}>
    <MapSelect on:mapSelected={setMap} bind:isOpen={showMapSelect} />
</Modal>
{/if}


{#if showCreatureSelect}
<Modal bind:isOpen={showCreatureSelect}>
    <CreatureSelect on:creatureSelected={addSelectedCreature} bind:isOpen={showCreatureSelect} />
</Modal>
{/if}


<style>
    button {
        @apply mr-2 p-1 rounded-md text-sm bg-slate-200 border-0 flex;
    }

    h1 {
        @apply text-lg font-bold mt-0 border-b-2 border-red-800 mb-2;
    }

    [contenteditable] { @apply p-2 border-slate-200 border-2; }
    
    :focus { @apply bg-white; }

    .box {
        @apply w-full pr-2 mb-6;
    }
</style>


