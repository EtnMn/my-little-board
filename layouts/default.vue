<script setup>
const { data, error, pending, refresh } = await useFetch("/api/profiles/me");
const me = useMe();
me.value = data;
</script>

<template>
  <div>
    <mlb-navbar />
    <mlb-content>
      <slot v-if="!error && !pending" />

      <div v-else-if="error">
        <mlb-message title="An error occurred">
          <template #image>
            <SvgoDesert class="text-8xl text-primary" />
          </template>

          {{ error }}

          <template #actions>
            <button class="btn btn-accent" @click="() => { refresh() }">
              Retry
            </button>
          </template>
        </mlb-message>
      </div>
      <div v-else>
        <mlb-loader />
      </div>
    </mlb-content>
  </div>
</template>
