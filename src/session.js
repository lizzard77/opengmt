import { scenes, creatures, maps, currentScene, currentMarker, markers, sounds, handouts, currentCampaign } from "./stores";
import { get } from "svelte/store";
import { get as apiGet, putObject } from "./api";
import { hubConnection } from "./hub";

hubConnection.on("session", (e) => 
    {
        //console.log("session", JSON.parse(e));
        //loadSession();
        /*
        const sess = JSON.parse(e);
        session.set(sess);
        currentScene.set(sess.scene);
        markers.set(sess.markers);

        const id = get(currentMarker).creatureId;
        currentMarker.set(get(markers).find(m => m.creatureId === id));    
        */
    });

hubConnection.on("marker", async (e) => 
    {
        const id = get(currentCampaign).id;
        currentCampaign.set(await apiGet("/api/campaigns/" + id));
    });

hubConnection.on("setCurrentPlayer", async  (e) =>
    {
        const id = get(currentCampaign).id;
        currentCampaign.set(await apiGet("/api/campaigns/" + id));
    });

export async function updateState(p)
{
    let currentList = get(markers);    
    const otherMarkers = currentList.filter(c => c.creatureId !== p.creatureId) || [];    
    //markers.set([ ...otherMarkers, p ]);
    await putObject("/api/marker", p);
    const id = get(currentCampaign).id;
    currentCampaign.set(await apiGet("/api/campaigns/" + id));
}

export function getState(id)
{    
    const defaultValues = 
    {
        "creatureId": id,
        "x": 1,
        "y": 1,
        "initiative": -1,
        "visible": false,
        "visionNormal": 10,
        "visionDim": 10,
        "light": false
    }
    
    const scene = get(currentScene);
    const creature = get(creatures).find(c => c.id === id);
    const marker = get(markers).find(c => c.creatureId === id) || defaultValues;
    marker.color = creature.color;
    marker.size = creature.size;
    marker.reach = creature.reach;
    marker.sceneId = scene.id;
    marker.mapId = scene.map.id;
    return marker;
}
