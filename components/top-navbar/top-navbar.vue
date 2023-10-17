<script setup>
const user = useSupabaseUser();
</script>

<template>
  <div class="navbar sticky top-0 border-b border-base-300">
    <div class="flex-1">
      <NuxtLink class="btn btn-ghost normal-case text-xl" to="/">
        <div class="font-title inline-flex text-lg md:text-2xl">
          <span class="text-primary mr-1">My</span>
          <span class="text-secondary mr-1">Little</span>
          <span class="text-accent">Board</span>
        </div>
      </NuxtLink>
    </div>

    <div class="navbar-end">
      <div v-if="user" class="flex items-stretch">
        <top-navbar-user-menu />
      </div>
      <NuxtLink v-else class="btn btn-accent" to="/login">
        Sign In
      </NuxtLink>
    </div>
  </div>
</template>
