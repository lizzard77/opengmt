import { mdiConsolidate } from "@mdi/js";
import * as signalR from "@microsoft/signalr";
import { writable, derived } from "svelte/store";
import { get } from "./api";

const storedUserName = localStorage.getItem("userName");
export const userName = writable(storedUserName);
userName.subscribe(value => {
    localStorage.setItem("userName", value);
});

export const activeSection = writable("");
export const statsEditing = writable(false);

export const creatures = writable([]);
export const maps = writable([]);
export const scenes = writable([]);

export const currentCampaign = writable();
export const currentScene = derived(currentCampaign, ($currentCampaign) => $currentCampaign.currentScene);
export const isMaster = derived([currentCampaign, userName], ([$currentCampaign, $userName]) => {
    if ($userName && $currentCampaign?.players?.length)
    {
        const player = $currentCampaign.players.find(p => p.name === $userName);
        return $currentCampaign.dmPlayerId === player.id;
    }
    return false;
});
export const markers = derived(currentScene, ($currentScene) => $currentScene.markers);
export const sounds = derived(currentScene, ($currentScene) => $currentScene.assets.filter(a => a.type === 2));
export const handouts = derived(currentScene, ($currentScene) => $currentScene.assets.filter(a => a.type !== 2));

export const squareSizeInPx = writable(1.0);
export const combat = writable(false);
export const currentMarker = writable({ id: 0, name: "", x:0, y:0, reach : 1, visible : true, light : true, creatureId : -1, initiative : -1, color : "pink", size: 1 });
export const currentHandout = writable("");

/*
import { writable } from "svelte/store";

const storedTheme = localStorage.getItem("theme");
export const theme = writable(storedTheme);
theme.subscribe(value => {
    localStorage.setItem("theme", value === 'dark' ? 'dark' : 'light');
});
*/

/*
import { writable } from 'svelte/store';
function myStore() {
	const { subscribe, set, update } = writable(0);

	return {
		subscribe,
		addOne: () => update(n => n + 1),
		reset: () => set(0)
	};
}

// Use it like a regular store
myStore.subscribe(console.log)
myStore.addOne()
*/
