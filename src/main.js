import "./app.css";
import App from "./App.svelte";

window.onYouTubeIframeAPIReady = () =>
    {
        console.log("3")    
        window.dispatchEvent(new Event("iframeApiReady"));
    }
    
    
const app = new App({
  target: document.getElementById("app"),
});

export default app;
