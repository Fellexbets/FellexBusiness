<template>
    <div class="container" id="myQuizzesDetailsContainer">
        <div v-if="!questionStore.getQuestionsByQuizz().length">
          <p class="text-center">No questions yet</p>
        </div>
        <div v-else>
            <MyQuizzesQuestionPopUp v-if="triggerPopUp" @close-pop-up="closePopUp()"></MyQuizzesQuestionPopUp>
            <div v-for="item in questionStore.getQuestionsByQuizz()" :key="item.questionId">
                <table class="table" id="quizzesDetailsTable">
                    <thead>
                        <th>Question</th>
                        <th>Difficulty</th>
                        <th>Status</th>
                        <th>Theme</th>
                        <th></th>
                    </thead>
                    <tbody>
                        <td>{{ item.questionText }}</td>
                        <td>{{ item.difficulty }}</td>
                        <td>{{ item.status }}</td>
                        <td>{{ item.themeId }}</td>
                        <td><button @click="questionDetails(item)">Details</button></td>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="buttonsCreateDiv">
            <button @click="questionAdd()" class="buttonsCreate">Add Questions</button>
            <button @click="back()" class="buttonsCreate">Back</button>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuestionStore, useAnswerStore } from "@/stores";
import type { IQuestion } from "@/models";
import MyQuizzesQuestionPopUp from "./MyQuizzesQuestionPopUp.vue";

export default defineComponent({
    data(){
        return {
           MyQuizzesQuestionPopUp,
           triggerPopUp : false
        }
    },
    methods: {
        questionAdd(){
            this.$emit('changePage', 'MyQuizzesAddQuestionsComponent');
        },
        questionDetails(question : IQuestion){
            this.questionStore.setCurrentQuestion(question);
            this.triggerPopUp = true;
        },
        closePopUp(){
            this.triggerPopUp = false;
        },
        back(){
            this.$emit('changePage', 'MyQuizzesComponent');
        }
    },
    computed: {
        ...mapStores(useQuestionStore, useAnswerStore)
    },
    components: {
        MyQuizzesQuestionPopUp
    },
    emits: ["changePage"]
})
</script>