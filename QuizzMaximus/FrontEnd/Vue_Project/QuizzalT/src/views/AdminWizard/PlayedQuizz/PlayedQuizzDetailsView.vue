<template>
  <div class="container">
    <h4>PlayedQuizz Details</h4>
    <table class="table">
      <thead>
        <tr>
          <th>PlayedQuizzId</th>
          <th>UserId</th>
          <th>QuizzId</th>
          <th>TotalPoints</th>
          <th>DateAnswered</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ playedQuizz.playedQuizzId }} </td>
          <td> {{ playedQuizz.userId }} </td>
          <td> {{ playedQuizz.quizzId }} </td>
          <td> {{ playedQuizz.totalPoints }} </td>
          <td> {{ playedQuizz.dateAnswered }} </td>
        </tr>
      </tbody>
    </table>
    <button @click="playedQuizzEdit(playedQuizz)">Edit</button>
    <button @click="playedQuizzDelete(playedQuizz)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { usePlayedQuizzStore } from "@/stores/";
import type { IPlayedQuizz } from "@/models/";

export default defineComponent({
  data() {
    return {
      playedQuizz: {} as IPlayedQuizz
    }
  },
  methods: {
    playedQuizzEdit(playedQuizz : IPlayedQuizz){
      this.playedQuizzStore.setCurrentPlayedQuizz(playedQuizz);
      this.$router.push(`/playedQuizzEdit/${playedQuizz.playedQuizzId}`)
    },
    playedQuizzDelete(playedQuizz: IPlayedQuizz){
      this.playedQuizzStore.setCurrentPlayedQuizz(playedQuizz);
      this.$router.push(`/playedQuizzDelete/${playedQuizz.playedQuizzId}`)
    },
    back() {
      this.$router.push(`/playedQuizzIndex`);
    },

  },
  mounted() {
   this.playedQuizz = this.playedQuizzStore.getCurrentPlayedQuizz;
  },
  computed: {
    ...mapStores(usePlayedQuizzStore)
  }
})
</script>