<template>
  <div class="container">
    <h4>Edit Question</h4>

    <form @submit.prevent="questionUpdate()">
      <div class="mb-3">
        <label for="inputQuestionEditUserId" class="form-label">Current userId: {{ questionToDisplayCurrent.userId }}</label>
        <input v-model="question.userId" type="userId" class="form-control" id="inputQuestionEditUserId">
      </div>

      <div class="mb-3">
        <label for="inputQuestionEditThemeId" class="form-label">Current themeId: {{ questionToDisplayCurrent.themeId }}</label>
        <input v-model="question.themeId" type="themeId" class="form-control" id="inputQuestionEditThemeId">
      </div>

      <div class="mb-3">
        <label for="inputQuestionEditQuestionName" class="form-label">Current questionName: {{ questionToDisplayCurrent.questionName }}</label>
        <input v-model="question.questionName" type="questionName" class="form-control" id="inputQuestionEditQuestionName">
      </div>

      <div class="mb-3">
        <label for="inputQuestionEditQuestionText" class="form-label">Current questionText: {{ questionToDisplayCurrent.questionText }}</label>
        <input v-model="question.questionText" type="questionText" class="form-control" id="inputQuestionEditQuestionText">
      </div>

      <div class="mb-3">
        <label for="inputQuestionEditDifficulty" class="form-label">Current difficulty: {{ questionToDisplayCurrent.difficulty }}</label>
        <input v-model="question.difficulty" type="difficulty" class="form-control" id="inputQuestionEditDifficulty">
      </div>

      <div class="mb-3">
        <label for="inputQuestionEditStatus" class="form-label">Status</label>
        <select id="inputQuestionEditStatus" v-model="question.status">
          <option value="Published">Published</option>
          <option value="Draft">Draft</option>
          <option value="Deleted">Deleted</option>
        </select>
      </div>

      <button type="submit">Update</button>
      <button @click="back(question.questionId)">Back</button>
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
      question: {} as IQuestion,
      questionToDisplayCurrent: {} as IQuestion
    }
  },
  methods: {
    questionUpdate() {
      this.questionStore.apiUpdate(this.question);
      this.$emit('changePage', 'QuestionDetailsComponent');
    },
    back(questionId: number) {
      this.$emit('changePage', 'QuestionIndexComponent');
    }
  },
  mounted() {
    this.question = this.questionStore.getCurrentQuestion;
    this.questionToDisplayCurrent.userId = this.question.userId;
    this.questionToDisplayCurrent.themeId = this.question.themeId;
    this.questionToDisplayCurrent.questionName = this.question.questionName;
    this.questionToDisplayCurrent.questionText = this.question.questionText;
    this.questionToDisplayCurrent.difficulty = this.question.difficulty;
    this.questionToDisplayCurrent.status = this.question.status;
  },
  computed: {
    ...mapStores(useQuestionStore)
  }
})
</script>