import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '^/rag': {
        target: 'http://localhost:5000/',
        secure: false        
      },
      '^/hybrid-rag': {
        target: 'http://localhost:5000/',
        secure: false        
      }
    }
  }
})
