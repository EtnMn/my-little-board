<script lang="ts" setup>
defineEmits<{
	close: [];
}>();

const { auth } = useSupabaseClient();
const me = useMe();
async function signOut() {
	const { error } = await auth.signOut();

	if (!error)
		return navigateTo("/login");
};
</script>

<template>
  <div class="drawer-side">
    <label for="mlb-drawer" aria-label="close sidebar" class="drawer-overlay" />
    <div class="menu p-2 w-80 min-h-full">
      <div class="bg-base-100 p-4 rounded-box grow shadow">
        <mlb-profile v-if="me" class="mb-3" :profile="me" />
        <!-- Drawer content here -->
        <ul v-if="me">
          <template v-if="me.role === 'administrator' || me.role === 'manager'">
            <li>
              <NuxtLink to="/organization" @click="$emit('close')">
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
</template>
