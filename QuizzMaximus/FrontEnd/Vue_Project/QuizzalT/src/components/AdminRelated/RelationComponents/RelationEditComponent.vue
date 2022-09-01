<template>
  <div class="container">
    <h4>Edit Relation</h4>

    <form @submit.prevent="relationUpdate()">
      <div class="mb-3">
        <label for="inputRelationEditRelationName" class="form-label">Current relation name: {{ relationToDisplayCurrent.relationName }}</label>
        <input v-model="relation.relationName" type="relationName" class="form-control" id="inputRelationEditRelationName">
      </div>

      <button type="submit">Update</button>
      <button @click="back()">Back</button>
    </form>

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
      relation: {} as IRelation,
      relationToDisplayCurrent: {} as IRelation
    }
  },
  methods: {
    relationUpdate() {
      this.relationStore.apiUpdate(this.relation);
      this.$emit('changePage', 'RelationIndexComponent');
    },
    back() {
      this.$emit('changePage', 'RelationIndexComponent');
    }
  },
  mounted() {
    this.relation = this.relationStore.getCurrentRelation;
    this.relationToDisplayCurrent.relationName = this.relation.relationName;
  },
  computed: {
    ...mapStores(useRelationStore)
  }
})
</script>