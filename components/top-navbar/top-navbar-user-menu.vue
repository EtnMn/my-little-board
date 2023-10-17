<script lang="ts" setup>
const { auth } = useSupabaseClient();
const me = useMe();

async function signOut() {
  const { error } = await auth.signOut();
  if (!error)
    return navigateTo("/login");
};
</script>

<template>
  <div class="dropdown dropdown-end">
    <label tabindex="0" class="btn btn-circle btn-link avatar">
      <div class="w-8 rounded-xl">
        <img :src="me.avatarUrl">
      </div>
    </label>
    <ul tabindex="0" class="menu dropdown-content z-[1] p-4 shadow bg-base-100 rounded-box w-52 mt-4">
      <template v-if="me.role === 'administrator' || me.role === 'manager'">
        <li>
          <NuxtLink to="/organization" @click="e => e.target.blur()">
            Your organization
          </NuxtLink>
        </li>
        <li class="h-px bg-neutral-300" />
      </template>
      <li>
        <button @click="signOut">
          Sign out
        </button>
      </li>
    </ul>
  </div>
</template>
