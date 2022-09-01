<template>
    <div class="container">
      <ModelTitle title="UserRelation Index" />
      <table class="table">
        <thead>
          <tr>
            <th>UserId</th>
            <th>RelatedUserId</th>
            <th>RelationId</th>
            <th>DateCreated</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, userId)  in userRelationStore.getUserRelations" :key="userId">
            <td> {{ item.userId }} </td>
            <td> {{ item.relatedUserId }} </td>
            <td> {{ item.relationId }} </td>
            <td> {{ item.dateCreated }} </td>
            <td>
              <button class="crudBtn" @click="userRelationDetails(item)">Details</button>
              <button class="crudBtn" @click="userRelationEdit(item)">Edit</button>
              <button class="crudBtn" @click="userRelationDelete(item)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="container">
      <button @click="userRelationCreate()">Create</button>
    </div>

</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useUserRelationStore } from "@/stores/";
import type { IUserRelation } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    userRelationDetails(userRelation: IUserRelation) {
      this.userRelationStore.setCurrentUserRelation(userRelation);
      this.$router.push(`/userRelationDetails/${userRelation.userId},${userRelation.relatedUserId}`);
    },
    userRelationEdit(userRelation: IUserRelation) {
      this.userRelationStore.setCurrentUserRelation(userRelation);
      this.$router.push(`/userRelationEdit/${userRelation.userId},${userRelation.relatedUserId}`);
    },
    userRelationDelete(userRelation: IUserRelation){
      this.userRelationStore.setCurrentUserRelation(userRelation);
      this.$router.push(`/userRelationDelete/${userRelation.userId},${userRelation.relatedUserId}`);
    },
    userRelationCreate() {
      this.$router.push(`/userRelationCreate`);
    }
  },
  computed:{
    ...mapStores(useUserRelationStore)
  },
  components: {
    ModelTitle
  }
})
</script>