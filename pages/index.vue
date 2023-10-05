<script lang="ts" setup>
const { auth } = useSupabaseClient();

async function signOut() {
  const { error } = await auth.signOut();
  if (!error)
    return navigateTo("/login");
}
</script>

<template>
  <button @click="signOut()">
    Sign out
  </button>
</template>

<style>

</style>
