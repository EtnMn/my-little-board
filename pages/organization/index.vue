<script lang="ts" setup>
const { data: organizations } = await useFetch("/api/organizations");
</script>

<template>
  <div v-if="organizations && organizations.length > 0">
    <div v-for="item in organizations" :key="item.organizationId">
      <h1 class="inline-block text-2xl sm:text-3xl font-semibold mb-3 text-primary capitalize">
        Organization: {{ item.name }}
      </h1>
      <nuxt-error-boundary>
        <mlb-organization-members :organization-id="item.organizationId" />
        <template #error="{ error, clearError }">
          <mlb-message-box :border="false">
            <template #image>
              <svgo-desert class="text-8xl text-error" />
            </template>
            An error occured
            <template #text>
              {{ error }}
            </template>
            <template #action>
              <button class="btn btn-sm btn-accent" @click="clearError()">
                Retry
              </button>
            </template>
          </mlb-message-box>
        </template>
      </nuxt-error-boundary>
    </div>
  </div>

  <mlb-message-box v-else>
    <template #image>
      <SvgoHouse class="text-8xl text-primary" />
    </template>
    You don't have any organization yet
    <template #text>
      An organization contains your projects
    </template>
    <template #action>
      <NuxtLink to="/organization/new" class="btn btn-primary">
        Add organization
      </NuxtLink>
    </template>
  </mlb-message-box>
</template>
