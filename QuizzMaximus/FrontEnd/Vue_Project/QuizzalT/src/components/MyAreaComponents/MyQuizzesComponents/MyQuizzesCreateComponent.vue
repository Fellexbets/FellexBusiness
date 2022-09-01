<template>
  <div class="container" id="myQuizzesCreateContainer">
    <h4>Create Quizz</h4>
    <form @submit.prevent="quizzCreate()">
      <div class="mb-3" >
        <label for="inputQuizzName" class="form-label">Quizz Name</label>
        <input v-model="quizz.quizzName" type="text" class="form-control" id="inputQuizzName">
      </div>
      <div class="buttonsCreateDiv">
        <p class="text-danger">{{ errorMsg }}</p>
        <button type="submit" class="buttonsCreate">Create</button>
        <button @click="back()" class="buttonsCreate">Back</button>
      </div>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import type { IQuizz } from '@/models';
import { useQuizzStore, useAuthStore } from "@/stores";

export default defineComponent({
    data(){
        return {
            quizz: {} as IQuizz,
            errorMsg: ''
        }
    },
    methods: {
      quizzCreate(){
        if(this.quizz.quizzName){
          this.quizz.userId = this.authStore.user.userId;
          this.quizz.status = 'Draft';
          this.quizzStore.apiCreate(this.quizz);
          this.$emit('changePage', 'MyQuizzesAddQuestionsComponent');
        } else {
          this.errorMsg = 'Please insert Quizz Name';
        }
      },
      back(){
            this.$emit('changePage', 'MyQuizzesComponent');
      }
    },
    computed: {
      ...mapStores(useAuthStore, useQuizzStore)
    }
})
</script>