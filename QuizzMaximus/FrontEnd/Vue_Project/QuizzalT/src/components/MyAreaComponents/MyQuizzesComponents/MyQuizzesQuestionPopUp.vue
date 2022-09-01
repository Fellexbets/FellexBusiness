<template>
    <div class="popupQuestionDetails">
         <div class="popupInnerQuestionDetails">
            <p class="popUpHeader">Question</p>
            <p >{{ questionStore.getCurrentQuestion.questionText }}</p>
            <p class="popUpHeader">Difficulty</p>
            <p>{{ questionStore.getCurrentQuestion.difficulty }}</p>
            <p class="popUpHeader">Theme ID</p>
            <p>{{ questionStore.getCurrentQuestion.themeId }}</p>
            <p class="popUpHeader">Answers</p>
            <div v-for="item in answerStore.getAllAnswersOfQuestion(questionStore.getCurrentQuestion.questionId)">
                <span v-if="item.rightAnswer == true">
                    <p class="popUpHeader">Right Answer</p>
                </span>
                <span v-else>
                    <p class="popUpHeader">Wrong Answer</p>
                </span>
                <p>{{ item.answerText }}</p>
            </div>
            <div v-if="questionStore.getCurrentQuestion.questionImage">
                <button @click="showImage()">View Image</button>
                <MyQuizzesImagePopUp v-if="showImagePopUp" @close-pop-up="closeImage()"></MyQuizzesImagePopUp>
            </div>
            <br/>
            <button @click="close()">Close</button>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAnswerStore, useQuestionStore } from "@/stores";
import MyQuizzesImagePopUp from "./MyQuizzesImagePopUp.vue";

export default defineComponent({
    data(){
        return{
            showImagePopUp: false
        }
    },
    methods: {
        close() {
            this.$emit("closePopUp");
        },
        showImage(){
            this.showImagePopUp = true;
        },
        closeImage(){
            this.showImagePopUp = false;
        }
    },
    computed: {
        ...mapStores(useQuestionStore, useAnswerStore)
    },
    emits: ["closePopUp"],
    components: {
        MyQuizzesImagePopUp
    }
})
</script>