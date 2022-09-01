<template>
    <div class="container">
      <ModelTitle title="QuizzQuestion Index" />
      <table class="table">
        <thead>
          <tr>
            <th>Quizz Id</th>
            <th>Question Id</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, quizzId)  in quizzQuestionStore.getQuizzQuestions" :key="quizzId">
            <td> {{ item.quizzId }} </td>
            <td> {{ item.questionId  }} </td>
            <td>
              <button class="mb-1 crudBtn d-flex flex-column" @click="quizzQuestionDetails(item)">Details</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="quizzQuestionEdit(item)">Edit</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="quizzQuestionDelete(item)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="container">
      <button @click="quizzQuestionCreate()">Create</button>
    </div>

</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useQuizzQuestionStore } from "@/stores/";
import type { IQuizzQuestion } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    quizzQuestionDetails(quizzQuestion: IQuizzQuestion) {
      this.quizzQuestionStore.setCurrentQuizzQuestion(quizzQuestion);
      this.$emit('changePage', 'QuizzQuestionDetailsComponent');
    },
    quizzQuestionEdit(quizzQuestion: IQuizzQuestion) {
      this.quizzQuestionStore.setCurrentQuizzQuestion(quizzQuestion);
      this.$emit('changePage', 'QuizzQuestionEditComponent');
    },
    quizzQuestionDelete(quizzQuestion: IQuizzQuestion){
      this.quizzQuestionStore.setCurrentQuizzQuestion(quizzQuestion);
      this.$emit('changePage', 'QuizzQuestionDeleteComponent');
    },
    quizzQuestionCreate() {
      this.$emit('changePage', 'QuizzQuestionCreateComponent');
    }
  },
  computed:{
    ...mapStores(useQuizzQuestionStore)
  },
  components: {
    ModelTitle
  },
  emits: ["changePage"]
})
</script>