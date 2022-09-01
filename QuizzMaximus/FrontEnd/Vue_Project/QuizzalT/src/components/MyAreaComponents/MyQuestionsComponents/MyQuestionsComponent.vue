<template>
  <div class="container" id="myQuestionsContainer">
    <h2 class="text-center">My Questions</h2>
    <MyQuestionsDeleteComponent v-if="deleteQuestionPopUp" @delete="(decision : boolean) => deleteConfirmed(decision)"></MyQuestionsDeleteComponent>
    <Filters :filterQuestions="filterQuestions" :sortQuestions="sortQuestions" :searchQuestions="searchQuestions" />
    <div v-if="!getQuestions().length">
      <p class="text-center">No questions yet</p>
    </div>
    <div v-else id="questionsTableDiv">
      <table class="table" v-for="item in getQuestions()" :key="item.questionId">
        <thead>
          <tr>
            <th>Name</th>
            <th>Question</th>
            <th>Difficulty</th>
            <th>Theme</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td> {{ item.questionName }} </td>
            <td> {{ item.questionText }} </td>
            <td> {{ item.difficulty }} </td>
            <td> {{ themeStore.getThemeName(item.themeId) }} </td>
            <td>
              <button class="mb-1 crudBtn d-flex flex-column" @click="questionDetails(item)">Details</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="questionEdit(item)">Edit</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="questionDelete(item)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="myAreaButtonsDiv">
      <button @click="questionAdd()" class="myAreaButtons">Create Question</button>
      <button @click="back()" class="myAreaButtons">Back</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import type { IQuestion } from "@/models";
import { mapStores } from "pinia";
import { useQuestionStore, useThemeStore } from "@/stores";
import Filters from "@/components/Filters/MyQuestionFilters.vue";
import ModelTitle from "@/components/ModelTitle.vue";
import MyQuestionsDeleteComponent from "./MyQuestionsDeleteComponent.vue";

export default defineComponent({
  data() {
    return {
      filter: {} as any,
      sort: {} as any,
      search: {} as any,
      deleteQuestionPopUp: false,
      questionToDelete: {} as IQuestion
    }
  },
  methods: {
    questionDetails(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$emit('changePage', 'MyQuestionsDetailsComponent');
    },
    questionEdit(question: IQuestion) {
      this.questionStore.setCurrentQuestion(question);
      this.$emit('changePage', 'MyQuestionsEditComponent');
    },
    questionDelete(question : IQuestion) {
      
      this.questionToDelete = question;
      console.log(this.questionToDelete)
      this.deleteQuestionPopUp = true;
    },
    deleteConfirmed(confirm : boolean){
      if(confirm){
        this.questionToDelete.status = "Deleted";
        this.questionStore.apiUpdate(this.questionToDelete);
        this.deleteQuestionPopUp = false;
      } else {
        this.deleteQuestionPopUp = false;
      }
    },
    questionAdd() {
      this.$emit('changePage', 'MyQuestionsCreateComponent');
    },
    back() {
      this.$router.push(`/profile`);
    },
    sortQuestions(selectId: any) {
      this.sort = selectId;
    }, 
    filterQuestions(themeOption: any) {
      this.filter = themeOption;
    },
    searchQuestions(term: any) {
      this.search = term; 
    },
    getQuestions() : IQuestion[]{
      return this.get;
    }
  },
  mounted(){
    this.filter = 'All';
  },
  computed: {
    ...mapStores(useQuestionStore, useThemeStore),
    get: function() :IQuestion[] {
      return this.questionStore.megaFilter(this.filter, this.sort, this.search);
    }
  },
  components: {
    Filters,
    ModelTitle,
    MyQuestionsDeleteComponent
  }
})
</script>