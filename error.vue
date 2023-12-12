<script setup lang="ts">
defineProps({
	error: Object,
});

const handleError = () => clearError({ redirect: "/" });
</script>

<template>
  <div v-if="error">
    <h2>{{ error.statusCode }} - {{ error.message }}</h2>
    <button @click="handleError">
      Clear errors
    </button>
  </div>
</template>
