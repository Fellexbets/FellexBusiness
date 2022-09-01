<template>
  <div class="container">
    <ModelTitle title="Answer Index" />
    <table class="table">
      <thead>
        <tr>
          <th>AnswerId</th>
          <th>QuestionId</th>
          <th>AnswerText</th>
          <th>RightAnswer</th>
          <th>DateCreated</th>
          <th>DateEdited</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in answerStore.getAnswers" :key="item.answerId">
          <td> {{ item.answerId }} </td>
          <td> {{ item.questionId }} </td>
          <td> {{ item.answerText }} </td>
          <td> {{ item.rightAnswer }} </td>
          <td> {{ item.dateCreated }} </td>
          <td> {{ item.dateEdited }} </td>
          <td>
            <button class="crudBtn" @click="answerDetails(item)">Details</button>
            <button class="crudBtn" @click="answerEdit(item)">Edit</button>
            <button class="crudBtn" @click="answerDelete(item)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="container">
    <button @click="answerCreate()">Create</button>
  </div>

</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useAnswerStore } from "@/stores/";
import type { IAnswer } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    answerDetails(answer: IAnswer) {
      this.answerStore.setCurrentAnswer(answer);
      this.$router.push(`/answerDetails/${answer.answerId}`);
    },
    answerEdit(answer: IAnswer) {
      this.answerStore.setCurrentAnswer(answer);
      this.$router.push(`/answerEdit/${answer.answerId}`);
    },
    answerDelete(answer: IAnswer){
      this.answerStore.setCurrentAnswer(answer);
      this.$router.push(`/answerDelete/${answer.answerId}`);
    },
    answerCreate() {
      this.$router.push(`/answerCreate`);
    }
  },
  computed:{
    ...mapStores(useAnswerStore)
  },
  components: {
    ModelTitle
  }
})
</script>