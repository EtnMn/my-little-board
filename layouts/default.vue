<script setup>
const open = ref(false);
const { data, error, pending, refresh } = await useFetch("/api/profiles/me");
const me = useMe();
me.value = data;
</script>

<template>
  <div>
    <nuxt-loading-indicator :height="4" />
    <mlb-navbar />
    <div class="drawer drawer-end">
      <input id="mlb-drawer" v-model="open" type="checkbox" class="drawer-toggle">
      <div class="drawer-content xl:container xl:mx-auto p-6">
        <!-- Page content here -->
        <slot v-if="!error && !pending" />

        <div v-else-if="error">
          <mlb-message-box :border="false">
            <template #image>
              <svgo-desert class="text-8xl text-primary" />
            </template>

            An error occurred

            <template #text>
              {{ error }}
            </template>
            <template #action>
              <button class="btn btn-accent" @click="refresh()">
                Retry
              </button>
            </template>
          </mlb-message-box>
        </div>
        <div v-else>
          <mlb-loader />
        </div>
      </div>
      <mlb-drawer @close="open = false" />
    </div>
  </div>
</template>
