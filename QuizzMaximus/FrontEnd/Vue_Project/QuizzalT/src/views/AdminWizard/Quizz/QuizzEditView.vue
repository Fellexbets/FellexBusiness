<template>
  <div class="container">
    <h4>Edit Quizz</h4>

    <form @submit.prevent="quizzUpdate()">
      <div class="mb-3">
        <label for="inputQuizzEditUserId" class="form-label">Current userId: {{ quizzToDisplayCurrent.userId }}</label>
        <input v-model="quizz.userId" type="userId" class="form-control" id="inputQuizzEditUserId">
      </div>

      <div class="mb-3">
        <label for="inputQuizzEditQuizzName" class="form-label">Current QuizzName: {{ quizzToDisplayCurrent.quizzName }}</label>
        <input v-model="quizz.quizzName" type="quizzName" class="form-control" id="inputQuizzEditQuizzName">
      </div>

      <div class="mb-3">
        <label for="inputQuizzEditStatus" class="form-label">Current Status: {{ quizzToDisplayCurrent.status }}</label>
        <select id="inputQuizzEditStatus" v-model="quizz.status">
          <option value="Published">Published</option>
          <option value="Draft">Draft</option>
          <option value="Deleted">Deleted</option>
        </select>
      </div>

      <button type="submit">Update</button>
      <button @click="back(quizz.quizzId)">Back</button>
    </form>

  </div>
</template>


<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuizzStore } from "@/stores/";
import type { IQuizz } from "@/models/";

export default defineComponent({
  data() {
    return {
      quizz: {} as IQuizz,
      quizzToDisplayCurrent: {} as IQuizz
    }
  },
  methods: {
    quizzUpdate() {
      this.quizzStore.apiUpdate(this.quizz);
      this.$router.push(`/quizzDetails/${this.quizz.quizzId}`);
    },
    back(quizzId: number) {
      this.$router.push(`/quizzDetails/${quizzId}`);
    }
  },
  mounted() {
    this.quizz = this.quizzStore.getCurrentQuizz;
    this.quizzToDisplayCurrent.userId = this.quizz.userId;
    this.quizzToDisplayCurrent.quizzName = this.quizz.quizzName;
    this.quizzToDisplayCurrent.status = this.quizz.status;
  },
  computed: {
    ...mapStores(useQuizzStore)
  }
})
</script>