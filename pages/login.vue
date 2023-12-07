<script setup lang="ts">
definePageMeta({
  layout: "simple",
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
  <div class="bg-base-200 flex justify-center items-center h-screen">
    <div class="card w-96 bg-base-100 shadow-sm">
      <figure><img src="~/assets/img/my-little-board.png" alt="logo"></figure>
      <div class="card-body">
        <h2 class="card-title">
          My Little Board!
        </h2>
        <p class="text-sm">
          Sign in with
        </p>
        <div class="card-actions">
          <button class="btn btn-block bg-base-200" @click="signIn()">
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
</template>
