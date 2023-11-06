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
    <mlb-hero>
      <template #title>
        <div class="flex justify-center">
          <SvgoNoBuilding class="text-8xl" />
        </div>
      </template>
      It looks like you haven't created an organization yet.
      <template #actions>
        <NuxtLink to="/organization/new" class="btn btn-primary">
          Create a new one
        </NuxtLink>
      </template>
    </mlb-hero>
  </div>
</template>
