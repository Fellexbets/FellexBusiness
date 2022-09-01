<template>
  <div class="container">
    <ModelTitle title="PlayedQuestion Index" />
    <table class="table">
      <thead>
        <tr>
          <th>PlayedQuestionId</th>
          <th>UserId</th>
          <th>QuestionId</th>
          <th>GotItRight</th>
          <th>Points</th>
          <th>DateAnswered</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in playedQuestionStore.getPlayedQuestions" :key="item.playedQuestionId">
          <td> {{ item.playedQuestionId }} </td>
          <td> {{ item.userId }} </td>
          <td> {{ item.questionId }} </td>
          <td> {{ item.gotItRight }} </td>
          <td> {{ item.points }} </td>
          <td> {{ item.dateAnswered }} </td>
          <td>
            <button class="crudBtn" @click="playedQuestionDetails(item)">Details</button>
            <button class="crudBtn" @click="playedQuestionEdit(item)">Edit</button>
            <button class="crudBtn" @click="playedQuestionDelete(item)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="container">
    <button @click="playedQuestionCreate()">Create</button>
  </div>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { usePlayedQuestionStore } from "@/stores/";
import type { IPlayedQuestion } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    playedQuestionDetails(playedQuestion: IPlayedQuestion) {
      this.playedQuestionStore.setCurrentPlayedQuestion(playedQuestion);
      this.$emit('changePage', 'PlayedQuestionDetailsComponent');
    },
    playedQuestionEdit(playedQuestion: IPlayedQuestion) {
      this.playedQuestionStore.setCurrentPlayedQuestion(playedQuestion);
      this.$emit('changePage', 'PlayedQuestionEditComponent');
    },
    playedQuestionDelete(playedQuestion: IPlayedQuestion) {
      this.playedQuestionStore.setCurrentPlayedQuestion(playedQuestion);
      this.$emit('changePage', 'PlayedQuestionDeleteComponent');
    },
    playedQuestionCreate() {
      this.$emit('changePage', 'PlayedQuestionCreateComponent');
    }
  },
  computed: {
    ...mapStores(usePlayedQuestionStore)
  },
  components: {
    ModelTitle
  },
  emits: ["changePage"]
})
</script>