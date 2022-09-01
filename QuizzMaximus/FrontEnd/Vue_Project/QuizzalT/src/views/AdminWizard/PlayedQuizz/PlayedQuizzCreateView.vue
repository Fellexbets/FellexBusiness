<template>
  <div class="container">
    <h4>Create PlayedQuizz</h4>

    <form @submit.prevent="playedQuizzCreate()">
      <div class="mb-3">
        <label for="inputPlayedQuizzCreateUserId" class="form-label">UserId</label>
        <input v-model="playedQuizz.userId" type="userId" class="form-control" id="inputPlayedQuizzCreateUserId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuizzCreateQuizzId" class="form-label">QuizzId</label>
        <input v-model="playedQuizz.quizzId" type="quizzId" class="form-control" id="inputPlayedQuizzCreateQuizzId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuizzCreateTotalPoints" class="form-label">TotalPoints</label>
        <input v-model="playedQuizz.totalPoints" type="totalPoints" class="form-control" id="inputPlayedQuizzCreateTotalPoints">
      </div>

      <button type="submit">Create</button>
      <button @click="back()">Back</button>
    </form>

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
    playedQuizzCreate() {
      this.playedQuizzStore.apiCreate(this.playedQuizz);
      this.$router.push(`/playedQuizzDetails/${this.playedQuizz.playedQuizzId}`);
      //this.$router.push(`/playedQuizzIndex`);
    },
    back() {
      this.$router.push(`/playedQuizzIndex`);
    }
  },
  computed: {
    ...mapStores(usePlayedQuizzStore)
  }
})
</script>