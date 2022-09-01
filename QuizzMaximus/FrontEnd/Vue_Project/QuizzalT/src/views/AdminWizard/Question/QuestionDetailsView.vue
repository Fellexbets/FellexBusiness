<template>
  <div class="container">
    <h4>Question Details</h4>
    <table class="table">
      <thead>
        <tr>
          <th>QuestionId</th>
          <th>UserId</th>
          <th>ThemeId</th>
          <th>QuestionText</th>
          <th>QuestionName</th>
          <th>Difficulty</th>
          <th>Status</th>
          <th>DateCreated</th>
          <th>DateEdited</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ question.questionId }} </td>
          <td> {{ question.userId }} </td>
          <td> {{ question.themeId }} </td>
          <td> {{ question.questionName }} </td>
          <td> {{ question.questionText }} </td>
          <td> {{ question.difficulty }} </td>
          <td> {{ question.status }} </td>
          <td> {{ question.dateCreated }} </td>
          <td> {{ question.dateEdited }} </td>
        </tr>
      </tbody>
    </table>
    <button @click="questionEdit(question)">Edit</button>
    <button @click="questionDelete(question)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useQuestionStore } from "@/stores/";
import type { IQuestion } from "@/models/";

export default defineComponent({
  data() {
    return {
      question: {} as IQuestion
    }
  },
  methods: {
    questionEdit(question : IQuestion){
      this.questionStore.setCurrentQuestion(question);
      this.$router.push(`/questionEdit/${question.questionId}`)
    },
    questionDelete(question: IQuestion){
      this.questionStore.setCurrentQuestion(question);
      this.$router.push(`/questionDelete/${question.questionId}`)
    },
    back() {
      this.$router.push(`/questionIndex`);
    },

  },
  mounted() {
   this.question = this.questionStore.getCurrentQuestion;
  },
  computed: {
    ...mapStores(useQuestionStore)
  }
})
</script>