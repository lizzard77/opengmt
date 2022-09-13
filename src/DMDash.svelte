<script>
    import { showPIP, activeSection, currentScene, currentHandout, creatures } from "./stores";
    import { mdiGlasses } from "@mdi/js";

    import Icon from "./lib/Icon.svelte";
    import Modal from "./lib/Modal.svelte";
    import StatBlock from "./lib/StatBlock.svelte";
    import { putObject } from "./api";
    import { hubConnection } from "./hub";
    import Upload from "./lib/Upload.svelte";
    import AudioBoard from "./lib/AudioBoard.svelte";
    import Screen from "./Screen.svelte";
import Pip from "./lib/PIP.svelte";
import Draggable from "./lib/Draggable.svelte";
import Map from "./lib/Map.svelte";

    let { name, description, strongStart, secretsAndHints, phantasticLocations } = $currentScene;

    $activeSection = "dash";

    function setHandout(asset = "")
    {
        $currentHandout = asset;
        hubConnection.invoke('SetHandout', JSON.stringify(asset));
    }

    let showMonsterStats = false;
    let monsterStatBlock = {};
    function showMonsterStatBlock(c = null){
        monsterStatBlock  = c;
        showMonsterStats = true;
    }

    async function saveScene()
    {
        const sceneUpdate ={ ...$currentScene, name, description, strongStart, secretsAndHints, phantasticLocations };
        await putObject("/api/scenes", sceneUpdate);
        $currentScene = sceneUpdate;
    }

    async function addCharacters()
    {
        $creatures.forEach(c => {
            $currentScene.creatures.push(c);
        });
        await saveScene();
    }

    async function removeCharacter(c)
    {
        $currentScene.creatures = $currentScene.creatures.filter(cc => cc.id !== c.id);
        await saveScene();
    }

    let showReach = true;
    let w = 0;
    let h = 0;
    let left = 0;
    let top = 0;


</script>

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
        @apply w-full md:w-5/12 basis-36 flex-grow overflow-auto pr-2;
    }
</style>

<Screen title={$currentScene.name}>
        <div class="p-4 min-h-fit md:h-full md:min-h-0 md:flex md:flex-col md:flex-wrap">
            <div class="box">
                <h1>Charaktere</h1>
                <div class="flex flex-row">
                    <button on:click={addCharacters}><Icon path={mdiGlasses} size={16} /></button>
                </div>

                {#each $currentScene.creatures.filter(cc => cc.type === "pc") as c}
                    <div on:click={() => showMonsterStatBlock(c)}>{c.name}</div>
                {/each}
            </div>

            <div class="box">
                <h1>NPCs</h1>
                <div class="flex flex-row">
                    <button>+</button>
                    <button>Dialoge</button>
                    <button>Schnellerzeugung</button>
                </div>

                {#each $currentScene.creatures.filter(cc => cc.type === "npc") as c}
                    <div on:click={() => showMonsterStatBlock(c)}>{c.name}</div>
                {/each}
            </div>

            <div class="box">
                <h1>Monster</h1>
                <div class="flex flex-row">
                    <button on:click={showMonsterStatBlock}>+</button>
                    <button>Schnellerzeugung</button>
                </div>

                {#each $currentScene.creatures.filter(cc => cc.type === "monster") as c}
                    <div class="flex" on:click={() => showMonsterStatBlock(c)}>{c.name} <button on:click={() => removeCharacter(c)}>X</button></div>
                {/each}
            </div>

            <div class="box">
                <h1>Starker Start</h1>
                <p contenteditable bind:innerHTML={strongStart} on:blur={saveScene}></p>
            </div>

            <div class="box">
                <h1>Geheimnisse und Hinweise</h1>
                <p class="" contenteditable bind:innerHTML={secretsAndHints} on:blur={saveScene}></p>
            </div>

            <div class="box">
                <h1>Fantastische Orte</h1>
                <p contenteditable bind:innerHTML={phantasticLocations} on:blur={saveScene}></p>
            </div>

            <div class="box">
                <h1>Magische Gegenstände</h1>
            </div>

            <div class="box">
                <h1>Handouts</h1>
                {#if $currentHandout}
                <button on:click={() => setHandout("")}>Handout Schließen</button>
                {:else}
                <Upload folder="handouts" /><br />
                <button on:click={() => setHandout("/assets/handouts/BarthensProvisions-1-scaled.jpg")}>BarthensProvisions-1-scaled.jpg</button>
                <button on:click={() => setHandout("/assets/handouts/637981721880603677.png")}>Dorf</button>
                {/if}
            </div>

            <div class="box">
                <h1>Audio</h1>
                <AudioBoard />
            </div>

            <div class="box flex-grow basis-[400px]" >
                <h1>Map</h1>
                <div bind:clientWidth={w} bind:clientHeight={h} class="flex-1 relative" >
                    <Draggable bind:left bind:top screenWidth={w} screenHeight={h}>
                        <Map {showReach} />
                    </Draggable>
                </div>
            </div>

            <div class="box overflow-hidden">
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
</Screen>

<Modal bind:isOpen={showMonsterStats}>
    <StatBlock creature={monsterStatBlock} bind:isOpen={showMonsterStats} />
</Modal>
