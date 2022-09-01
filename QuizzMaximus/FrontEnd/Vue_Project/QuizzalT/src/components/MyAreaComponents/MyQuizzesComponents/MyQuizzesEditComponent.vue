<template>
    <div class="container">
        <h4>Create Quizz</h4>
        <form @submit.prevent="quizzUpdate()">
            <div class="mb-3">
                <label for="inputQuizzName" class="form-label">Quizz Name</label>
                <input v-model="quizz.quizzName" type="text" class="form-control" id="inputQuizzName">
            </div>
            <div class="buttonsCreateDiv">
                <p class="text-danger">{{ errorMsg }}</p>
                <button type="submit" class="buttonsCreate">Update</button>
                <button @click="back()" class="buttonsCreate">Back</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import type { IQuizz } from '@/models';
import { useQuizzStore, useUserStore } from "@/stores";

export default defineComponent({
    data(){
        return {
            quizz: {} as IQuizz,
            errorMsg: ''
        }
    },
    methods: {
        quizzUpdate(){
            if(this.quizz.quizzName){
                this.quizzStore.apiUpdate(this.quizz);
                this.$emit('changePage', 'MyQuizzesComponent');
            } else {
                this.errorMsg = 'Please insert Quizz Name';
            }
        },
        back(){
            this.$emit('changePage', 'MyQuizzesComponent');
        }
    },
    mounted() {
      this.getQuizz;
    },
    computed: {
      ...mapStores(useQuizzStore, useUserStore),
      getQuizz: function(){
        this.quizz = this.quizzStore.getCurrentQuizz;
      }
    }
})
</script>