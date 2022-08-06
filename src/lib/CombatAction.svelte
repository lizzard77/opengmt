<script>
    import { scenes, currentScene, creatures, currentPlayer } from "../stores";
    import { dice, roll2 } from "./dice";
    import Modal from "./Modal.svelte";
    
    let attackRollResult = "";
    let damageResult = "";
    let currentAttack = null;

    function attack(a)
    {
        currentAttack = a;
        const d = dice.find(d => d.name === "W20");
        if (d)
        {
            d.number = 1;
            d.modifier = parseInt(a.mod) || 0;
            const result = roll2([d]);
            attackRollResult = `${result[0].total} + ${d.modifier} = ${result[0].total + d.modifier}`;
        }
    }

    function damage(a)
    {
        let num, name, mod = "";
        var myRegexp = new RegExp("(\\d+)\\s*[W|d]\\s*(\\d+)(\\s*([+-]*)\\s*(\\d+))*", "g");
        var match = myRegexp.exec(a.dmg);
        if (match != null) {
            num = match[1];
            name = match[2]
            mod = match[3] + match[4];
        }

        const d = dice.find(d => d.name === "W" + name);
        if (d)
        {
            d.number = parseInt(num);
            d.modifier = parseInt(mod) || 0;
            const result = roll2([d]);
            damageResult = `${result[0].total} + ${d.modifier} = ${result[0].total + d.modifier}`;
        }

    }

    function run(a)
    {
        if (!currentAttack)
        {
            attack(a);
        } else {
            damage(currentAttack);
        }
    }

    function close()
    {
        isOpen = false;
        attackRollResult="";
        damageResult="";
        currentAttack = null;
    }

    let isOpen = false;
</script>

<Modal bind:isOpen={isOpen}>
    <div class="h-full w-full bg-white rounded-xl p-8">
        {#if !currentAttack}
        <ul>
        {#each $currentPlayer.attacks as a}
        <button class="block p-2" on:click={() => run(a)}>
            <li>{a.name} ({a.mod} / Schaden: {a.dmg})</li>
        </button>
        {/each}
        </ul>
        {:else}
        <p>
            {attackRollResult} <button class="block p-2 rounded-xl bg-red-500 text-white" on:click={() => run()}>{currentAttack.dmg} Schaden würfeln</button><br />
            {damageResult}
        </p>
        {/if}
        <button class="border-2 rounded-xl p-2 mt-4 bg-black text-white" on:click={close}>Zurück</button>
    </div>
</Modal>

<button class="border-2 rounded-xl p-2 mr-2 bg-red-500 text-white" on:click={() => isOpen = true}>ANGRIFF</button>
