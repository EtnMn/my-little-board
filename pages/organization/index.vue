<script lang="ts" setup>
const { pending, data: organizations } = useLazyFetch("/api/organizations");
</script>

<template>
  <div v-if="pending">
    <mlb-loader />
  </div>
  <div v-else-if="organizations && organizations.length > 0">
    <div v-for="item in organizations" :key="item.organizationId">
      <h1 class="inline-block text-2xl sm:text-3xl font-semibold mb-3 text-primary capitalize">
        Organization: {{ item.name }}
      </h1>
      <mlb-organization-members :organization-id="item.organizationId" />
    </div>
  </div>
  <div v-else>
    <mlb-message title="You don't have any organization yet.">
      <template #image>
        <SvgoHouse class="text-8xl text-primary" />
      </template>

      An organization contains your projects.

      <template #actions>
        <NuxtLink to="/organization/new" class="btn btn-primary">
          Create a new one
        </NuxtLink>
      </template>
    </mlb-message>
  </div>
</template>
