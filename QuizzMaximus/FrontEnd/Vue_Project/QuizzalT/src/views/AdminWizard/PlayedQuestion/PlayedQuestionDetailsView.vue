<template>
  <div class="container">
    <h4>PlayedQuestion Details</h4>
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
    <button @click="playedQuestionEdit(playedQuestion)">Edit</button>
    <button @click="playedQuestionDelete(playedQuestion)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
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
    playedQuestionEdit(playedQuestion : IPlayedQuestion){
      this.playedQuestionStore.setCurrentPlayedQuestion(playedQuestion);
      this.$router.push(`/playedQuestionEdit/${playedQuestion.playedQuestionId}`)
    },
    playedQuestionDelete(playedQuestion: IPlayedQuestion){
      this.playedQuestionStore.setCurrentPlayedQuestion(playedQuestion);
      this.$router.push(`/playedQuestionDelete/${playedQuestion.playedQuestionId}`)
    },
    back() {
      this.$router.push(`/playedQuestionIndex`);
    },

  },
  mounted() {
   this.playedQuestion = this.playedQuestionStore.getCurrentPlayedQuestion;
  },
  computed: {
    ...mapStores(usePlayedQuestionStore)
  }
})
</script>