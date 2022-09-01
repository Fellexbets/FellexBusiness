<template>
  <div class="container">
    <h4>Delete Answer</h4>
    <table class="table">
      <thead>
        <tr>
          <th>AnswerId</th>
          <th>QuestionId</th>
          <th>AnswerText</th>
          <th>RightAnswer</th>
          <th>DateCreated</th>
          <th>DateEdited</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ answer.answerId }} </td>
          <td> {{ answer.questionId }} </td>
          <td> {{ answer.answerText }} </td>
          <td> {{ answer.rightAnswer }} </td>
          <td> {{ answer.dateCreated }} </td>
          <td> {{ answer.dateEdited }} </td>
        </tr>
      </tbody>
    </table>
    <p> Are you sure?</p>
    <button @click="answerDelete(answer.answerId)">Delete</button>
    <button @click="back()">Back</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAnswerStore } from "@/stores/";
import type { IAnswer } from "@/models/";

export default defineComponent({
  data() {
    return {
      answer: {} as IAnswer
    }
  },
  methods: {
    answerDelete(answerId: number){
        this.answerStore.apiDelete(answerId);
        this.$router.push(`/answerIndex`);
      },
      back(){
        this.$router.push(`/answerDetails/${this.answer.answerId}`);
      }
  },
  mounted(){
    this.answer = this.answerStore.getCurrentAnswer;
  },
  computed:{
    ...mapStores(useAnswerStore)
  },
  components: {
  }
})
</script>