<template>
  <div class="container">
    <h4>Create Quizz</h4>

    <form @submit.prevent="quizzCreate()">
      <div class="mb-3">
        <label for="inputQuizzCreateUserId" class="form-label">UserId</label>
        <input v-model="quizz.userId" type="userId" class="form-control" id="inputQuizzCreateUserId">
      </div>

      <div class="mb-3">
        <label for="inputQuizzCreateQuizzName" class="form-label">QuizzName</label>
        <input v-model="quizz.quizzName" type="quizzName" class="form-control" id="inputQuizzCreateQuizzName">
      </div>

      <div class="mb-3">
        <label for="inputQuizzCreateStatus" class="form-label">Status</label>
        <select class="dropdown col-2 " id="inputQuizzCreateStatus" v-model="quizz.status">
          <option value="Published">Published</option>
          <option value="Draft">Draft</option>
          <option value="Deleted">Deleted</option>
        </select>
      </div>

      <button type="submit">Create</button>
      <button @click="back()">Back</button>
    </form>

  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuizzStore } from "@/stores/";
import type { IQuizz } from "@/models/";

export default defineComponent({
  data() {
    return {
      quizz: {} as IQuizz
    }
  },
  methods: {
    quizzCreate() {
      this.quizzStore.apiCreate(this.quizz);
      this.$emit('changePage', 'QuizzIndexComponent');
    },
    back() {
      this.$emit('changePage', 'QuizzIndexComponent');
    }
  },
  computed: {
    ...mapStores(useQuizzStore)
  }
})
</script>