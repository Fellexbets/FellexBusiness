<template>
  <div class="container">
    <ModelTitle title="Question Index" />
    <Filters :filterQuestions="filterQuestions" :sortQuestions="sortQuestions" :search="search" />
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
        <tr v-for="item in questions" :key="item.questionId">
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
            <button class="mb-1 crudBtn d-flex flex-column" @click="questionDetails(item)">Details</button>
            <button class="mb-1 crudBtn d-flex flex-column" @click="questionEdit(item)">Edit</button>
            <button class="mb-1 crudBtn d-flex flex-column" @click="questionDelete(item)">Delete</button>
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
import { useQuestionStore, useThemeStore } from "@/stores/";
import type { IQuestion } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";
import { useAuthStore } from "@/stores/AuthStore";
import Filters from "@/components/Filters/QuestionFilters.vue";

export default defineComponent({
  data() {
    return {
      questions: [] as IQuestion[], null_value: null
    }
  },
  methods: {
    questionDetails(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$emit('changePage', 'QuestionDetailsComponent');
    },
    questionEdit(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$emit('changePage', 'QuestionEditComponent');
    },
    questionDelete(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$emit('changePage', 'QuestionDeleteComponent');
    },
    questionCreate() {
      this.$emit('changePage', 'QuestionCreateComponent');
    },
    sortQuestions(selectId: any) {
      if (selectId == 'UserId') {
        this.questions.sort((a, b) => a.userId > b.userId ? 1 : -1);
      }
      else if (selectId == 'QuestionId') {
        this.questions.sort((a, b) => a.questionId > b.questionId ? 1 : -1);
      }
      else if (selectId == 'ThemeId') {
        this.questions.sort((a, b) => a.themeId > b.themeId ? 1 : -1);
      }
    },
    filterQuestions(themeName: any) {
      this.resetQuestions()
      if (themeName !== 'All') {
          let themeId = this.themeStore.getThemeId(themeName);
          console.log(themeId)
          this.questions = this.questionStore.getQuestions.filter(question => question.themeId == themeId);
      }
    },
    search(term: string) {
      this.resetQuestions()
      this.questions = this.questions.filter((question) => {
        return question.questionText.toLowerCase().includes(term.toLowerCase())
      })
    },
    resetQuestions() {
      this.questions = this.questionStore.getQuestions;
    }
  },
  mounted() {
    this.questions = this.questionStore.getQuestions
  },
  computed: {
    ...mapStores(useAuthStore, useQuestionStore, useThemeStore)
  },
  components: {
    ModelTitle, Filters
  },
  emits: ["changePage"]
})
</script>