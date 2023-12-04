<script lang="ts" setup>
const { auth } = useSupabaseClient();
const open = ref(false);
const me = useMe();
async function signOut() {
	const { error } = await auth.signOut();

	if (!error)
		return navigateTo("/login");
};
</script>

<template>
  <div class="drawer drawer-end">
    <input id="mlb-drawer" :value="open" :checked="open" type="checkbox" class="drawer-toggle" @input="open = !open">
    <div class="drawer-content xl:container xl:mx-auto p-6">
      <!-- Page content here -->
      <slot />
    </div>
    <div class="drawer-side">
      <label for="mlb-drawer" aria-label="close sidebar" class="drawer-overlay" />
      <div class="menu p-2 w-80 min-h-full">
        <div class="bg-base-100 p-4 rounded-box grow shadow">
          <!-- Drawer content here -->
          <ul v-if="me">
            <template v-if="me.role === 'administrator' || me.role === 'manager'">
              <li>
                <NuxtLink to="/organization" @click="open = !open">
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
      </div>
    </div>
  </div>
</template>
