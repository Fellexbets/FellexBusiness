<template>
  <div class="container">
    <h4>UserRelation Details</h4>
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
    <button @click="userRelationEdit(userRelation)">Edit</button>
    <button @click="userRelationDelete(userRelation)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
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
    userRelationEdit(userRelation : IUserRelation){
      this.userRelationStore.setCurrentUserRelation(userRelation);
      this.$router.push(`/userRelationEdit/${userRelation.userId},${userRelation.relatedUserId}`)
    },
    userRelationDelete(userRelation: IUserRelation){
      this.userRelationStore.setCurrentUserRelation(userRelation);
      this.$router.push(`/userRelationDelete/${userRelation.userId},${userRelation.relatedUserId}`)
    },
    back() {
      this.$router.push(`/userRelationIndex`);
    },

  },
  mounted() {
   this.userRelation = this.userRelationStore.getCurrentUserRelation;
  },
  computed: {
    ...mapStores(useUserRelationStore)
  }
})
</script>