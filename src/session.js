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
    const sess = await apiGet("/api/session");
    
    if (sess && sess.scene)
    {
        console.log("loaded session", sess)
        session.set(sess);
        currentScene.set(sess.scene);
        currentPlayer.set(sess.scene.creatures[0]);    
    }
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
