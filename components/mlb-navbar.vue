<script setup>
const { auth } = useSupabaseClient();
const user = useSupabaseUser();
const open = ref(false);
const me = useMe();

async function signOut() {
  const { error } = await auth.signOut();

  if (!error)
    return navigateTo("/login");
};
</script>

<template>
  <div class="navbar sticky top-0 border-b bg-base-200 border-base-300">
    <div class="flex-1">
      <NuxtLink class="btn btn-ghost normal-case text-xl" to="/">
        <div class="font-title inline-flex text-lg md:text-2xl">
          <span class="text-primary mr-1">My</span>
          <span class="text-secondary mr-1">Little</span>
          <span class="text-accent">Board</span>
        </div>
      </NuxtLink>
    </div>

    <div class="navbar-end">
      <div v-if="user" class="flex items-stretch">
        <mlb-drawer v-model="open">
          <label for="mlb-drawer" tabindex="0" class="btn btn-circle btn-link avatar">
            <div class="w-8 rounded-xl">
              <img :src="me.avatarUrl">
            </div>
          </label>
          <template #drawer>
            <ul>
              <template v-if="me.role === 'administrator' || me.role === 'manager'">
                <li>
                  <NuxtLink to="/organization" @click="open = false">
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
          </template>
        </mlb-drawer>
      </div>
      <NuxtLink v-else class="btn btn-accent" to="/login">
        Sign In
      </NuxtLink>
    </div>
  </div>
</template>
