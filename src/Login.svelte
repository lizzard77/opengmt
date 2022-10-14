<script>
    import { userName, currentCampaign } from "./stores";

    let name = $userName;
    let campaignId = parseInt(localStorage.getItem("campaignId") || "0");

    let campaignsToJoin;
    let message;
    let nameTextBox;
    let campaignSelect;

    $: nameTextBox?.focus();

    async function login()
    {
        $userName = name;
        message = "";

        if (name && !campaignId)
        {
            const campaigns = await currentCampaign.loadAll();
            campaignsToJoin = campaigns.filter(c => c.players.find(p => p.name === name));
            if (!campaignsToJoin.length)
            {
                message = `Sorry, ${name} - there are no campaigns for you.`;
                campaignsToJoin = null;
                campaignId = 0;
                name = "";
                nameTextBox?.focus();
            } else {
                campaignSelect?.focus();
            }
        } 

        if (name && campaignId)
            await currentCampaign.load(campaignId);
    }

    if (name && campaignId)
        currentCampaign.load(campaignId);
</script>

<div class="m-auto mt-48 w-1/2 md:w-1/4">
    <h1 class="text-2xl font-bold">Join the Table</h1>

    <div class="mt-2">
    <p>Your name</p>
    <input bind:this={nameTextBox} type="text" bind:value={name} class="border-2 rounded-md w-full p-1 disabled:bg-slate-200" disabled={campaignsToJoin?.length} />
    </div>

    {#if message}
    <div class="italic">{message}</div>
    {/if}

    {#if campaignsToJoin}
    <div class="mt-2">
        <p>Campaign</p>
        <select bind:this={campaignSelect} bind:value={campaignId} class="border-2 rounded-md w-full p-1">
        {#each campaignsToJoin as c}
        <option value={c.id}>{c.name}</option>
        {/each}
        </select>
    </div>
    {/if}

    <div class="mt-2">
    {#if !campaignId}
    <button on:click={login} class="bg-orange-500 p-2 rounded-lg text-white disabled:bg-slate-200" disabled={!name}>Next</button>
    {:else}
    <button on:click={() => { campaignsToJoin = null; name = ""; campaignId = 0; }} class="bg-orange-500 p-2 rounded-lg text-white">Back</button>
    <button on:click={login} class="bg-green-500 p-2 rounded-lg text-white">Join</button>
    {/if}
    </div>
</div>