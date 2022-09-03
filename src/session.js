import { session, scenes, creatures, maps, currentScene, currentPlayer } from "./stores";
import { get } from "svelte/store";
import { get as apiGet, putObject } from "./api";
import { hubConnection } from "./hub";

hubConnection.on("sessionInfo", (e) => {
    console.log("sessionInfo", JSON.parse(e));
    session.set(JSON.parse(e));
});

export async function updateState(p)
{
    let s = get(session);
    const states = s.creatureStates.filter(c => c.creatureId !== p.creatureId);
    const currentState = s.creatureStates.find(c => c.creatureId === p.creatureId) || p;

    currentState.x = p.x;
    currentState.y = p.y;
    currentState.initiative = p.initiative;
    currentState.visible = p.visible;
    currentState.light = p.light;

    const sessionBody = { ...s, creatureStates : [ ...states, currentState ] };
    await putObject("/api/session", sessionBody);
    session.set(sessionBody);
}

export async function loadSession()
{
    const s = await apiGet("/api/session");
    let scene;
    if (s.sceneId)
        scene = loadScene(s.sceneId);

    if (s.creatureStates && scene)
    {
        s.creatureStates.forEach(c => {
            const creature = scene.creatures.find(ccc => ccc.id === c.creatureId);
            if (creature)
            {
                c.color = creature.color;
                c.size = creature.size;
                c.reach = creature.reach;
            }
        });
    }
    
    session.set(s);
    console.log("loaded session", s)
}

export function getState(id)
{
    let s = get(session);
    const defaultValues = 
    {
      "creatureId": id,
      "x": 1,
      "y": 1,
      "initiative": -1,
      "visible": false,
      "visionNormal": 10,
      "visionDim": 10,
      "light": false,
      "reach" : 5,
      "size" : 5,
      "color" : "white"
    }
    return s.creatureStates.find(c => c.creatureId === id) || defaultValues;
}

function loadScene(id)
{
    const s = get(scenes).find(s => s.id === id);
    s.map = get(maps).find(m => m.id === s.mapId);
    s.creatures = s.creatureIds.map(c => {
        return get(creatures).find(cc => cc.id === c);
    });
    currentScene.set(s);
    currentPlayer.set(s.creatures[0]);    
    return s;
}

    