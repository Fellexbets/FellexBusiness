<template>
  <div class="container">
    <h4>Delete PlayedQuizz</h4>
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
    <p> Are you sure?</p>
    <button @click="playedQuizzDelete(playedQuizz.playedQuizzId)">Delete</button>
    <button @click="back()">Back</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
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
    playedQuizzDelete(playedQuizzId: number){
        this.playedQuizzStore.apiDelete(playedQuizzId);
        this.$emit('changePage', 'PlayedQuizzIndexComponent');
      },
      back(){
        this.$emit('changePage', 'PlayedQuizzIndexComponent');
      }
  },
  mounted(){
    this.playedQuizz = this.playedQuizzStore.getCurrentPlayedQuizz;
  },
  computed:{
    ...mapStores(usePlayedQuizzStore)
  },
  components: {
  }
})
</script>