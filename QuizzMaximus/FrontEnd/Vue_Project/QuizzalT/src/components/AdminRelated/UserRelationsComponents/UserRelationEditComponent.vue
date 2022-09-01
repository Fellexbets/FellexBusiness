<template>
  <div class="container">
    <h4>Edit UserRelation</h4>

    <form @submit.prevent="userRelationUpdate()">
      <div class="mb-3">
        <label for="inputUserRelationEditUserId" class="form-label">Current UserId: {{ userRelationToDisplayCurrent.userId }}</label>
        <input v-model="userRelation.userId" type="userId" class="form-control" id="inputUserRelationEditUserId">
      </div>

      <div class="mb-3">
        <label for="inputUserRelationEditRelatedUserId" class="form-label">Current RelatedUserId: {{ userRelationToDisplayCurrent.relatedUserId }}</label>
        <input v-model="userRelation.relatedUserId" type="relatedUserId" class="form-control" id="inputUserRelationEditRelatedUserId">
      </div>

      <div class="mb-3">
        <label for="inputUserRelationEditRelationId" class="form-label">Current RelationId: {{ userRelationToDisplayCurrent.relationId }}</label>
        <input v-model="userRelation.relationId" type="relationId" class="form-control" id="inputUserRelationEditRelationId">
      </div>

      <button type="submit">Update</button>
      <button @click="back(userRelation.userId, userRelation.relatedUserId)">Back</button>
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
      userRelation: {} as IUserRelation,
      userRelationToDisplayCurrent: {} as IUserRelation
    }
  },
  methods: {
    userRelationUpdate() {
      this.userRelationStore.apiUpdate(this.userRelation);
      this.$emit('changePage', 'UserRelationDetailsComponent');
    },
    back(userId: number, relatedUserId: number) {
      this.$emit('changePage', 'UserRelationIndexComponent');
    }
  },
  mounted() {
    this.userRelation = this.userRelationStore.getCurrentUserRelation;
    this.userRelationToDisplayCurrent.userId = this.userRelation.userId;
    this.userRelationToDisplayCurrent.relatedUserId = this.userRelation.relatedUserId;
    this.userRelationToDisplayCurrent.relationId = this.userRelation.relationId;
  },
  computed: {
    ...mapStores(useUserRelationStore)
  }
})
</script>