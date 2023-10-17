<script setup>
// Set current user state.
useSetMe(useSupabaseUser().value);
</script>

<template>
  <div>
    <top-navbar />
    <div class="2xl:container 2xl:mx-auto p-4">
      <slot />
    </div>
  </div>
</template>
