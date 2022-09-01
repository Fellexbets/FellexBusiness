<template>
  <div class="container">
    <h4>Create QuizzQuestion</h4>

    <form @submit.prevent="quizzQuestionCreate()">
      <div class="mb-3">
        <label for="inputQuizzQuestionCreatequizzId" class="form-label">QuizzId</label>
        <input v-model="quizzQuestion.quizzId" type="quizzId" class="form-control" id="inputQuizzQuestionCreatequizzId">
      </div>
      <div class="mb-3">
        <label for="inputQuizzQuestionCreateQuestionId" class="form-label">QuestionId</label>
        <input v-model="quizzQuestion.questionId" type="questionId" class="form-control" id="inputQuizzQuestionCreateQuestionId">
      </div>
      
      <button type="submit">Create</button>
      <button @click="back()">Back</button>
    </form>

  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuizzQuestionStore } from "@/stores/";
import type { IQuizzQuestion } from "@/models/";

export default defineComponent({
  data() {
    return {
      quizzQuestion: {} as IQuizzQuestion
    }
  },
  methods: {
    quizzQuestionCreate() {
      this.quizzQuestionStore.apiCreate(this.quizzQuestion);
      this.$emit('changePage', 'QuizzQuestionIndexComponent');
    },
    back() {
      this.$emit('changePage', 'QuizzQuestionIndexComponent');
    }
  },
  computed: {
    ...mapStores(useQuizzQuestionStore)
  }
})
</script>