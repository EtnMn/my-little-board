<script setup lang="ts">
definePageMeta({
  layout: "simple",
  middleware: ["sign-in"],
});

const user = useSupabaseUser();

watch(user, () => {
  if (user.value)
    return navigateTo("/");
}, { immediate: true });
</script>

<template>
  <div class="text-info-content">
    Waiting for login...
  </div>
</template>
