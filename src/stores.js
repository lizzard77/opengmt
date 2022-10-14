import * as signalR from "@microsoft/signalr";
import { writable, derived, get as getStoreValue } from "svelte/store";
import { get, putObject } from "./api";

const storedUserName = localStorage.getItem("userName");
export const userName = writable(storedUserName);
userName.subscribe(value => {
    localStorage.setItem("userName", value);
});

export const activeSection = writable("");
export const statsEditing = writable(false);

function createApiStore(endpoint) 
{
    const { subscribe, set, update } = writable();
	return {
		subscribe,
        set,
        update,
        loadAll: async () => set(await get("/api/" + endpoint, (e) => console.log("error loading " + endpoint, e))),
        load: async (id) => await get("/api/" + id, (e) => console.log("error loading " + endpoint, e)),
        save: async (data) => await putObject("/api/" + endpoint, data, (e) => console.log("error loading " + endpoint, e))
    }
};

export const creatures = createApiStore("creatures");
export const maps = createApiStore("maps");
export const scenes = createApiStore("scenes");

function createCampaignStore() 
{
    const { subscribe, set, update } = writable();
	return {
		subscribe,
        set,
        update,
        loadAll: async () => await get("/api/campaigns"),
        load: async (id) => {
            set(await get("/api/campaigns/" + id));
            localStorage.setItem("campaignId", id.toString());
        },
        reload: async () => set(await get("/api/campaigns/" + getStoreValue(currentCampaign).id))
	};
}
export const currentCampaign = createCampaignStore();
export const currentScene = derived(currentCampaign, ($currentCampaign) => $currentCampaign.currentScene);
export const isMaster = derived([currentCampaign, userName], ([$currentCampaign, $userName]) => {
    if ($userName && $currentCampaign?.players?.length)
    {
        const player = $currentCampaign.players.find(p => p.name === $userName);
        return $currentCampaign.dmPlayerId === player.id;
    }
    return false;
});
export const markers = derived(currentScene, ($currentScene) => $currentScene?.markers);
export const sounds = derived(currentScene, ($currentScene) => $currentScene?.assets?.filter(a => a.type === 2));
export const handouts = derived(currentScene, ($currentScene) => $currentScene?.assets?.filter(a => a.type !== 2));

function createSoundPlayerStore()
{
    const { subscribe, set, update } = writable([]);
	return {
		subscribe,
        set,
        update,        
        add: (sound, autoStart = false) => { 
            update((players) => {
                const file = new Audio(sound.uri);
                const player = {
                    ...sound,
                    file,
                    play: () => { file.play(); update((players) => players); },
                    pause: () => { file.pause(); update((players) => players); },
                    isPlaying : () => !file.paused,
                    toggle: () => {
                        if (file.duration && !file.paused)
                            file.pause();
                        else
                            file.play();
                    },
                    seek: (time) => file.playTime = time,
                    toggleLoop : () => file.loop = !file.loop,
                    playTime : "?"
                };
                players.push(player);

                file.addEventListener('timeupdate', () => {
                    const m = "0" + Math.floor(file.currentTime / 60);
                    const s = "0" + Math.floor(file.currentTime % 60);
                    const ts = m.slice(-2) + ":" + s.slice(-2);
                    if (player.playTime !== ts)
                    {
                        player.playTime = ts;
                        update((players) => players);
                    }
                });
                if (autoStart)
                    player.play();
                return players;
            })
        }
	};
}
export const soundPlayers = createSoundPlayerStore();

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
