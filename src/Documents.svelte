<script>
    import {  hubConnection, currentHandout } from "./stores";

    function setHandout(asset = "")
    {
        $currentHandout = asset;
        $hubConnection.invoke('SetHandout', JSON.stringify(asset));
    }
</script>

<div class="ml-24">
    <h1 class="text-lg font-bold mb-4">Handouts</h1>
    {#if $currentHandout}
    <button on:click={() => setHandout("")} class="p-2 rounded-lg bg-slate-200">Handout Schließen</button>
    {:else}
    <button on:click={() => setHandout("/assets/handouts/BarthensProvisions-1-scaled.jpg")} class="p-1">BarthensProvisions-1-scaled.jpg</button>
    {/if}

    
    <h1 class="text-lg font-bold mb-4 mt-4">Dialoge</h1>
<pre>
<b>DIALOG BEI ANKUNFT</b>
1: "Ich geh da ganz sicher nicht mehr rein!!"
2: "Aber wie sollen wir dann weiterarbeiten? "
1: "Dieses Ding... hätte uns fast gefressen!?!?!?"
2: "Das war bestimmt nur ein kleines ... was auch immer."
1: "Egal. Da geh ich nicht wieder rein!!"
[[ENDLOSER STREIT]]

<b>DIALOG BEI BEKANNTSCHAFT</b>
Wenn die Charaktere sie erreicht haben:

1: "Oh, was führt euch denn hierher?"
2: "Wir sind ddd und"
1: "aaa"

1: "Ihr seht aber mächtig aus... bestimmt könnt ihr uns bei einem Problem..."
2: "Sei doch still. Die Herrschaften müssen wir nicht belästigen mit unseren Kleinigkeiten"
1: "Kleinigkeiten nennst du das? Das hätte mich fast gefre..."
2: "Pscht. Was werden die denn von uns denken?"
1: "Ist mir egal, ich will auch weiterarbeiten. Aber dazu muss dieses Ding weg."
2: "Ja, aber... "

<b>DIALOG QUEST</b>
1: "Danke für die Warnung, aber wir haben hier selbst schon Probleme."
2: "Aber keine schlimmen... wir kommen klar. Danke für die Warnung jedenfalls"
1: "Keine Kleinen? Du spinnst ja wohl! Meint ihr //wendet sich an die PC// ihr könnt uns beim Beseitigen einer kleinen Kreatur helfen?"
2: "Aber was sollen wir den Herrschaften denn als Entlohnung anbieten? Hast du das mal bedacht?"
1: "Habe ich. Wenn ihr uns helft, überlassen wir euch unsere Steine der Macht"
2: "Aber aaa, das können wir doch nicht..."
1: "Papperlapp, bbb, das müssen wir. Sonst können wir wieder heimfahren."
2: "Na gut."
</pre>
</div>