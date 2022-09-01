<template>
  <div class="container">
    <h4>Create Question</h4>

    <form @submit.prevent="questionCreate()">
      <div class="mb-3">
        <label for="inputQuestionCreateUserId" class="form-label">UserId</label>
        <input v-model="question.userId" type="userId" class="form-control" id="inputQuestionCreateUserId">
      </div>

      <div class="mb-3">
        <label for="inputQuestionCreateThemeId" class="form-label">ThemeId</label>
        <input v-model="question.themeId" type="themeId" class="form-control" id="inputQuestionCreateThemeId">
      </div>

      <div class="mb-3">
        <label for="inputQuestionCreateQuestionName" class="form-label">QuestionName</label>
        <input v-model="question.questionName" type="questionName" class="form-control" id="inputQuestionCreateQuestionName">
      </div>

      <div class="mb-3">
        <label for="inputQuestionCreateQuestionText" class="form-label">QuestionText</label>
        <input v-model="question.questionText" type="questionText" class="form-control" id="inputQuestionCreateQuestionText">
      </div>

      <div class="mb-3">
        <label for="inputQuestionCreateDifficulty" class="form-label">Difficulty</label>
        <input v-model="question.difficulty" type="difficulty" class="form-control" id="inputQuestionCreateDifficulty">
      </div>

      <div class="mb-3">
        <label for="inputQuestionCreateStatus" class="form-label">Status</label>
        <select id="inputQuestionCreateStatus" v-model="question.status">
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
import { useQuestionStore } from "@/stores/";
import type { IQuestion } from "@/models/";

export default defineComponent({
  data() {
    return {
      question: {} as IQuestion
    }
  },
  methods: {
    questionCreate() {
      this.questionStore.apiCreate(this.question);
      //this.$router.push(`/questionDetails/${this.question.questionId}`);
      this.$router.push(`/questionIndex`);
    },
    back() {
      this.$router.push(`/questionIndex`);
    }
  },
  computed: {
    ...mapStores(useQuestionStore)
  }
})
</script>