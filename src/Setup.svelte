<script>
    import { userName, campaigns, currentCampaign } from "./stores";

    let name;

    async function setup()
    {
        var campaign = await campaigns.create(
            { 
                name: name + "'s Campaign", 
                players : [ { name } ], 
                currentScene: { name : "Intro Scene" }
            });
        $userName = name;
        await currentCampaign.load(campaign.id);
        window.location.reload();
    }
</script>

<div class="m-auto mt-48 w-1/4">
    <h1 class="text-2xl font-bold">Setup your OpenGMT</h1>

    <div class="mt-2">
    <p>Your name</p>
    <input type="text" bind:value={name} class="border-2 rounded-md w-full p-1" />
    </div>
    <div class="mt-2">
    <button on:click={setup} class="bg-green-500 p-2 rounded-md text-white">Set up</button>
    </div>
</div>