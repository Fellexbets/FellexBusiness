<template>
  <div class="container">
    <h4>Create UserRelation</h4>

    <form @submit.prevent="userRelationCreate()">
      <div class="mb-3">
        <label for="inputUserRelationCreateUserId" class="form-label">UserId</label>
        <input v-model="userRelation.userId" type="userId" class="form-control" id="inputUserRelationCreateUserId">
      </div>
      <div class="mb-3">
        <label for="inputUserRelationCreateRelatedUserId" class="form-label">RelatedUserId</label>
        <input v-model="userRelation.relatedUserId" type="relatedUserId" class="form-control" id="inputUserRelationCreateRelatedUserId">
      </div>
      <div class="mb-3">
        <label for="inputUserRelationCreateRelationId" class="form-label">RelationId</label>
        <input v-model="userRelation.relationId" type="relationId" class="form-control" id="inputUserRelationCreateRelationId">
      </div>

      <button type="submit">Create</button>
      <button @click="back()">Back</button>
    </form>

  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useUserRelationStore } from "@/stores/";
import type { IUserRelation } from "@/models/";

export default defineComponent({
  data() {
    return {
      userRelation: {} as IUserRelation
    }
  },
  methods: {
    userRelationCreate() {
      this.userRelationStore.apiCreate(this.userRelation);
      this.$emit('changePage', 'UserRelationDetailsComponent');
    },
    back() {
      this.$emit('changePage', 'UserRelationIndexComponent');
    }
  },
  computed: {
    ...mapStores(useUserRelationStore)
  }
})
</script>