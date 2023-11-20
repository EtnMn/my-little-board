<script lang="ts" setup>
import { useForm } from "vee-validate";
import { toTypedSchema } from "@vee-validate/zod";
import type { Organization } from "~/types";

const emit = defineEmits<{ "create": [organizationName: string] }>();

const validationSchema = toTypedSchema(useOrganizationSchema());

const { handleSubmit, errors, defineInputBinds } = useForm<Organization>({
  validationSchema,
  initialValues: { name: "" },
});

const name = defineInputBinds("name", { validateOnInput: true });
const onSubmit = handleSubmit(async (values) => {
  const { data, error } = await useFetch<Organization>("/api/organizations", {
    method: "post",
    body: values,
  });

  if (error.value)
    throw error.value;

  emit("create", data.value?.name ?? "");
});
</script>

<template>
  <form class="form-control w-full" @submit="onSubmit($event)">
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
</template>
