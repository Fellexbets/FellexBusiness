<template>
  <div class="container">
    <h4>Create Relation</h4>

    <form @submit.prevent="relationCreate()">
      <div class="mb-3">
        <label for="inputRelationCreateRelationName" class="form-label">Relation Name</label>
        <input v-model="relation.relationName" type="relationName" class="form-control" id="inputRelationCreateRelationName">
      </div>

      <button type="submit">Create</button>
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
      relation: {} as IRelation
    }
  },
  methods: {
    relationCreate() {
      this.relationStore.apiCreate(this.relation);
      this.$emit('changePage', 'RelationIndexComponent');
    },
    back() {
      this.$emit('changePage', 'RelationIndexComponent');
    }
  },
  computed: {
    ...mapStores(useRelationStore)
  }
})
</script>