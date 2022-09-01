<template>
  <div class="container">
    <h4>Delete Quizz</h4>
    <table class="table">
      <thead>
        <tr>
          <th>QuizzId</th>
          <th>UserId</th>
          <th>QuizzName</th>
          <th>Status</th>
          <th>DateCreated</th>
          <th>DateEdited</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ quizz.quizzId }} </td>
          <td> {{ quizz.userId }} </td>
          <td> {{ quizz.quizzName }} </td>
          <td> {{ quizz.status }} </td>
          <td> {{ quizz.dateCreated }} </td>
          <td> {{ quizz.dateEdited }} </td>
        </tr>
      </tbody>
    </table>
    <p> Are you sure?</p>
    <button @click="quizzDelete(quizz.quizzId)">Delete</button>
    <button @click="back()">Back</button>
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
      quizz: {} as IQuizz
    }
  },
  methods: {
    quizzDelete(quizzId: number){
        this.quizzStore.apiDelete(quizzId);
        this.$emit('changePage', 'QuizzIndexComponent');
      },
      back(){
        this.$emit('changePage', 'QuizzIndexComponent');
      }
  },
  mounted(){
    this.quizz = this.quizzStore.getCurrentQuizz;
  },
  computed:{
    ...mapStores(useQuizzStore)
  },
  components: {
  }
})
</script>