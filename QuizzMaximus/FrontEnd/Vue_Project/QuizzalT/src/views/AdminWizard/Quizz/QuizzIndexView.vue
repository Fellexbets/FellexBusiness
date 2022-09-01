<template>
  <div class="container">
    <ModelTitle title="Quizz Index" />
    <table class="table">
      <thead>
        <tr>
          <th>QuizzId</th>
          <th>UserId</th>
          <th>QuizzName</th>
          <th>Status</th>
          <th>DateCreated</th>
          <th>DateEdited</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in quizzStore.getQuizzes" :key="item.quizzId">
          <td> {{ item.quizzId }} </td>
          <td> {{ item.userId }} </td>
          <td> {{ item.quizzName }} </td>
          <td> {{ item.status }} </td>
          <td> {{ item.dateCreated }} </td>
          <td> {{ item.dateEdited }} </td>
          <td>
            <button class="crudBtn" @click="quizzDetails(item)">Details</button>
            <button class="crudBtn" @click="quizzEdit(item)">Edit</button>
            <button class="crudBtn" @click="quizzDelete(item)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="container">
    <button @click="quizzCreate()">Create</button>
  </div>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuizzStore } from "@/stores/";
import type { IQuizz } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    quizzDetails(quizz: IQuizz) {
      this.quizzStore.setCurrentQuizz(quizz);
      this.$router.push(`/quizzDetails/${quizz.quizzId}`);
    },
    quizzEdit(quizz: IQuizz) {
      this.quizzStore.setCurrentQuizz(quizz);
      this.$router.push(`/quizzEdit/${quizz.quizzId}`);
    },
    quizzDelete(quizz: IQuizz) {
      this.quizzStore.setCurrentQuizz(quizz);
      this.$router.push(`/quizzDelete/${quizz.quizzId}`);
    },
    quizzCreate() {
      this.$router.push(`/quizzCreate`);
    }
  },
  computed: {
    ...mapStores(useQuizzStore)
  },
  components: {
    ModelTitle
  }
})
</script>