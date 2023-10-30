<script lang="ts" setup>
const me = useMe();
const { pending, data: organizations } = useLazyFetch("/api/organizations");
const myOrganizations = computed(() => organizations.value?.filter(o => o.ownerId === me.value.userId) ?? []);
</script>

<template>
  <div v-if="pending">
    <mlb-loader />
  </div>
  <div v-else-if="myOrganizations.length > 0">
    <div v-for="item in organizations" :key="item.organizationId">
      <div class="text-xl font-bold mb-6 flex items-center gap-x-2">
        <SvgoBuilding />
        <h2>{{ item.name }}</h2>
      </div>
    </div>
  </div>
  <div v-else>
    <div class="flex flex-col items-center justify-center py-16 gap-8">
      <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="h-16 w-16 text-accent-content" viewBox="0 0 16 16">
        <path d="M13.879 10.414a2.501 2.501 0 0 0-3.465 3.465l3.465-3.465Zm.707.707-3.465 3.465a2.501 2.501 0 0 0 3.465-3.465Zm-4.56-1.096a3.5 3.5 0 1 1 4.949 4.95 3.5 3.5 0 0 1-4.95-4.95Z" />
        <path d="M2 1a1 1 0 0 1 1-1h10a1 1 0 0 1 1 1v6.5a.5.5 0 0 1-1 0V1H3v14h3v-2.5a.5.5 0 0 1 .5-.5H8v4H3a1 1 0 0 1-1-1V1Z" />
        <path d="M4.5 2a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm-6 3a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm-6 3a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Z" />
      </svg>
      <p class="text-xl">
        It looks like you haven't created an organization yet.
      </p>
      <NuxtLink to="/organization/new" class="btn btn-primary">
        Create a new one
      </NuxtLink>
    </div>
  </div>
</template>
