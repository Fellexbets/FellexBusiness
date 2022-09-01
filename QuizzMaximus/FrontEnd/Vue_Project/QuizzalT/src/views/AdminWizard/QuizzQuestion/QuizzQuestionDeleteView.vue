<template>
  <div class="container">
    <h4>Delete QuizzQuestion</h4>
    <table class="table">
      <thead>
        <tr>
          <th>QuizzId</th>
          <th>QuestionId</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ quizzQuestion.quizzId }} </td>
          <td> {{ quizzQuestion.questionId }} </td>
        </tr>
      </tbody>
    </table>
    <p> Are you sure?</p>
    <button @click="quizzQuestionDelete(quizzQuestion.quizzId, quizzQuestion.questionId)">Delete</button>
    <button @click="back()">Back</button>
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
    quizzQuestionDelete(quizzId: number, questionId: number) {
      this.quizzQuestionStore.apiDelete(quizzId, questionId);
      this.$router.push(`/quizzQuestionIndex`);
    },
    back() {
      this.$router.push(`/quizzQuestionDetails/${this.quizzQuestion.quizzId},${this.quizzQuestion.questionId}`);
    }
  },
  mounted() {
    this.quizzQuestion = this.quizzQuestionStore.getCurrentQuizzQuestion;
  },
  computed: {
    ...mapStores(useQuizzQuestionStore)
  },
  components: {
  }
})
</script>