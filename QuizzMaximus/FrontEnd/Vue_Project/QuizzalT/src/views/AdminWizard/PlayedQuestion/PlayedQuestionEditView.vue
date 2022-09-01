<template>
  <div class="container">
    <h4>Edit PlayedQuestion</h4>

    <form @submit.prevent="playedQuestionUpdate()">
      <div class="mb-3">
        <label for="inputPlayedQuestionEditUserId" class="form-label">Current userId: {{ playedQuestionToDisplayCurrent.userId }}</label>
        <input v-model="playedQuestion.userId" type="userId" class="form-control" id="inputPlayedQuestionEditUserId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuestionEditQuestionId" class="form-label">Current questionId: {{ playedQuestionToDisplayCurrent.questionId }}</label>
        <input v-model="playedQuestion.questionId" type="questionId" class="form-control" id="inputPlayedQuestionEditQuestionId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuestionEditGotItRight" class="form-label">Current gotItRight: {{ playedQuestionToDisplayCurrent.gotItRight }}</label>
        <input v-model="playedQuestion.gotItRight" type="checkbox" id="inputPlayedQuestionEditGotItRight">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuestionEditPoints" class="form-label">Current points: {{ playedQuestionToDisplayCurrent.points }}</label>
        <input v-model="playedQuestion.points" type="points" class="form-control"  id="inputPlayedQuestionEditPoints">
      </div>

      <button type="submit">Update</button>
      <button @click="back(playedQuestion.playedQuestionId)">Back</button>
    </form>

  </div>
</template>


<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { usePlayedQuestionStore } from "@/stores/";
import type { IPlayedQuestion } from "@/models/";

export default defineComponent({
  data() {
    return {
      playedQuestion: {} as IPlayedQuestion,
      playedQuestionToDisplayCurrent: {} as IPlayedQuestion
    }
  },
  methods: {
    playedQuestionUpdate() {
      this.playedQuestionStore.apiUpdate(this.playedQuestion);
      this.$router.push(`/playedQuestionDetails/${this.playedQuestion.playedQuestionId}`);
    },
    back(playedQuestionId: number) {
      this.$router.push(`/playedQuestionDetails/${playedQuestionId}`);
    }
  },
  mounted() {
    this.playedQuestion = this.playedQuestionStore.getCurrentPlayedQuestion;
    this.playedQuestionToDisplayCurrent.userId = this.playedQuestion.userId;
    this.playedQuestionToDisplayCurrent.questionId = this.playedQuestion.questionId;
    this.playedQuestionToDisplayCurrent.gotItRight = this.playedQuestion.gotItRight;
    this.playedQuestionToDisplayCurrent.points = this.playedQuestion.points;
  },
  computed: {
    ...mapStores(usePlayedQuestionStore)
  }
})
</script>