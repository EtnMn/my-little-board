<script lang="ts" setup>
const { organizationId } = defineProps<{ organizationId: string }>();

const { pending, data: members } = useLazyFetch(`/api/organizations/${organizationId}/members`);
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
          <th class="hidden sm:block">
            Role
          </th>
          <th>
            Actions
          </th>
        </tr>
      </thead>
      <tbody>
        <template v-if="!pending && members">
          <!-- row 1 -->
          <tr v-for="m in members" :key="m.memberId">
            <td>
              <div class="flex items-center gap-3">
                <div class="avatar">
                  <div class="mask mask-squircle w-8 h-8">
                    <img :src="m.profile?.avatar" :alt="`${m.profile?.name} avatar`">
                  </div>
                </div>
                <div>
                  <div class="font-bold">
                    {{ m.profile?.name }}
                  </div>
                  <div class="text-sm opacity-50">
                    {{ m.profile?.email }}
                  </div>
                </div>
              </div>
            </td>
            <td class="hidden sm:block">
              User
            </td>
            <td>
              <button v-if="m.profile?.profileId !== m.organization?.ownerId" class="btn btn-ghost btn-xs">
                Remove
              </button>
            </td>
          </tr>
        </template>
        <template v-else>
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
      </tbody>
    </table>
  </div>
</template>
