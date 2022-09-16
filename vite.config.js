import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [svelte()],
    build: {
        outDir: "wwwroot"
    },
    server: {
        port: 3000,
        proxy: {
            '/api': {
                target : 'https://localhost:7207', 
                secure: false
            },
            '/hubs/game': {
                target : 'https://localhost:7207', 
                secure: false,
                ws: true
            }
        }
    }
})
