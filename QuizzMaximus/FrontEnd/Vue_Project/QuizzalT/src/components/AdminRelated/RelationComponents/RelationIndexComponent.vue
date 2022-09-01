<template>
    <div class="container">
      <ModelTitle title="Relation Index" />
      <table class="table">
        <thead>
          <tr>
            <th>RelationId</th>
            <th>Relation Name</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in relationStore.getRelations" :key="item.relationId">
            <td> {{ item.relationId }} </td>
            <td> {{ item.relationName }} </td>
            <td>
              <button class="mb-1 crudBtn d-flex flex-column" @click="relationDetails(item)">Details</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="relationEdit(item)">Edit</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="relationDelete(item)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="container">
      <button @click="relationCreate()">Create</button>
    </div>

</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useRelationStore } from "@/stores/";
import type { IRelation } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    relationDetails(relation: IRelation) {
      this.relationStore.setCurrentRelation(relation);
      this.$emit('changePage', 'RelationDetailsComponent');
    },
    relationEdit(relation: IRelation) {
      this.relationStore.setCurrentRelation(relation);
      this.$emit('changePage', 'RelationEditComponent');
    },
    relationDelete(relation: IRelation){
      this.relationStore.setCurrentRelation(relation);
      this.$emit('changePage', 'RelationDeleteComponent');
    },
    relationCreate() {
      this.$emit('changePage', 'RelationCreateComponent');
    }
  },
  computed:{
    ...mapStores(useRelationStore)
  },
  components: {
    ModelTitle
  },
  emits: ["changePage"]
})
</script>