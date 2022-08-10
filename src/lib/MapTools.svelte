<script>
    import { hubConnection, fog, zoom, isMaster } from '../stores';
    import { mdiOpenInNew, mdiFullscreen, mdiCloud, mdiCloudOutline } from "@mdi/js";
    import Icon from './Icon.svelte';
    
    function toggleZoom()
    {
        const levels = [0.5,1,1.5];
        let index = levels.indexOf($zoom);
        index++;
        if (index > levels.length-1) index = 0;
        $zoom = levels[index];
    }

    function toggleFullScreen()
    {
        if (document.fullscreenElement) {
            document.exitFullscreen();
        } else {
            
            document.documentElement.requestFullscreen();
        }
    }

    function popout()
    {
        openPopup(window.location.href, "OpenGMTStage", window.outerWidth, window.outerHeight,
            { toolbar:0, resizable:1, location:0, menubar:0, status:0, popup: 1 });
    }

    function openPopup(url, title, w, h, opts) 
    {
        var _innerOpts = '';
        if(opts !== null && typeof opts === 'object' ){
            for (var p in opts ) {
                if (opts.hasOwnProperty(p)) {
                    _innerOpts += p + '=' + opts[p] + ',';
                }
            }
        }
            // Fixes dual-screen position, Most browsers, Firefox
        var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
        var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

        var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
        var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

        var left = ((width / 2) - (w / 2)) + dualScreenLeft;
        var top = ((height / 2) - (h / 2)) + dualScreenTop;
        var newWindow = window.open(url, title, _innerOpts + ' width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

        // Puts focus on the newWindow
        if (window.focus) {
            newWindow.focus();
        }
    }

    function toggleFog()
    {
        $fog = !$fog
        $hubConnection.invoke('SetFog', $fog);
    }

</script>

<div class="absolute p-2 top-0 right-0 z-50 flex">
    <button on:click={toggleZoom} class="p-2 rounded-lg bg-slate-200 border-4 mr-2">{$zoom*100}%</button>
    <button on:click={toggleFullScreen} class="p-2 rounded-lg bg-slate-200 border-4 flex mr-2"><Icon path={mdiFullscreen} size=24 /></button>
    {#if window.name !== "OpenGMTStage" && $isMaster}
        <button on:click={popout} class="p-2 rounded-lg bg-slate-200 border-4 mr-2"><Icon path={mdiOpenInNew} size=24 /></button>
        <button on:click={toggleFog} class="p-2 rounded-lg bg-slate-200 border-4 mr-2"><Icon path={$fog ? mdiCloud : mdiCloudOutline } size=24 /></button>
    {/if}
</div>
