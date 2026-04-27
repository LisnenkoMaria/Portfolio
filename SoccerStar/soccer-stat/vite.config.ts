import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
    plugins: [react()],
    server: {
        proxy: {
            '/api': {
                target: 'https://api.football-data.org/v4',
                changeOrigin: true,
                rewrite: (path) => path.replace(/^\/api/, ''),
                headers: {
                    'X-Auth-Token': '05089575f34946c3bd4a0d0a7a0e4790'
                }
            }
        }
    }
})