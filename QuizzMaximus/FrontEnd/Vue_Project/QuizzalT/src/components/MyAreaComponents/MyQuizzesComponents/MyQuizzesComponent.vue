<template>
  <div class="container" id="myQuizzesContainer">
    <h2 class="text-center">My Quizzes</h2>
    <MyQuizzesDeleteComponent v-if="deleteQuizzPopUp" @delete="(decision : boolean) => deleteConfirmed(decision)"></MyQuizzesDeleteComponent>
    <Filters :sortQuizzes="sortQuizzes" :searchQuizzes="searchQuizzes" />
    <div v-if="!getQuizzes().length">
      <p class="text-center">No Quizzes Yet!</p>
    </div>
    <div v-else>
      <div v-for="item in getQuizzes()">
        <table class="table" id="quizzesTable">
          <thead>
            <tr>
              <th>Quizz ID</th>
              <th>Quizz Name</th>
              <th>Date Created</th>
              <th v-if="item.dateEdited != null">Date Edited</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>{{ item.quizzId }}</td>
              <td>{{ item.quizzName }}</td>
              <td>{{ item.dateCreated }}</td>
              <td v-if="item.dateEdited != null">{{ item.dateEdited }}</td>
              <td>
                <button class="mb-1 crudBtn d-flex flex-column" @click="quizzDetails(item)">Details</button>
                <button class="mb-1 crudBtn d-flex flex-column" @click="quizzEdit(item)">Edit</button>
                <button class="mb-1 crudBtn d-flex flex-column" @click="quizzDelete(item)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="myAreaButtonsDiv">
      <button @click="quizzCreate()" class="myAreaButtons">Create a Quizz</button>
      <button @click="back()" class="myAreaButtons">Back</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import type { IQuizz } from "@/models";
import { useAuthStore, useQuizzStore } from "@/stores";
import Filters from "@/components/Filters/MyQuizzesFilters.vue";
import MyQuizzesDeleteComponent from "./MyQuizzesDeleteComponent.vue";

export default defineComponent({
  data() {
    return {
      sort: {} as any,
      search: {} as any,
      deleteQuizzPopUp: false,
      quizzToDelete: {} as IQuizz
    }
  },
  methods: {
    quizzDetails(quizz: IQuizz) {
      this.quizzStore.setCurrentQuizz(quizz);
      this.$emit('changePage', 'MyQuizzesDetailsComponent');
    },
    quizzEdit(quizz: IQuizz) {
      this.quizzStore.setCurrentQuizz(quizz);
      this.$emit('changePage', 'MyQuizzesEditComponent');
    },
    quizzDelete(quizz: IQuizz) {
      this.quizzToDelete = quizz;
      this.deleteQuizzPopUp = true;
    },
    deleteConfirmed(decision : boolean){
      if(decision){
          this.quizzToDelete.status = "Deleted";
          this.quizzStore.apiUpdate(this.quizzToDelete);
          this.deleteQuizzPopUp = false;
      } else {
          this.deleteQuizzPopUp = false;
      }
    },
    quizzCreate() {
      this.$emit('changePage', 'MyQuizzesCreateComponent');
    },
    back() {
      this.$router.push(`/profile`);
    },
    sortQuizzes(themeOption: any) {
      this.sort = themeOption;
    },
    searchQuizzes(term: any) {
      this.search = term; 
    },
    getQuizzes() : IQuizz[]{
      return this.get;
    }
  },
  computed: {
    ...mapStores(useAuthStore, useQuizzStore),
    get: function() : IQuizz[]{
      return this.quizzStore.megaFilter(this.sort, this.search);
    }
  },
  components: {
    Filters,
    MyQuizzesDeleteComponent
}
})
</script>