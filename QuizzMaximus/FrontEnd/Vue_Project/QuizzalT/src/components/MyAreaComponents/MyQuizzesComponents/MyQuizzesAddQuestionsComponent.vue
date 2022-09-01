<template>
    <div class="container" id="addQuestionsContainer">
        <h2 class="text-center">My Questions</h2>
        <Filters :filterQuestions="filterQuestions" :sortQuestions="sortQuestions" :searchQuestions="searchQuestions" />
        <MyQuizzesQuestionPopUp v-if="triggerPopUp" @close-pop-up="closePopUp()"></MyQuizzesQuestionPopUp>
        <div v-if="!getQuestions().length">
            <p class="text-center">No questions that can be added to this Quizz</p>
            <p class="text-center">Please create more questions</p>
        </div>
        <div v-else class="text-center">
            <table class="table" v-for="item in getQuestions()" id="addQuestionsTable">
                <thead>
                    <th>Name</th>
                    <th>Question</th>
                    <th>Difficulty</th>
                    <th>Theme</th>
                    <th></th>
                </thead>
                <tbody>
                    <td> {{ item.questionName }} </td>
                    <td> {{ item.questionText }} </td>
                    <td> {{ item.difficulty }} </td>
                    <td> {{ item.themeId }} </td>
                    <td>
                        <button @click="questionDetails(item)" class="tableButtons">Details</button>
                        <button @click="questionSelect(item)" class="tableButtons">Select</button>
                    </td>
                </tbody>
            </table>
            <span v-if="!selectedQuestions.length">
                <h2 class="selectedQuestion">No Questions Selected</h2>
            </span>
            <span v-else>
                <h2 class="selectedQuestion">Selected Questions</h2>
                <table class="selectQuestionsDiv" v-for="item in selectedQuestions">
                    <thead>
                        <th class="selectedQuestionHeader">Question Name</th>
                        <th class="selectedQuestionHeader">Question</th>
                        <th></th>
                    </thead>
                    <tbody>
                        <td class="selectedQuestion">{{ item.questionName }}</td>
                        <td class="selectedQuestion">{{ item.questionText }}</td>
                        <td><button @click="questionRemove(item)" class="buttonsCreate">Remove</button></td>
                    </tbody>
                </table>
                <button @click="questionAdd()" class="buttonsCreate">Add To Quizz</button>
            </span>
        </div>
        <div class="buttonsCreateDiv">
            <button @click="back()" class="buttonsCreate">Back</button>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import type { IQuestion, IQuizzQuestion } from "@/models";
import { useAuthStore, useQuestionStore, useQuizzQuestionStore, useQuizzStore } from "@/stores";
import Filters from "@/components/Filters/MyQuestionFilters.vue";
import ModelTitle from "@/components/ModelTitle.vue";
import MyQuizzesQuestionPopUp from "./MyQuizzesQuestionPopUp.vue";

export default defineComponent({
    data(){
        return {
            selectedQuestions: [] as IQuestion[],
            quizzQuestions: [] as IQuizzQuestion[],
            filter: {} as any,
            sort: {} as any,
            search: {} as any,
            triggerPopUp: false
        }
    },
    methods:{
        questionDetails(question : IQuestion){
            this.questionStore.setCurrentQuestion(question);
            this.triggerPopUp = true;
        },
        questionSelect(question : IQuestion){
            if(!this.selectedQuestions.find(q => q.questionId == question.questionId)){
                this.selectedQuestions.push(question);
            }
        },
        questionAdd(){
            this.selectedQuestions.forEach(selectedQuestion => {
                let quizzQuestionToAdd : IQuizzQuestion = {
                    quizzId: useQuizzStore().getCurrentQuizz.quizzId,
                    questionId: selectedQuestion.questionId
                }
                this.quizzQuestions.push(quizzQuestionToAdd as IQuizzQuestion);
            });
            useQuizzQuestionStore().apiCreateQuizzQuestions(this.quizzQuestions);
            let quizz = useQuizzStore().getCurrentQuizz;
            quizz.status = 'Published';
            this.quizzStore.apiUpdate(quizz);
            this.$emit('changePage', 'MyQuizzesComponent');
        },
        questionRemove(question : IQuestion){
            let indexQuestion = this.selectedQuestions.indexOf(question);
            this.selectedQuestions.splice(indexQuestion, 1);
        },
        closePopUp(){
            this.triggerPopUp = false;
        },
        back(){
            this.$emit('changePage', 'MyQuizzesDetailsComponent');
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
    computed:{
        ...mapStores(useAuthStore, useQuestionStore, useQuizzStore, useQuizzQuestionStore),
        get: function() : IQuestion[]{
            return this.questionStore.megaFilterAddQuestions(this.filter, this.sort, this.search)
        }
    },
    components: {
        Filters, ModelTitle, MyQuizzesQuestionPopUp
    }
})
</script>