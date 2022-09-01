<template>
  <div class="container">
    <h4>Edit Answer</h4>

    <form @submit.prevent="answerUpdate()">
      <div class="mb-3">
        <label for="inputAnswerEditQuestionId" class="form-label">Current questionId: {{ answerToDisplayCurrent.questionId }}</label>
        <input v-model="answer.questionId" type="questionId" class="form-control" id="inputAnswerEditQuestionId">
      </div>

      <div class="mb-3">
        <label for="inputAnswerEditAnswerText" class="form-label">Current answerText: {{ answerToDisplayCurrent.answerText }}</label>
        <input v-model="answer.answerText" type="answerText" class="form-control" id="inputAnswerEditAnswerText">
      </div>

      <div class="mb-3">
        <label for="inputAnswerCreateRightAnswer" class="form-label">Current rightAnswer: {{ answerToDisplayCurrent.rightAnswer }}</label> 
        <input v-model="answer.rightAnswer" type="checkbox" id="inputAnswerCreateRightAnswer">
      </div>

      <button type="submit">Update</button>
      <button @click="back(answer.answerId)">Back</button>
    </form>

  </div>
</template>


<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAnswerStore } from "@/stores/";
import type { IAnswer } from "@/models/";

export default defineComponent({
  data() {
    return {
      answer: {} as IAnswer,
      answerToDisplayCurrent: {} as IAnswer
    }
  },
  methods: {
    answerUpdate() {
      this.answerStore.apiUpdate(this.answer);
      this.$router.push(`/answerDetails/${this.answer.answerId}`);
    },
    back(answerId: number) {
      this.$router.push(`/answerDetails/${answerId}`);
    }
  },
  mounted() {
    this.answer = this.answerStore.getCurrentAnswer;
    this.answerToDisplayCurrent.questionId = this.answer.questionId;
    this.answerToDisplayCurrent.answerText = this.answer.answerText;
    this.answerToDisplayCurrent.rightAnswer = this.answer.rightAnswer;
  },
  computed: {
    ...mapStores(useAnswerStore)
  }
})
</script>