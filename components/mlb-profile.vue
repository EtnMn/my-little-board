<script lang="ts" setup>
import type { Profile } from "~/types";

type Props = {
	profile: Omit<Profile, "role">;
};

const { profile } = defineProps<Props>();
</script>

<template>
  <div class="flex items-center gap-3">
    <div class="avatar">
      <div class="rounded-full w-8">
        <img :src="profile?.avatar" :alt="`${profile?.name}'s avatar`">
      </div>
    </div>
    <div>
      <div class="font-bold">
        {{ profile?.name }}
      </div>
      <div class="text-sm opacity-50">
        {{ profile?.email }}
      </div>
    </div>
  </div>
</template>
