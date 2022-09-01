import * as signalR from "@microsoft/signalr";

const hubConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();
await hubConnection.start();

export { hubConnection };