<template>
  <div class="container">
    <ModelTitle title="PlayedQuizz Index" />
    <table class="table">
      <thead>
        <tr>
          <th>PlayedQuizzId</th>
          <th>UserId</th>
          <th>QuizzId</th>
          <th>TotalPoints</th>
          <th>DateAnswered</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in playedQuizzStore.getPlayedQuizzes" :key="item.playedQuizzId">
          <td> {{ item.playedQuizzId }} </td>
          <td> {{ item.userId }} </td>
          <td> {{ item.quizzId }} </td>
          <td> {{ item.totalPoints }} </td>
          <td> {{ item.dateAnswered }} </td>
          <td>
            <button class="crudBtn" @click="playedQuizzDetails(item)">Details</button>
            <button class="crudBtn" @click="playedQuizzEdit(item)">Edit</button>
            <button class="crudBtn" @click="playedQuizzDelete(item)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="container">
    <button @click="playedQuizzCreate()">Create</button>
  </div>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { usePlayedQuizzStore } from "@/stores/";
import type { IPlayedQuizz } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    playedQuizzDetails(playedQuizz: IPlayedQuizz) {
      this.playedQuizzStore.setCurrentPlayedQuizz(playedQuizz);
      this.$emit('changePage', 'PlayedQuizzDetailsComponent');
    },
    playedQuizzEdit(playedQuizz: IPlayedQuizz) {
      this.playedQuizzStore.setCurrentPlayedQuizz(playedQuizz);
      this.$emit('changePage', 'PlayedQuizzEditComponent');
    },
    playedQuizzDelete(playedQuizz: IPlayedQuizz) {
      this.playedQuizzStore.setCurrentPlayedQuizz(playedQuizz);
      this.$emit('changePage', 'PlayedQuizzDeleteComponent');
    },
    playedQuizzCreate() {
      this.$emit('changePage', 'PlayedQuizzCreateComponent');
    }
  },
  computed: {
    ...mapStores(usePlayedQuizzStore)
  },
  components: {
    ModelTitle
  },
  emits: ["changePage"]
})
</script>