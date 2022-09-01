<template>
  <div class="container">
    <h4>QuizzQuestion Details</h4>
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
    <button @click="quizzQuestionEdit(quizzQuestion)">Edit</button>
    <button @click="quizzQuestionDelete(quizzQuestion)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
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
    quizzQuestionEdit(quizzQuestion : IQuizzQuestion){
      this.quizzQuestionStore.setCurrentQuizzQuestion(quizzQuestion);
      this.$emit('changePage', 'QuizzQuestionEditComponent');
    },
    quizzQuestionDelete(quizzQuestion: IQuizzQuestion){
      this.quizzQuestionStore.setCurrentQuizzQuestion(quizzQuestion);
      this.$emit('changePage', 'QuizzQuestionDeleteComponent');
    },
    back() {
      this.$emit('changePage', 'QuizzQuestionIndexComponent');
    },

  },
  mounted() {
   this.quizzQuestion = this.quizzQuestionStore.getCurrentQuizzQuestion;
  },
  computed: {
    ...mapStores(useQuizzQuestionStore)
  }
})
</script>