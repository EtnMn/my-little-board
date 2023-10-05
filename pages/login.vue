<script setup lang="ts">
definePageMeta({
  middleware: ["sign-in"],
});

const user = useSupabaseUser();
const { auth } = useSupabaseClient();
const redirectTo = `${useRuntimeConfig().public.websiteBaseUrl}/confirm`;
const errorMessage = ref("");

async function signIn() {
  const { error } = await auth.signInWithOAuth({
    provider: "github",
    options: { redirectTo },
  });

  errorMessage.value = error?.message ?? "";
}

watchEffect(() => {
  if (user.value)
    navigateTo("/");
});
</script>

<template>
  <div class="bg-gray-100 flex justify-center items-center h-screen">
    <div class="w-1/2 h-screen hidden lg:block">
      <img src="~/assets/img/my-little-board.png" alt="My little board placeholder" class="object-cover w-full h-full">
    </div>
    <div class="lg:p-36 md:p-52 sm:20 p-8 w-full lg:w-1/2 flex flex-col items-center">
      <h1 class="text-2xl text-primary font-semibold mb-4">
        My Little Board
      </h1>
      <div class="card bg-base-100 w-96">
        <div class="card-body">
          <p class="mb-2 text-info-content">
            Sign in with
          </p>
          <div class="card-actions">
            <button class="btn btn-block" @click="signIn()">
              <SvgoGithub class="text-xl" />
              Github
            </button>
            <div v-if="errorMessage" class="error">
              errorMessage
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
