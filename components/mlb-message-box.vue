<script lang="ts" setup>
const { border = true } = defineProps<{ border?: boolean }>();
</script>

<template>
  <div class="py-8" :class="{ border }">
    <div class="max-w-md mx-auto overflow-hidden md:max-w-md">
      <div v-if="$slots.image" class="flex justify-center">
        <slot name="image" />
      </div>

      <div class="flex justify-center mt-3">
        <span class="text-lg">
          <slot />
        </span>
      </div>

      <p v-if="$slots.text" class="px-16 text-center opacity-60">
        <slot name="text" />
      </p>

      <div v-if="$slots.action" class="px-14 mt-5 flex justify-center">
        <slot name="action" />
      </div>
    </div>
  </div>
</template>
