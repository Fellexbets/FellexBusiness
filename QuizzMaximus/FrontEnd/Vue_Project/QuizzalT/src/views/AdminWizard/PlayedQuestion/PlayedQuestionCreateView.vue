<template>
  <div class="container">
    <h4>Create PlayedQuestion</h4>

    <form @submit.prevent="playedQuestionCreate()">
      <div class="mb-3">
        <label for="inputPlayedQuestionCreateUserId" class="form-label">UserId</label>
        <input v-model="playedQuestion.userId" type="userId" class="form-control" id="inputQuestionCreateUserId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuestionCreateQuestionId" class="form-label">QuestionId</label>
        <input v-model="playedQuestion.questionId" type="questionId" class="form-control" id="inputPlayedQuestionCreateQuestionId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuestionCreateGotItRight" class="form-label">GotItRight</label>
        <input v-model="playedQuestion.gotItRight" type="checkbox" id="inputPlayedQuestionCreateGotItRight">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuestionCreatePoints" class="form-label">Points</label>
        <input v-model="playedQuestion.points" type="points" class="form-control" id="inputPlayedQuestionCreatePoints">
      </div>

      <button type="submit">Create</button>
      <button @click="back()">Back</button>
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
      playedQuestion: {} as IPlayedQuestion
    }
  },
  methods: {
    playedQuestionCreate() {
      this.playedQuestionStore.apiCreate(this.playedQuestion);
      //this.$router.push(`/playedQuestionDetails/${this.playedQuestion.playedQuestionId}`);
      this.$router.push(`/playedQuestionIndex`);
    },
    back() {
      this.$router.push(`/playedQuestionIndex`);
    }
  },
  computed: {
    ...mapStores(usePlayedQuestionStore)
  }
})
</script>