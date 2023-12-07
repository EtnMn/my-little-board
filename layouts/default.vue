<script setup>
const open = ref(false);
const { data, error, pending, refresh } = await useFetch("/api/profiles/me");
const me = useMe();
me.value = data;
</script>

<template>
  <div>
    <mlb-navbar />
    <div class="drawer drawer-end">
      <input id="mlb-drawer" v-model="open" type="checkbox" class="drawer-toggle">
      <div class="drawer-content xl:container xl:mx-auto p-6">
        <!-- Page content here -->
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
      </div>
      <mlb-drawer @close="open = false" />
    </div>
  </div>
</template>
