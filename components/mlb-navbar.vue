<script setup>
const me = useMe();
</script>

<template>
  <div class="navbar sticky top-0 border-b bg-base-200 border-base-300">
    <div class="flex-1">
      <NuxtLink class="btn btn-ghost text-xl" to="/">
        <div class="font-title inline-flex text-lg md:text-2xl">
          <span class="text-primary mr-1">My</span>
          <span class="text-secondary mr-1">Little</span>
          <span class="text-accent">Board</span>
        </div>
      </NuxtLink>
    </div>

    <div class="navbar-end">
      <div v-if="me" class="flex items-stretch">
        <!-- <mlb-drawer v-model="open"> -->
        <label for="mlb-drawer" tabindex="0" class="btn btn-circle btn-link avatar">
          <div class="w-8 rounded-xl">
            <img :src="me.avatar">
          </div>
        </label>
      </div>
      <NuxtLink v-else-if="!user" class="btn btn-primary" to="/login">
        Sign In
      </NuxtLink>
    </div>
  </div>
</template>
