<template>
  <div class="container">
    <h4>Delete UserRelation</h4>
    <table class="table">
      <thead>
        <tr>
          <th>UserId</th>
          <th>RelatedUserId</th>
          <th>RelationId</th>
          <th>DateCreated</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ userRelation.userId }} </td>
          <td> {{ userRelation.relatedUserId }} </td>
          <td> {{ userRelation.relationId }} </td>
          <td> {{ userRelation.dateCreated }} </td>
        </tr>
      </tbody>
    </table>
    <p> Are you sure?</p>
    <button @click="userRelationDelete(userRelation.userId, userRelation.relatedUserId)">Delete</button>
    <button @click="back()">Back</button>
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
    userRelationDelete(themeId: number, userId: number) {
      this.userRelationStore.apiDelete(themeId, userId);
      this.$router.push(`/userRelationIndex`);
    },
    back() {
      this.$router.push(`/userRelationDetails/${this.userRelation.userId},${this.userRelation.relatedUserId}`);
    }
  },
  mounted() {
    this.userRelation = this.userRelationStore.getCurrentUserRelation;
  },
  computed: {
    ...mapStores(useUserRelationStore)
  },
  components: {
  }
})
</script>