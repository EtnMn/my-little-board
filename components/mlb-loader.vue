<template>
  <div class="w-full flex flex-col justify-center items-center py-8 gap-8">
    <span class="loading loading-spinner loading-lg text-primary" />
    <slot>
      <p class="text-sm">
        One moment please...
      </p>
    </slot>
  </div>
</template>
