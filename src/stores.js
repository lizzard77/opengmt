import * as signalR from "@microsoft/signalr";
import { writable, derived } from "svelte/store";
import { loadSession } from "./session";

export const activeSection = writable("");
export const statsEditing = writable(false);

export const creatures = writable([]);
export const maps = writable([]);
export const sounds = writable([]);
export const handouts = writable([]);
export const scenes = writable([]);
export const stage = writable({});
export const markers = writable([]);

const storedIsMaster = JSON.parse(localStorage.getItem("isMaster") || "false");
export const isMaster = writable(storedIsMaster);
isMaster.subscribe(value => {
    localStorage.setItem("isMaster", JSON.stringify(value));
});

export const squareSizeInPx = writable(1.0);
export const combat = writable(false);

export const currentMarker = writable({ id: 0, name: "", x:0, y:0, reach : 1, visible : true, light : true, creatureId : -1, initiative : -1, color : "pink", size: 1 });
export const currentScene = writable({ id: 0, map: {}, creatures : [], name : null, description : null, strongStart : null, secretsAndHints : null, phantasticLocations : null, scenesAndEncounters : null, assets: [] });

export const currentHandout = writable("");

//export const session = writable({ id: 0, markers : [], scene : {}, sceneId : 0 });
//await loadSession();

export const currentCampaign = writable();

const storedUserName = localStorage.getItem("userName");
export const userName = writable(storedUserName);
userName.subscribe(value => {
    localStorage.setItem("userName", value);
});

export const session = derived([userName], ($userName) => {
    console.log("username derived", $userName);
    return { id: 1, markers : [], scene : {}, sceneId : 11 };
}, { id: 0, markers : [], scene : {}, sceneId : 0 });





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
