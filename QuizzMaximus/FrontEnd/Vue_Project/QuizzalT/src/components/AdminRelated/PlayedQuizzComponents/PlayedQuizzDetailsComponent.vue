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
      this.$emit('changePage', 'PlayedQuizzEditComponent');
    },
    playedQuizzDelete(playedQuizz: IPlayedQuizz){
      this.playedQuizzStore.setCurrentPlayedQuizz(playedQuizz);
      this.$emit('changePage', 'PlayedQuizzDeleteComponent');
    },
    back() {
      this.$emit('changePage', 'PlayedQuizzEditComponent');
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