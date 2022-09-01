import { defineStore } from 'pinia'
import type { IQuestion } from '@/models/'
import { QuestionApi } from "@/api/";
import { useAuthStore } from '../AuthStore';
import { useQuizzQuestionStore } from './QuizzQuestionStore';
import { useQuizzStore } from './QuizzStore';
import { useThemeStore } from './ThemeStore';

export const useQuestionStore = defineStore({
  id: 'question',
  state: () => ({
    currentQuestion: {} as IQuestion,
    questions: [] as IQuestion[],
    filteredQuestions: [] as IQuestion[]
  }),
  getters: {
    getCurrentQuestion: (state) => state.currentQuestion,
    getQuestions: (state) => state.questions,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentQuestion(question: IQuestion): void {
      this.currentQuestion = question;
    },
    setQuestions(questions: IQuestion[]): void {
      this.questions = questions
    },
    getQuestionById(questionId: number): IQuestion {
      return this.questions.find(q => q.questionId == questionId) as IQuestion;
    },
    getQuestionsByIds(questionsIds: number[]): IQuestion[] { // assumes that all ids are correct
      const questionsToReturn = [] as IQuestion[];
      questionsIds.forEach(questionId =>
          questionsToReturn.push(this.questions.find(q => q.questionId == questionId) as IQuestion));
      return questionsToReturn;
    },
    getFilteredQuestionsUser(userId : number){
      return this.questions.filter(q => q.status != 'Deleted' && q.userId == userId);
    },
    getQuestionsNotInQuizz() : IQuestion[]{
      let questionsToFind : IQuestion[] = this.questions.filter(q => q.userId == useAuthStore().user.userId && q.status != "Deleted");
      let questionsByQuizz = this.getQuestionsByQuizz();
      questionsByQuizz.forEach(question => {
        let index = questionsToFind.indexOf(question);
        questionsToFind.splice(index, 1);
      })
      return questionsToFind;
    },
    getQuestionsByQuizz(){
      let quizzQuestionsIds : number[] = useQuizzQuestionStore().getQuizzQuestionsByQuizz(useQuizzStore().getCurrentQuizz.quizzId);
      let questionsToReturn : IQuestion[] = [];
      quizzQuestionsIds.forEach(quizzQuestionId => {
        let question = this.questions.find(q => q.questionId == quizzQuestionId);
        questionsToReturn.push(question as IQuestion);
      })
      return questionsToReturn;
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addQuestion(question: IQuestion): void {
      this.questions.push(question);
      this.currentQuestion = question;
    },
    updateQuestion(question: IQuestion): void {
      const index = this.questions.findIndex((u) => u.questionId == question.questionId);
      this.questions.splice(index, 1, question);
      this.currentQuestion = question;
    },
    deleteQuestion(questionId: number): void {
      const index = this.questions.findIndex((u) => u.questionId == questionId);
      this.questions.splice(index, 1);
    },
    clearStore() {
      this.questions = [];
      this.filteredQuestions = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IQuestion[] {
      QuestionApi.getAll()
        .then((response) => {
          this.questions = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuestionStore - apiGetAll() | ", error);
        })
      return this.questions;
    },
    apiGetById(questionId: number): IQuestion {
      QuestionApi.getById(questionId)
        .then((response) => {
          this.currentQuestion = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuestionStore - apiGetById(questionId) | ", error);
        })
        return this.currentQuestion;
    },
    apiCreate(question: IQuestion): boolean {
      QuestionApi.create(question)
        .then((response) => {
          if (response.status == 200) {
            this.addQuestion(response.data);
            console.log('%c Success! Question created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuestionStore - apiCreate(question)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuestionStore - apiCreate(question) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(question: IQuestion): boolean  {
      QuestionApi.update(question)
        .then((response) => {
          if (response.data == true) {
            this.updateQuestion(question);
            console.log('%c Success! Question updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuestionStore - apiUpdate(question)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuestionStore - apiUpdate(question) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(questionId: number): boolean  {
      QuestionApi.delete(questionId)
        .then((response) => {
          if (response.data == true) {
            this.deleteQuestion(questionId);
            console.log('%c Success! Question deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuestionStore - apiDelete(questionId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuestionStore - apiDelete(questionId) | ", error);
          return false;
        })
        return true;
    },
    apiGetAllQuestionsBase() : IQuestion[]{
      QuestionApi.getAllQuestionsBase()
      .then((response) => {
        let questions : IQuestion[] = response.data;
        questions.forEach(question => this.questions.push(question));
      })
      .catch((error) => {
        console.error("Error: API connection | Location: QuestionStore - apiGetAllQuestionsAdmin() | ", error);
      })
      return this.questions;
    },
    apiGetAllQuestionsUser(userId: number) : IQuestion[]{
      QuestionApi.getAllQuestionsUser(userId)
      .then((response) => {
        this.questions = [...this.questions , ...response.data];
      })
      .catch((error) => {
        console.error("Error: API connection | Location: QuestionStore - apiGetAllQuestionsUser() | ", error);
      })
      return this.questions;
    },
    //Other methods
    //Create string base64 from image
    async createBase64Image(imageFile : File) : Promise<string> {
      return new Promise((resolve) => {
        const reader = new FileReader();
        reader.onload = () => {
           let imageString = reader.result as string;
            return resolve(imageString.split(',')[1]);
        }
        return reader.readAsDataURL(imageFile);
      })
    },
    //Filter, Sort and Search methods
    filterQuestions(themeOption: any) { 
      if (themeOption !== 'All') {
        let themeId = useThemeStore().getThemeId(themeOption);
        return this.filteredQuestions.filter(q => q.themeId == themeId);
      } else {
        return this.filteredQuestions;
      }
    },
    sortQuestions(selectId: any) {
      if (selectId == 'UserId') {
        return this.filteredQuestions.sort((a, b) => a.userId > b.userId ? 1 : -1);
      }
      else if (selectId == 'QuestionId') {
        return this.filteredQuestions.sort((a, b) => a.questionId > b.questionId ? 1 : -1);
      }
      else if (selectId == 'ThemeId') {
        return this.filteredQuestions.sort((a, b) => a.themeId > b.themeId ? 1 : -1);
      } else {
        return this.filteredQuestions;
      }
    },
    searchQuestions(term: any) { 
      if (!term .length) {
        return this.filteredQuestions;
      } else {       
        return this.filteredQuestions.filter(q => q.questionText.toLowerCase().includes(term.toLowerCase()))
      }
    },
    megaFilter(themeOption: any, selectId: any, term: any) { 
      this.filteredQuestions = this.questions.filter(q => q.status != "Deleted" && q.userId == useAuthStore().user.userId);
      if (themeOption){
        this.filteredQuestions = this.filterQuestions(themeOption);
      }
      if(selectId != ''){
        this.filteredQuestions = this.sortQuestions(selectId);
      }
      if(term){
        this.filteredQuestions = this.searchQuestions(term);
      }
      return this.filteredQuestions;
    },
    //Filter for adding questions to quizz that don't exist on that quizz
    megaFilterAddQuestions(themeOption: any, selectId: any, term: any) {
      this.filteredQuestions = this.getQuestionsNotInQuizz();
      if (themeOption.length){
        this.filteredQuestions = this.filterQuestions(themeOption);
      }
      if(selectId != ''){
        this.filteredQuestions = this.sortQuestions(selectId);
      }
      if(term){
        this.filteredQuestions = this.searchQuestions(term);
      }
      return this.filteredQuestions;
    },
  }
})
