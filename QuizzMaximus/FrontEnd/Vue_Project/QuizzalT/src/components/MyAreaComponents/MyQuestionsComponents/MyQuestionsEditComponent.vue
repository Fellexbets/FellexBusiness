<template>
    <div class="container">
        <form @submit.prevent="editQuestion()">
            <div class="mb-3">
                <label for="questionName">Question Name</label>
                <input v-model="questionAnswers.question.questionName" type="text" class="form-control" id="inputQuestionName">
            </div>
            <div class="mb-3">
                <label for="inputQuestionText">Question Text</label>
                <input v-model="questionAnswers.question.questionText" type="text" class="form-control" id="inputQuestionText">
            </div>
            <div class="mb-3">
                <select class="dropdown col-2" v-model="selectedDifficulty" @change="setDifficulty()" id="selectDifficulty">
                    <option v-for="item in difficultyNumbers">{{ item }}</option>
                </select>
            </div>
            <div class="mb-3">
                <select class="dropdown col-2" v-model="selectedTheme" @change="setTheme()" id="selectTheme">
                    <option v-for="item in themeNames">{{ item }}</option>
                </select>
            </div>
            <div class="mb-3" v-if="previewImage.length">
                <h3 class="text-center">Image Preview</h3>
                <div class="imageDiv">
                    <img :src="`data:image/png;base64,${previewImage}`" alt="questionImage" id="imagePreview"/>
                </div>
                <div class="imageDiv">
                    <button @click.prevent="removeImage()">Remove Image</button>
                </div>
            </div>
            <div class="mb-3" v-else>
                <label Htmlfor="inputImage">Image (optional)</label>
                <input type="file" @change="receiveImage" accept="image/x-png,image/gif,image/jpeg" class="form-control" id="inputImage">
            </div>
            <span v-for="(item, index) in questionAnswers.answers">
                <div class="mb-3" v-if="index == 0">
                    <label Htmlfor="answerText">Right Answer</label>
                    <input v-model="questionAnswers.answers[index].answerText" class="form-control" id="inputAnswerText">
                </div>
                <div class="mb-3" v-else>
                    <label Htmlfor="answerText">Wrong Answer</label>
                    <input v-model="questionAnswers.answers[index].answerText" class="form-control" id="inputAnswerText">
                </div>
            </span>
            <div class="buttonsCreateDiv">
                <p class="text-danger">{{ errorMsg }}</p>
                <button type="submit" class="buttonsCreate">Update</button>
            </div>
        </form>
        <div class="buttonsCreateDiv">
            <button @click="back()" class="buttonsCreate">Back</button>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuestionStore, useAnswerStore, useAuthStore, useThemeStore } from "@/stores";
import type { IQuestion, IAnswer, QuestionAnswer } from "@/models";
import { updateQuestionAnswers } from "@/models";

export default defineComponent({
    data(){
        return {
            questionAnswers: { 
                question: {
                    status: 'Draft'
                } as IQuestion,
                answers: [] as IAnswer[]
            } as QuestionAnswer,
            previewImage: {} as string | any[],
            themeNames: [] as string [],
            selectedTheme: {} as string,
            difficultyNumbers: [1,2,3,4,5,6,7,8,9,10] as number[],
            selectedDifficulty: {} as number,
            errorMsg : ''
        }
    },
    methods: {
        editQuestion(){
            if(this.questionAnswers.answers[0].answerText && this.questionAnswers.answers[1].answerText
            && this.questionAnswers.answers[2].answerText && this.questionAnswers.answers[3].answerText
            && this.questionAnswers.question.questionName && this.questionAnswers.question.questionText
            && this.questionAnswers.question.difficulty && this.questionAnswers.question.themeId){
                updateQuestionAnswers(this.questionAnswers);
                this.$emit('changePage', 'MyQuestionsComponent');
            } else {
                this.errorMsg = 'Please fill all the fields';
            }
        },
        async receiveImage(e : any){
            const selectedImage = e.target.files[0];
            this.questionAnswers.question.questionImageString = await this.questionStore.createBase64Image(selectedImage);
            this.previewImage = this.questionAnswers.question.questionImageString;
        },
        removeImage(){
            this.questionAnswers.question.questionImageString = '';
            this.previewImage = '';
        },
        convertImageAtLoad(){
            this.previewImage = this.questionAnswers.question.questionImage;
            this.questionAnswers.question.questionImageString = this.questionAnswers.question.questionImage.toString();
        },
        setTheme(){
            this.questionAnswers.question.themeId = this.themeStore.getThemeId(this.selectedTheme);
        },
        setDifficulty(){
            this.questionAnswers.question.difficulty = this.selectedDifficulty;
        },
        back(){
            this.$emit('changePage', 'MyQuestionsComponent');
        }
    },
    mounted(){
        this.questionAnswers.question = this.questionStore.getCurrentQuestion;
        this.themeNames = this.themeStore.getThemeNames();
        this.selectedTheme = this.themeStore.getThemeName(this.questionAnswers.question.themeId);
        this.selectedDifficulty = this.questionStore.getCurrentQuestion.difficulty;
        if(this.questionAnswers.question.questionImage != null){
            this.convertImageAtLoad();
        }
        this.questionAnswers.answers = this.answerStore.getAllAnswersOfQuestion(this.questionStore.getCurrentQuestion.questionId);
        console.log(this.questionAnswers.answers);
    },
    computed: {
        ...mapStores(useAuthStore, useQuestionStore, useAnswerStore, useThemeStore),
        getThemeNames: function() : string[]{
            return this.themeStore.getThemeNames();
        }
    }
})
</script>