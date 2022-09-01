<template>
  <div class="container">
    <h4>Answer Details</h4>
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
    <button @click="answerEdit(answer)">Edit</button>
    <button @click="answerDelete(answer)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
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
    answerEdit(answer : IAnswer){
      this.answerStore.setCurrentAnswer(answer);
      this.$emit('changePage', 'AnswerEditComponent');
    },
    answerDelete(answer: IAnswer){
      this.answerStore.setCurrentAnswer(answer);
      this.$emit('changePage', 'AnswerDeleteComponent');
    },
    back() {
      this.$emit('changePage', 'AnswerIndexComponent');
    },

  },
  mounted(){
        this.answer = this.answerStore.getCurrentAnswer;
  },
  computed: {
    ...mapStores(useAnswerStore)
  }
})
</script>