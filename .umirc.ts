import { defineConfig } from "@umijs/max";

export default defineConfig({
  npmClient: 'npm',
  request: {},
  model: {},
  proxy: {
    '/api': {
      target: 'http://127.0.0.1:15301',
      changeOrigin: true,
      pathRewrite: { '^': ''}
    }
  },
  routes: [
    {path: '/', component: 'index'},
    {path: '/docs', component: 'docs'},
    {path: '/student/edit/:id', component: 'student/edit/[id]'},
  ]
});
