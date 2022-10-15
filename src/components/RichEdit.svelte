<script>

import EditorJS from '@editorjs/editorjs';
import Header from '@editorjs/header'; 
import List from '@editorjs/list'; 
import ImageTool from '@editorjs/image';

export let data;

const editor = new EditorJS({ 
  /** 
   * Id of Element that should contain the Editor 
   */ 
  holder: 'editorjs', 
  
  /** 
   * Available Tools list. 
   * Pass Tool's class or Settings object for each Tool you want to use 
   */ 
  tools: 
  { 
    header: Header, 
    list: List,
    image: {
        class: ImageTool,
        config: {
            endpoints: {
                byFile: '/api/upload', // Your backend file uploader endpoint
                byUrl: 'http://localhost:8008/fetchUrl', // Your endpoint that provides uploading by Url
            }, 
            field: "files",
            additionalRequestData: { "folder" : "test" }
        }
    }
    },
    data

})

function save()
{
    editor.save().then((outputData) => {
       console.log('Article data: ', outputData)
    }).catch((error) => {
        console.log('Saving failed: ', error)
    });
}
</script>

<button on:click={save}>Save</button>
<div id="editorjs"></div>

<style>
    :global(h2) { @apply font-bold; }
</style>
