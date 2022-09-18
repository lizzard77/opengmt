<script>
    import { activeSection, currentScene } from "./stores";
    import { mdiGlasses } from "@mdi/js";

    import Icon from "./lib/Icon.svelte";
    import Modal from "./lib/Modal.svelte";
    import { putObject } from "./api";
    import Upload from "./lib/Upload.svelte";
    import AudioBoard from "./lib/AudioBoard.svelte";
    import Screen from "./Screen.svelte";
    import Draggable from "./lib/Draggable.svelte";
    import CreatureSelect from "./lib/CreatureSelect.svelte";
    import DashCreatureInfo from "./lib/DashCreatureInfo.svelte";
    import DashAssetInfo from "./lib/DashAssetInfo.svelte";
    import DashPcInfo from "./lib/DashPCInfo.svelte";
    import MapSelect from "./lib/MapSelect.svelte";
    import { loadSession } from "./session";

    $activeSection = "scene";

    async function saveScene()
    {
        await putObject("/api/scenes", $currentScene);
        $currentScene = $currentScene;
    }

    let showMapSelect = false;

    async function setMap(e)
    {
        $currentScene.map = e.detail;
        await saveScene();
        await loadSession();
    }
</script>

<Screen title={$currentScene.name}>
        <div class="p-4 min-h-fit md:h-full md:min-h-0 md:grid md:grid-cols-3">
            <div class="col-start-1">
                <div class="flex flex-row col-start-1">
                    <Upload folder="handouts" /><br />
                    <button on:click={() => showMapSelect = true}>Map</button>
                </div>

                <div class="box col-start-1">
                    <h1>Starker Start</h1>
                    <p contenteditable bind:innerHTML={$currentScene.strongStart} on:blur={saveScene}></p>
                </div>

                <div class="box col-start-1">
                    <h1>Szenen und Begegnungen</h1>
                    <p contenteditable bind:innerHTML={$currentScene.scenesAndEncounters} on:blur={saveScene}></p>
                </div>

                <div class="box col-start-1">
                    <h1>Geheimnisse und Hinweise</h1>
                    <p class="" contenteditable bind:innerHTML={$currentScene.secretsAndHints} on:blur={saveScene}></p>
                </div>

                <div class="box col-start-1">
                    <h1>Fantastische Orte</h1>
                    <p contenteditable bind:innerHTML={$currentScene.phantasticLocations} on:blur={saveScene}></p>
                </div>
            </div>

            <div class="col-start-2">
                <div class="box col-start-2">
                    <h1>Magische Gegenstände</h1>
                </div>

                <div class="box col-start-2">
                    <h1>Handouts</h1>
                    {#each $currentScene.assets as asset}
                    <DashAssetInfo {asset} />
                    {/each}
                </div>

                <div class="box basis-52 col-start-2">
                    <h1>Audio</h1>
                    <AudioBoard />
                </div>
            </div>

            <div class="col-start-3">
                <div class="box flex-grow basis-[400px] col-start-3" >
                    <h1>Map</h1>
                    <div class="flex-1 relative w-full h-96 overflow-hidden">
                        <Draggable fog={false} zoom={0.5} />
                    </div>
                </div>

                <div class="box overflow-hidden col-start-3">
                        <h1 class="text-lg font-bold mb-4 mt-4">Dialoge</h1>
                        <tt class="text-xs">
                <b>DIALOG BEI ANKUNFT</b><br/>
                1: "Ich geh da ganz sicher nicht mehr rein!!"<br/>
                2: "Aber wie sollen wir dann weiterarbeiten? "<br/>
                1: "Dieses Ding... hätte uns fast gefressen!?!?!?"<br/>
                2: "Das war bestimmt nur ein kleines ... was auch immer."<br/>
                1: "Egal. Da geh ich nicht wieder rein!!"<br/>
                [[ENDLOSER STREIT]]<br/>
                <br/>
                <b>DIALOG BEI BEKANNTSCHAFT</b><br/>
                Wenn die Charaktere sie erreicht haben:<br/>
                <br/>
                1: "Oh, was führt euch denn hierher?"<br/>
                2: "Wir sind ddd und"<br/>
                1: "aaa"<br/>
                <br/>
                1: "Ihr seht aber mächtig aus... bestimmt könnt ihr uns bei einem Problem..."<br/>
                2: "Sei doch still. Die Herrschaften müssen wir nicht belästigen mit unseren Kleinigkeiten"<br/>
                1: "Kleinigkeiten nennst du das? Das hätte mich fast gefre..."<br/>
                2: "Pscht. Was werden die denn von uns denken?"<br/>
                1: "Ist mir egal, ich will auch weiterarbeiten. Aber dazu muss dieses Ding weg."<br/>
                2: "Ja, aber... "<br/>
                <br/>
                <b>DIALOG QUEST</b><br/>
                1: "Danke für die Warnung, aber wir haben hier selbst schon Probleme."<br/>
                2: "Aber keine schlimmen... wir kommen klar. Danke für die Warnung jedenfalls"<br/>
                1: "Keine Kleinen? Du spinnst ja wohl! Meint ihr //wendet sich an die PC// ihr könnt uns beim Beseitigen einer kleinen Kreatur helfen?"<br/>
                2: "Aber was sollen wir den Herrschaften denn als Entlohnung anbieten? Hast du das mal bedacht?"<br/>
                1: "Habe ich. Wenn ihr uns helft, überlassen wir euch unsere Steine der Macht"<br/>
                2: "Aber aaa, das können wir doch nicht..."<br/>
                1: "Papperlapp, bbb, das müssen wir. Sonst können wir wieder heimfahren."<br/>
                2: "Na gut."<br/>
                        </tt>
                </div>
            </div>
        </div>
</Screen>

{#if showMapSelect}
<Modal bind:isOpen={showMapSelect}>
    <MapSelect on:mapSelected={setMap} bind:isOpen={showMapSelect} />
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

