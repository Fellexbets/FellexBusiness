<template>
  <div class="container">
    <h4>Edit PlayedQuizz</h4>

    <form @submit.prevent="playedQuizzUpdate()">
      <div class="mb-3">
        <label for="inputPlayedQuizzEditUserId" class="form-label">Current userId: {{ playedQuizzToDisplayCurrent.userId }}</label>
        <input v-model="playedQuizz.userId" type="userId" class="form-control" id="inputPlayedQuizzEditUserId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuizzEditQuizzId" class="form-label">Current quizzId: {{ playedQuizzToDisplayCurrent.quizzId }}</label>
        <input v-model="playedQuizz.quizzId" type="quizzId" class="form-control" id="inputPlayedQuizzEditQuizzId">
      </div>

      <div class="mb-3">
        <label for="inputPlayedQuizzEditTotalPoints" class="form-label">Current totalPoints: {{ playedQuizzToDisplayCurrent.totalPoints }}</label>
        <input v-model="playedQuizz.totalPoints" type="totalPoints" class="form-control"  id="inputPlayedQuizzEditTotalPoints">
      </div>

      <button type="submit">Update</button>
      <button @click="back(playedQuizz.playedQuizzId)">Back</button>
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
      playedQuizz: {} as IPlayedQuizz,
      playedQuizzToDisplayCurrent: {} as IPlayedQuizz
    }
  },
  methods: {
    playedQuizzUpdate() {
      this.playedQuizzStore.apiUpdate(this.playedQuizz);
      this.$router.push(`/playedQuizzDetails/${this.playedQuizz.playedQuizzId}`);
    },
    back(playedQuizzId: number) {
      this.$router.push(`/playedQuizzDetails/${playedQuizzId}`);
    }
  },
  mounted() {
    this.playedQuizz = this.playedQuizzStore.getCurrentPlayedQuizz;
    this.playedQuizzToDisplayCurrent.userId = this.playedQuizz.userId;
    this.playedQuizzToDisplayCurrent.quizzId = this.playedQuizz.quizzId;
    this.playedQuizzToDisplayCurrent.totalPoints = this.playedQuizz.totalPoints;
  },
  computed: {
    ...mapStores(usePlayedQuizzStore)
  }
})
</script>