import { writable } from "svelte/store";

export const creatures = writable([]);
export const maps = writable([]);
export const scenes = writable([]);
export const stage = writable({});
export const session = writable({});
export const hubConnection = writable();
export const zoom = writable(1.0);

export const currentPlayer = writable({ id: 0, name: "", x:0, y:0 });
export const currentScene = writable({ map: null, creatures : null });

