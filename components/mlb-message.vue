<script lang="ts" setup>
type Props = {
  title?: string;
  image?: string;
};

const { title = "" } = defineProps<Props>();
</script>

<template>
  <div class="flex pt-12 justify-center text-center">
    <div class="max-w-md">
      <div v-if="$slots.image" class="flex justify-center">
        <slot name="image" />
      </div>

      <h2 v-if="title || $slots.title" class="text-2xl font-bold">
        <slot name="title">
          {{ title }}
        </slot>
      </h2>
      <p class="py-9">
        <slot />
      </p>
      <slot name="actions" />
    </div>
  </div>
</template>
