<template>
  <div class="container">
    <h4>Delete Question</h4>
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
    <p> Are you sure?</p>
    <button @click="questionDelete(question.questionId)">Delete</button>
    <button @click="back()">Back</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
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
    questionDelete(questionId: number){
        this.questionStore.apiDelete(questionId);
        this.$router.push(`/questionIndex`);
      },
      back(){
        this.$router.push(`/questionDetails/${this.question.questionId}`);
      }
  },
  mounted(){
    this.question = this.questionStore.getCurrentQuestion;
  },
  computed:{
    ...mapStores(useQuestionStore)
  },
  components: {
  }
})
</script>