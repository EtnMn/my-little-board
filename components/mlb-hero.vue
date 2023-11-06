<script lang="ts" setup>
type Props = {
  title?: string;
  image?: string;
};

const { title = "", image = "" } = defineProps<Props>();
</script>

<template>
  <div class="hero h-[calc(100vh-6rem-1px)]">
    <div class="hero-content text-center flex-col lg:flex-row">
      <img v-if="image" :src="image" alt="logo" class="max-w-sm rounded-lg shadow-sm">
      <div>
        <h2 v-if="title || $slots.title" class="text-5xl font-bold">
          <slot name="title">
            {{ title }}
          </slot>
        </h2>
        <p class="py-6">
          <slot />
        </p>
        <slot name="actions" />
      </div>
    </div>
  </div>
</template>
