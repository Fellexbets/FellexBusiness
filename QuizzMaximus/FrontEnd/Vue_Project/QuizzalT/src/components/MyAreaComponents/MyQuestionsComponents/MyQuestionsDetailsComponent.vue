<template>
    <div class="container" id="questionDetailsDiv">
        <div v-if="questionStore.getCurrentQuestion.questionImage">
            <h3>Question Image</h3>
            <img :src="`data:image/png;base64,${questionStore.getCurrentQuestion.questionImage}`" id="questionImageDetails"/>
            <br/><br/><br/>
        </div>
        <table class="table">
            <thead>
                <th>Question Name</th>
                <th>Question</th>
                <th>Difficulty</th>
                <th>Theme ID</th>
                <th>Date Created</th>
                <th v-if="questionStore.getCurrentQuestion.dateEdited != null">Date Edited</th>
            </thead>
            <tbody>
                <td>{{ questionStore.getCurrentQuestion.questionName }}</td>
                <td>{{ questionStore.getCurrentQuestion.questionText }}</td>
                <td>{{ questionStore.getCurrentQuestion.difficulty }}</td>
                <td>{{ themeStore.getThemeName(questionStore.getCurrentQuestion.themeId) }}</td>
                <td>{{ questionStore.getCurrentQuestion.dateCreated }}</td>
                <td v-if="questionStore.getCurrentQuestion.dateEdited != null">{{ questionStore.getCurrentQuestion.dateEdited }}</td>
            </tbody>
        </table>
        <h2>Answers</h2>
        <table v-for="item in answerStore.getAllAnswersOfQuestion(questionStore.getCurrentQuestion.questionId)" class="table">
            <thead v-if="item.rightAnswer == true">
                <th>Right Answer</th>
            </thead>
            <thead v-else>
                <th>Wrong Answer</th>
            </thead>
            <tbody>
                <td>{{ item.answerText }}</td>
            </tbody>
        </table>
        <button @click="back()" class="buttonsCreate">Back</button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAnswerStore, useQuestionStore, useThemeStore } from "@/stores";

export default defineComponent({
    methods: {
        back(){
            this.$emit('changePage', 'MyQuestionsComponent');
        }
    },
    computed: {
        ...mapStores(useQuestionStore, useAnswerStore, useThemeStore)
    }
})
</script>