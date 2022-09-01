<template>
  <div class="container">
    <h4>Delete PlayedQuestion</h4>
    <table class="table">
      <thead>
        <tr>
          <th>PlayedQuestionId</th>
          <th>UserId</th>
          <th>QuestionId</th>
          <th>GotItRight</th>
          <th>Points</th>
          <th>DateAnswered</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ playedQuestion.playedQuestionId }} </td>
          <td> {{ playedQuestion.userId }} </td>
          <td> {{ playedQuestion.questionId }} </td>
          <td> {{ playedQuestion.gotItRight }} </td>
          <td> {{ playedQuestion.points }} </td>
          <td> {{ playedQuestion.dateAnswered }} </td>
        </tr>
      </tbody>
    </table>
    <p> Are you sure?</p>
    <button @click="playedQuestionDelete(playedQuestion.playedQuestionId)">Delete</button>
    <button @click="back()">Back</button>
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
    playedQuestionDelete(playedQuestionId: number){
        this.playedQuestionStore.apiDelete(playedQuestionId);
        this.$router.push(`/playedQuestionIndex`);
      },
      back(){
        this.$router.push(`/playedQuestionDetails/${this.playedQuestion.playedQuestionId}`);
      }
  },
  mounted(){
    this.playedQuestion = this.playedQuestionStore.getCurrentPlayedQuestion;
  },
  computed:{
    ...mapStores(usePlayedQuestionStore)
  },
  components: {
  }
})
</script>