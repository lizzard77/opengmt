import * as signalR from "@microsoft/signalr";
import { writable } from "svelte/store";
import { loadSession } from "./session";

export const activeSection = writable("");
export const statsEditing = writable(false);

export const creatures = writable([]);
export const maps = writable([]);
export const sounds = writable([]);
export const scenes = writable([]);
export const stage = writable({});
export const fog = writable(true);


const storedZoom = parseFloat(localStorage.getItem("zoom")) || 1.0;
export const zoom = writable(storedZoom);
zoom.subscribe(value => {
    localStorage.setItem("zoom", JSON.stringify(value));
});

const storedIsMaster = JSON.parse(localStorage.getItem("isMaster") || "false");
export const isMaster = writable(storedIsMaster);
isMaster.subscribe(value => {
    localStorage.setItem("isMaster", JSON.stringify(value));
});

export const squareSizeInPx = writable(1.0);
export const combat = writable(false);

export const currentMarker = writable({ id: 0, name: "", x:0, y:0, reach : 1, visible : true, light : true, creatureId : -1, initiative : -1, color : "pink" });
export const currentScene = writable({ id: 0, map: null, creatures : null, name : null, description : null, strongStart : null, secretsAndHints : null, phantasticLocations : null });

export const currentHandout = writable("");

export const showPIP = writable(false);

export const session = writable({ markers : [] });
await loadSession();


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
