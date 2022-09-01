<template>
  <div class="container">
    <h4>Edit QuizzQuestion</h4>

    <form @submit.prevent="quizzQuestionUpdate()">
      <div class="mb-3">
        <label for="inputQuizzQuestionEditQuizzId" class="form-label">Current QuizzId: {{ quizzQuestionToDisplayCurrent.quizzId }} </label>
        <input v-model="quizzQuestion.quizzId" type="quizzId" class="form-control" id="inputQuizzQuestionEditQuizzId">
      </div>
      <div class="mb-3">
        <label for="inputQuizzQuestionEditQuestionId" class="form-label">Current QuestionId: {{ quizzQuestionToDisplayCurrent.questionId }}</label>
        <input v-model="quizzQuestion.questionId" type="questionId" class="form-control" id="inputQuizzQuestionEditQuestionId">
      </div>

      <button type="submit">Update</button>
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
      quizzQuestion: {} as IQuizzQuestion,
      quizzQuestionToDisplayCurrent: {} as IQuizzQuestion
    }
  },
  methods: {
    quizzQuestionUpdate() {
      this.quizzQuestionStore.apiUpdate(this.quizzQuestion);
      this.$emit('changePage', 'QuizzQuestionIndexComponent');
    },
    back() {
      this.$emit('changePage', 'QuizzQuestionIndexComponent');
    }
  },
  mounted() {
    this.quizzQuestion = this.quizzQuestionStore.getCurrentQuizzQuestion;
    this.quizzQuestionToDisplayCurrent.quizzId = this.quizzQuestion.quizzId;
    this.quizzQuestionToDisplayCurrent.questionId = this.quizzQuestion.questionId;
  },
  computed: {
    ...mapStores(useQuizzQuestionStore)
  }
})
</script>