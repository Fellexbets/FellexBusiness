<template>
  <div class="container">
    <h4>Create Answer</h4>

    <form @submit.prevent="answerCreate()">
      <div class="mb-3">
        <label for="inputAnswerCreateQuestionId" class="form-label">QuestionId</label>
        <input v-model="answer.questionId" type="questionId" class="form-control" id="inputAnswerCreateQuestionId">
      </div>

      <div class="mb-3">
        <label for="inputAnswerCreateAnswerText" class="form-label">AnswerText</label>
        <input v-model="answer.answerText" type="answerText" class="form-control" id="inputAnswerCreateAnswerText">
      </div>

      <div class="mb-3">
        <label for="inputAnswerCreateRightAnswer" class="form-label">RightAnswer</label>
        <input v-model="answer.rightAnswer" type="checkbox" id="inputAnswerCreateRightAnswer">
      </div>

      <button type="submit">Create</button>
      <button @click="back()">Back</button>
    </form>

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
    answerCreate() {
      this.answerStore.apiCreate(this.answer);
      this.$emit('changePage', 'AnswerIndexComponent');
    },
    back() {
      this.$emit('changePage', 'AnswerIndexComponent');
    }
  },
  computed: {
    ...mapStores(useAnswerStore)
  }
})
</script>