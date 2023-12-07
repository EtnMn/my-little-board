<script lang="ts" setup>
const { organizationId } = defineProps<{ organizationId: string }>();

const deleteError = ref(false);
const { pending, data: members, refresh } = await useLazyFetch(`/api/organizations/${organizationId}/members`);

async function onRemoveMember(organizationId?: string, memberId?: string) {
	const { error } = await useFetch<boolean>(`/api/organizations/${organizationId}/members/${memberId}`, {
		method: "delete",
	});

	if (error.value && error.value.statusCode !== 404)
		deleteError.value = !!error.value;
	else
		deleteError.value = false;

	refresh();
};
</script>

<template>
  <h2 class="text-xl mb-3 capitalize">
    Manage access
  </h2>
  <div class="overflow-x-auto">
    <table class="table border sm:table-fixed">
      <!-- head -->
      <thead>
        <tr class="bg-base-200">
          <th>Name</th>
          <th class="hidden sm:table-cell">
            Role
          </th>
          <th>
            Actions
          </th>
        </tr>
      </thead>
      <tbody>
        <template v-if="!pending && members && members?.length > 0">
          <tr v-for="m in members" :key="m.memberId">
            <td>
              <mlb-profile v-if="m.profile" :profile="m.profile" />
            </td>
            <td class="hidden sm:table-cell">
              User
            </td>
            <td>
              <button
                v-if="m.profile?.profileId !== m.organization?.ownerId"
                class="btn btn-ghost btn-xs"
                @click="onRemoveMember(m.organization?.organizationId, m.memberId)"
              >
                Remove
              </button>
            </td>
          </tr>
        </template>
        <template v-else-if="pending">
          <tr v-for="i in 3" :key="i">
            <td>
              <div class="flex items-center gap-3">
                <div class="skeleton w-8 h-8 rounded-full" />
                <div>
                  <div class="skeleton h-4 w-40" />
                </div>
              </div>
            </td>
            <td><div class="skeleton h-4 w-20" /></td>
            <td>
              <div class="skeleton h-4 w-20" />
            </td>
          </tr>
        </template>
        <tr v-else>
          <td colspan="3">
            <div class="flex justify-center">
              <svgo-user-question class="text-6xl opacity-60" filled />
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <div v-if="deleteError" role="alert" class="alert alert-error mt-3">
      <svg xmlns="http://www.w3.org/2000/svg" class="stroke-current shrink-0 h-6 w-6" fill="none" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
      <span>Could not remove user.</span>
      <div>
        <button class="btn btn-sm btn-ghost" @click="deleteError = false">
          Deny
        </button>
      </div>
    </div>
  </div>
</template>
