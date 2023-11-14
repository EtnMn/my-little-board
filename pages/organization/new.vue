<script lang="ts" setup>
// import { fromZodError } from "zod-validation-error";
import { useField, useForm } from "vee-validate";
import { toTypedSchema } from "@vee-validate/zod";
import type { Organization } from "~/types";

// import * as zod from "zod";

definePageMeta({
  middleware: "no-organization",
});
// const message = ref("");
// const org = { name: "" };
// const organizationSchema = useOrganizationSchema();
// const parsed = organizationSchema.safeParse(org);
// if (!parsed.success)
// if (isValidationErrorLike(err)) {
//     return 400; // Bad Data (this is a client error)
//   }
// message.value = fromZodError(parsed.error).message;

const validationSchema = toTypedSchema(useOrganizationSchema());

const { handleSubmit, errors, defineInputBinds } = useForm<Organization>({
  validationSchema,
  initialValues: { name: "coucou" },
});

const name = defineInputBinds("name", { validateOnInput: true });

const onSubmit = handleSubmit((values) => {
  alert(JSON.stringify(values, null, 2));
});
</script>

<template>
  <div class="md:mx-auto md:max-w-lg">
    <div class="border-b mb-2 pb-2">
      <h2 class="text-2xl font-semibold">
        Create a new organization
      </h2>
      <p class="text-sm opacity-60 leading-6">
        An organization contains all your projects, including the sprints history.
      </p>
    </div>
    <p class="text-sm opacity-60 mb-2">
      <em>Required fields are marked with an asterisk (*)</em>
    </p>
    <form class="form-control w-full" @submit="onSubmit">
      <label class="label">
        <span class="label-text">Organization name *</span>
      </label>
      <input v-bind="name" name="name" type="text" class="input input-bordered input-primary" data-1p-ignore>
      <label class="label">
        <span class="label-text-alt text-error">{{ errors.name }}</span>
      </label>
      <div class="flex justify-end">
        <button class="btn btn-primary">
          Create organization
        </button>
      </div>
    </form>
  </div>
</template>
