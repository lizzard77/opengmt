import { session, scenes, creatures, maps, currentScene, currentMarker, markers, sounds, handouts } from "./stores";
import { get } from "svelte/store";
import { get as apiGet, putObject } from "./api";
import { hubConnection } from "./hub";

hubConnection.on("session", (e) => 
    {
        console.log("session", JSON.parse(e));
        loadSession();
        /*
        const sess = JSON.parse(e);
        session.set(sess);
        currentScene.set(sess.scene);
        markers.set(sess.markers);

        const id = get(currentMarker).creatureId;
        currentMarker.set(get(markers).find(m => m.creatureId === id));    
        */
    });

hubConnection.on("marker", (e) => 
    {
        console.log("marker", JSON.parse(e));
        
        const markerInfo = JSON.parse(e);
        
        let currentList = get(markers);    
        const otherMarkers = currentList.filter(c => c.creatureId !== markerInfo.creatureId) || [];    
        markers.set([ ...otherMarkers, markerInfo ]);
        console.log(get(markers))
        
        const id = get(currentMarker).creatureId;
        currentMarker.set(get(markers).find(m => m.creatureId === id));    
    });

hubConnection.on("setCurrentPlayer", e =>
    {
        console.log("setCurrentPlayer", JSON.parse(e));
        const { creatureId } = JSON.parse(e);
        currentMarker.set(get(markers).find(m => m.creatureId === creatureId));
    });

    

export async function updateState(p)
{
    let currentList = get(markers);    
    const otherMarkers = currentList.filter(c => c.creatureId !== p.creatureId) || [];    
    markers.set([ ...otherMarkers, p ]);

    await putObject("/api/marker", p);
}

export async function loadSession()
{
    const sess = await apiGet("/api/session");    
    if (sess && sess.scene)
    {
        console.log("loaded session", sess);
        session.set(sess);
        currentScene.set(sess.scene);
        markers.set(sess.scene.markers);
        sounds.set(sess.scene.assets.filter(a => a.type === 2));
        handouts.set(sess.scene.assets.filter(a => a.type !== 2));
        currentMarker.set(sess.scene.markers[0]);
    }
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
