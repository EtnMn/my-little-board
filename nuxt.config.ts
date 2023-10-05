// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  app: {
    head: {
      title: "My Little Board",
      link: [{ rel: "icon", type: "image/jpg", href: "./favicon.ico" }],
    },
  },
  devtools: { enabled: true },
  modules: ["@nuxtjs/tailwindcss", "@nuxtjs/supabase", "nuxt-svgo"],
  runtimeConfig: {
    public: {
      websiteBaseUrl: "",
    },
  },
  typescript: {
    shim: false,
  },
});
