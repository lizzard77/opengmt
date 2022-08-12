import { writable } from "svelte/store";

export const creatures = writable([]);
export const maps = writable([]);
export const sounds = writable([]);
export const scenes = writable([]);
export const stage = writable({});
export const session = writable({});
export const hubConnection = writable();
export const zoom = writable(1.0);
export const fog = writable(true);
export const isMaster = writable(false);
export const squareSizeInPx = writable(1.0);
export const combat = writable(false);

export const currentPlayer = writable({ id: 0, name: "", x:0, y:0 });
export const currentScene = writable({ map: null, creatures : null });

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