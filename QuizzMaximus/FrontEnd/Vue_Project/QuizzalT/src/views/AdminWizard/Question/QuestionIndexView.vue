<template>
  <div class="container">
    <ModelTitle title="Question Index" />
    <table class="table">
      <thead>
        <tr>
          <th>QuestionId</th>
          <th>UserId</th>
          <th>ThemeId</th>
          <th>QuestionName</th>
          <th>QuestionText</th>
          <th>Difficulty</th>
          <th>Status</th>
          <th>DateCreated</th>
          <th>DateEdited</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in questionStore.getQuestions" :key="item.questionId">
          <td> {{ item.questionId }} </td>
          <td> {{ item.userId }} </td>
          <td> {{ item.themeId }} </td>
          <td> {{ item.questionName }} </td>
          <td> {{ item.questionText }} </td>
          <td> {{ item.difficulty }} </td>
          <td> {{ item.status }} </td>         
          <td> {{ item.dateCreated }} </td>
          <td> {{ item.dateEdited }} </td>
          <td>
            <button class="crudBtn" @click="questionDetails(item)">Details</button>
            <button class="crudBtn" @click="questionEdit(item)">Edit</button>
            <button class="crudBtn" @click="questionDelete(item)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="container">
    <button @click="questionCreate()">Create</button>
  </div>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuestionStore } from "@/stores/";
import type { IQuestion } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    questionDetails(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$router.push(`/questionDetails/${question.questionId}`);
    },
    questionEdit(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$router.push(`/questionEdit/${question.questionId}`);
    },
    questionDelete(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$router.push(`/questionDelete/${question.questionId}`);
    },
    questionCreate() {
      this.$router.push(`/questionCreate`);
    }
  },
  computed: {
    ...mapStores(useQuestionStore)
  },
  components: {
    ModelTitle
  }
})
</script>