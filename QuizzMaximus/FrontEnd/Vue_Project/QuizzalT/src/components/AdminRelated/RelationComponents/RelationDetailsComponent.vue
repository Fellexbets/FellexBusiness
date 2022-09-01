<template>
  <div class="container">
    <h4>Relation Details</h4>
    <table class="table">
      <thead>
        <tr>
          <th>RelationId</th>
          <th>Relation Name</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ relation.relationId }} </td>
          <td> {{ relation.relationName }} </td>
        </tr>
      </tbody>
    </table>
    <button @click="relationEdit(relation)">Edit</button>
    <button @click="relationDelete(relation)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useRelationStore } from "@/stores/";
import type { IRelation } from "@/models/";

export default defineComponent({
  data() {
    return {
      relation: {} as IRelation
    }
  },
  methods: {
    relationEdit(relation : IRelation){
      this.relationStore.setCurrentRelation(relation);
      this.$emit('changePage', 'RelationEditComponent');
    },
    relationDelete(relation: IRelation){
      this.relationStore.setCurrentRelation(relation);
      this.$emit('changePage', 'RelationDeleteComponent');
    },
    back() {
      this.$emit('changePage', 'RelationIndexComponent');
    },

  },
  mounted() {
   this.relation = this.relationStore.getCurrentRelation;
  },
  computed: {
    ...mapStores(useRelationStore)
  }
})
</script>