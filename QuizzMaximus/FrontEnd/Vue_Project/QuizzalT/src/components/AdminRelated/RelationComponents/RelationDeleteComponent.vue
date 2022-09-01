<template>
  <div class="container">
    <h4>Delete Relation</h4>
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
    <p> Are you sure?</p>
    <button @click="relationDelete(relation.relationId)">Delete</button>
    <button @click="back()">Back</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
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
    relationDelete(relationId: number){
        this.relationStore.apiDelete(relationId);
        this.$emit('changePage', 'RelationIndexComponent');
      },
      back(){
        this.$emit('changePage', 'RelationIndexComponent');
      }
  },
  mounted(){
    this.relation = this.relationStore.getCurrentRelation;
  },
  computed:{
    ...mapStores(useRelationStore)
  },
  components: {
  }
})
</script>