export default defineNuxtRouteMiddleware(async () => {
  const { data: organizations } = await useFetch("/api/organizations");
  if (organizations.value && organizations.value.length > 0)
    return await navigateTo("/organization");
});
