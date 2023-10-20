<script setup>
// Set current user state.
useSetMe(useSupabaseUser().value);
</script>

<template>
  <div>
    <mlb-navbar />
    <div class="2xl:container 2xl:mx-auto p-4">
      <slot />
    </div>
  </div>
</template>
