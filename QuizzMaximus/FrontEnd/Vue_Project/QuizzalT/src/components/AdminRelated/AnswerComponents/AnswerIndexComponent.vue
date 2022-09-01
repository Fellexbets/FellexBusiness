<template>
  <div class="container">
    <ModelTitle title="Answer Index" />
    <Filters :filterAnswers="filterAnswers" :search="search" />
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
        <!--<tr v-for="item in answerStore.getAnswers" :key="item.answerId">-->
        <tr v-for="item in answers" :key="item.answerId">
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
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAnswerStore } from "@/stores/";
import type { IAnswer } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";
import { useAuthStore } from "@/stores/AuthStore";
import Filters from "@/components/Filters/AnswersFilters.vue";

export default defineComponent({
  data() {
    return {
      answers: [] as IAnswer[]
    }
  },
  methods: {
    answerDetails(answer: IAnswer) {
      this.answerStore.setCurrentAnswer(answer);
      this.$emit('changePage', 'AnswerDetailsComponent');
    },
    answerEdit(answer: IAnswer) {
      this.answerStore.setCurrentAnswer(answer);
      this.$emit('changePage', 'AnswerEditComponent');
    },
    answerDelete(answer: IAnswer) {
      this.answerStore.setCurrentAnswer(answer);
      this.$emit('changePage', 'AnswerDeleteComponent');
    },
    answerCreate() {
      this.$emit('changePage', 'AnswerCreateComponent');
    },
    filterAnswers(answerr: any) {
      this.resetAnswers()
      if (answerr == 'True') {
        var isTrueSet = (answerr === 'true');
        this.answers = this.answerStore.getAnswers.filter((answer) => {
          return answer.rightAnswer === true
        }
        )
      } else if (answerr == 'False') {
        var isFalseSet = (answerr === 'false');
        this.answers = this.answerStore.getAnswers.filter((answer) => {
          return answer.rightAnswer === false
        })
      }
    },
    search(term: string) {
      this.resetAnswers()
      this.answers = this.answers.filter((answer) => {
        return answer.answerText.toLowerCase().includes(term.toLowerCase())
      })
    },
    resetAnswers() {
      this.answers = this.answerStore.getAnswers;
    }
  },
  mounted() {
    this.answers = this.answerStore.getAnswers
  },

  computed: {
    ...mapStores(useAuthStore, useAnswerStore)
  },
  components: {
    ModelTitle, Filters
  },
  emits: ["changePage"]
})
</script>