<script lang="ts" setup>
definePageMeta({
  middleware: "no-organization",
});

function someErrorLogger(err) {
  console.log("got an error", err);
}

function onCreate(value: string) {
  console.log("new", value);
}
</script>

<template>
  <div class="md:mx-auto md:max-w-lg">
    <div class="border-b mb-2 pb-2">
      <h2 class="text-2xl font-semibold">
        Create a new organization
      </h2>
      <p class="text-sm opacity-60 leading-6">
        An organization contains all your projects, including the sprint history.
      </p>
    </div>
    <p class="text-sm opacity-60 mb-2">
      <em>Required fields are marked with an asterisk (*)</em>
    </p>
    <NuxtErrorBoundary @error="someErrorLogger($event)">
      <mlb-organization-create @create="(x) => onCreate(x)" />

      <template #error="{ clearError }">
        <div role="alert" class="alert alert-error">
          <svg xmlns="http://www.w3.org/2000/svg" class="stroke-current shrink-0 h-6 w-6" fill="none" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
          <div>
            <h3 class="font-bold">
              An error occurred!
            </h3>
            <div class="text-xs">
              Please retry later.
            </div>
          </div>
          <div>
            <button class="btn btn-sm btn-ghost" @click="clearError()">
              Retry
            </button>
          </div>
        </div>
      </template>
    </NuxtErrorBoundary>
  </div>
</template>
