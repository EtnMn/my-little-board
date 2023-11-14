<script setup>
const { data, error, pending, refresh } = await useFetch("/api/profiles/me");
const me = useMe();
me.value = data;
</script>

<template>
  <div>
    <mlb-navbar />
    <div v-if="!error && !pending" class="2xl:container 2xl:mx-auto p-4">
      <slot />
    </div>
    <div v-else-if="!pending">
      <mlb-message title="An error occurred">
        <template #image>
          <SvgoDesert class="text-8xl text-primary" />
        </template>

        {{ error }}

        <template #actions>
          <button class="btn btn-primary" @click="() => { refresh() }">
            Retry
          </button>
        </template>
      </mlb-message>
    </div>
  </div>
</template>
