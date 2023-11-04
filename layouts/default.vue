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
      <div class="hero h-[calc(100vh-4rem-1px)]">
        <div class="hero-content text-center flex-col lg:flex-row">
          <img src="~/assets/img/my-little-board.png" alt="logo" class="max-w-sm rounded-lg shadow-sm">
          <div>
            <h2 class="text-5xl font-bold">
              An error occurred
            </h2>
            <p class="py-6">
              {{ error }}
            </p>
            <button class="btn btn-primary" @click="() => { refresh() }">
              Retry
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
