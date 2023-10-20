<script lang="ts" setup>
const { modelValue } = defineProps<{ modelValue: boolean }>();
defineEmits<{ "update:modelValue": [value: boolean] }>();
</script>

<template>
  <div class="drawer drawer-end">
    <input id="mlb-drawer" :value="modelValue" :checked="modelValue" type="checkbox" class="drawer-toggle" @input="$emit('update:modelValue', !modelValue)">
    <div class="drawer-content">
      <!-- Page content here -->
      <slot />
    </div>
    <div class="drawer-side">
      <label for="mlb-drawer" aria-label="close sidebar" class="drawer-overlay" />
      <div class="menu p-2 w-80 min-h-full">
        <div class="bg-base-100 p-4 rounded-box grow shadow">
          <!-- Drawer content here -->
          <slot name="drawer" />
        </div>
      </div>
    </div>
  </div>
</template>
