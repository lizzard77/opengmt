import { writable } from "svelte/store";

export const creatures = writable([]);
export const maps = writable([]);
export const scenes = writable([]);
export const stage = writable({});

export const currentPlayer = writable({ name: "", x:0, y:0 });
export const currentScene = writable({ map: null, creatures : null });

