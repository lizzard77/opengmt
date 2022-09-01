import * as signalR from "@microsoft/signalr";
import { writable } from "svelte/store";

export const creatures = writable([]);
export const maps = writable([]);
export const sounds = writable([]);
export const scenes = writable([]);
export const stage = writable({});
export const session = writable({});
export const fog = writable(true);

const storedZoom = localStorage.getItem("zoom") || 1.0;
export const zoom = writable(storedZoom);
zoom.subscribe(value => {
    localStorage.setItem("zoom", value);
});

const storedIsMaster = JSON.parse(localStorage.getItem("isMaster") || "false");
export const isMaster = writable(storedIsMaster);
isMaster.subscribe(value => {
    localStorage.setItem("isMaster", JSON.stringify(value));
});

export const squareSizeInPx = writable(1.0);
export const combat = writable(false);

export const currentPlayer = writable({ id: 0, name: "", x:0, y:0 });
export const currentScene = writable({ map: null, creatures : null });

export const currentHandout = writable("");
export const audioFile = writable();

export const showPIP = writable(false);

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
