<template>
    <div class="container">
        <h4>Create Question</h4>
        <form @submit.prevent="createQuestion()">
            <div class="mb-3">
                <input type="hidden" v-bind="questionAnswers.question.userId = authStore.user.userId">
            </div>
            <div class="mb-3">
                <label for="questionName">Question Name</label>
                <input v-model="questionAnswers.question.questionName" type="text" class="form-control" id="inputQuestionName">
            </div>
            <div class="mb-3">
                <label for="inputQuestionText">Question Text</label>
                <input v-model="questionAnswers.question.questionText" type="text" class="form-control" id="inputQuestionText">
            </div>
            <div class="mb-3">
                <p>Difficulty</p>
                <select class="dropdown col-2" v-model="selectedDifficulty" @change="setDifficulty()" id="selectDifficulty">
                    <option v-for="item in difficultyNumbers">{{ item }}</option>
                </select>
            </div>
            <div class="mb-3">
                <p>Theme</p>
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
            <div class="mb-3">
                <label Htmlfor="answerText">Right Answer</label>
                <input v-model="questionAnswers.answers[0].answerText" class="form-control" id="inputAnswerText">
            </div>
            <div v-for="(item, index) in questionAnswers.answers" class="mb-3">
                <span v-if="index > 0">
                    <label Htmlfor="answerText">Wrong Answer</label>
                    <input v-model="item.answerText" class="form-control" id="inputAnswerText">
                </span>
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
import { useQuestionStore, useAuthStore, useThemeStore } from "@/stores";
import type { IQuestion, IAnswer, QuestionAnswer } from "@/models";
import { createQuestionAnswers } from "@/models";

export default defineComponent({
    data(){
        return {
            questionAnswers: { 
                question: {
                    status: 'Draft'
                } as IQuestion,
                answers: [ 
                    { rightAnswer : true },
                    { rightAnswer : false },
                    { rightAnswer : false },
                    { rightAnswer : false }
                    ] as IAnswer[]
            } as QuestionAnswer,
            errorMsg: '',
            previewImage: {} as string | any[],
            themeNames: [] as string [],
            selectedTheme: {} as string,
            difficultyNumbers: [1,2,3,4,5,6,7,8,9,10] as number[],
            selectedDifficulty: {} as number,
        }
    },
    methods: {
        createQuestion(){
            if(this.questionAnswers.answers[0].answerText && this.questionAnswers.answers[1].answerText
            && this.questionAnswers.answers[2].answerText && this.questionAnswers.answers[3].answerText
            && this.questionAnswers.question.questionName && this.questionAnswers.question.questionText
            && this.questionAnswers.question.difficulty && this.questionAnswers.question.themeId){
            createQuestionAnswers(this.questionAnswers);
            this.$emit('changePage', 'MyQuestionsComponent');
            }
            else{
                this.errorMsg = 'Please fill all the fields';
            }
        },
        async receiveImage(e : any){
            if(e != null){
                const selectedImage = e.target.files[0];
                this.questionAnswers.question.questionImageString = await this.questionStore.createBase64Image(selectedImage);
                this.previewImage = this.questionAnswers.question.questionImageString;
            }
        },
        removeImage(){
            this.questionAnswers.question.questionImageString = '';
            this.previewImage = '';
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
        this.themeNames = this.getThemeNames;
    },
    computed: {
        ...mapStores(useAuthStore, useQuestionStore, useThemeStore),
        getThemeNames: function() : string[]{
            return this.themeStore.getThemeNames();
        }
    }
})
</script>