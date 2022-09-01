import { defineStore } from 'pinia'
import type { IQuizzQuestion } from '@/models/'
import { QuizzQuestionApi } from "@/api/";

export const useQuizzQuestionStore = defineStore({
  id: 'quizzQuestion',
  state: () => ({
    currentQuizzQuestion: {} as IQuizzQuestion,
    quizzQuestions: [] as IQuizzQuestion[]
  }),
  getters: {
    getCurrentQuizzQuestion: (state) => state.currentQuizzQuestion,
    getQuizzQuestions: (state) => state.quizzQuestions,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentQuizzQuestion(quizzQuestion: IQuizzQuestion): void {
      this.currentQuizzQuestion = quizzQuestion;
    },
    setQuizzQuestions(quizzQuestions: IQuizzQuestion[]): void {
      this.quizzQuestions = quizzQuestions
    },
    getQuizzQuestionsOfQuizzId(quizzId: number): IQuizzQuestion[] {
      return this.quizzQuestions.filter(qq => qq.quizzId == quizzId);
    },
    getQuestionIdsOfQuizzId(quizzId: number): number[] {
      return [...new Set(this.quizzQuestions.filter(q => q.quizzId == quizzId).map<number>(q => q.questionId))] ;
    },
    getQuestionsNumberOfQuizzId(quizzId: number): number {
      return this.getQuizzQuestionsOfQuizzId(quizzId).length;
    },
    getQuizzQuestionsByQuizz(quizzId : number): number[] {
      let quizzQuestions : IQuizzQuestion[] = this.quizzQuestions.filter(q => q.quizzId == quizzId);
      let quizzQuestionsIds : number[] = [];
      for(let i = 0; i < quizzQuestions.length; ++i){
          quizzQuestionsIds.push(quizzQuestions[i].questionId);
      }
      return quizzQuestionsIds;
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addQuizzQuestion(quizzQuestion: IQuizzQuestion): void {
      this.quizzQuestions.push(quizzQuestion);
      this.currentQuizzQuestion = quizzQuestion;
    },
    addSeveralQuizzQuestions(quizzQuestions : IQuizzQuestion[]){
      quizzQuestions.forEach(quizzQuestion => this.quizzQuestions.push(quizzQuestion));
    },
    updateQuizzQuestion(quizzQuestion: IQuizzQuestion): void {
      const index = this.quizzQuestions.findIndex((u) => u.quizzId == quizzQuestion.quizzId && u.questionId == quizzQuestion.questionId);
      this.quizzQuestions.splice(index, 1, quizzQuestion);
      this.currentQuizzQuestion = quizzQuestion;
    },
    deleteQuizzQuestion(quizzId: number, questionId: number): void {
      const index = this.quizzQuestions.findIndex((u) => u.quizzId == quizzId && u.questionId == questionId);
      this.quizzQuestions.splice(index, 1);
    },
    clearStore() {
      this.quizzQuestions = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IQuizzQuestion[] {
      QuizzQuestionApi.getAll()
        .then((response) => {
          this.quizzQuestions = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiGetAll() | ", error);
        })
      return this.quizzQuestions;
    },
    apiGetById(quizzId: number, questionId: number): IQuizzQuestion {
      QuizzQuestionApi.getById(quizzId, questionId)
        .then((response) => {
          this.currentQuizzQuestion = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiGetById(quizzQuestionId) | ", error);
        })
        return this.currentQuizzQuestion;
    },
    apiCreate(quizzQuestion: IQuizzQuestion): boolean {
      QuizzQuestionApi.create(quizzQuestion)
        .then((response) => {
          if (response.status == 201) {
            this.addQuizzQuestion(response.data);
            console.log('%c Success! QuizzQuestion created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuizzQuestionStore - apiCreate(quizzQuestion)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiCreate(quizzQuestion) | ", error);
          return false;
        })
        return true;
    },
    apiCreateQuizzQuestions(quizzQuestions: IQuizzQuestion[]) {
      QuizzQuestionApi.createQuizzQuestions(quizzQuestions)
        .then((response) => {
          if ((response.status) == 200) {
            this.addSeveralQuizzQuestions(response.data);
            console.log('%c Success! QuizzQuestions created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuizzQuestionStore - apiCreate(quizzQuestion)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiCreate(quizzQuestion) | ", error);
        })
    },
    apiUpdate(quizzQuestion: IQuizzQuestion): boolean {
      QuizzQuestionApi.update(quizzQuestion)
        .then((response) => {
          if (response.data == true) {
            this.updateQuizzQuestion(quizzQuestion);
            console.log('%c Success! QuizzQuestion updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuizzQuestionStore - apiUpdate(quizzQuestion)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiUpdate(quizzQuestion) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(quizzId: number, questionId: number): boolean {
      QuizzQuestionApi.delete(quizzId, questionId)
        .then((response) => {
          if (response.data == true) {
            this.deleteQuizzQuestion(quizzId, questionId);
            console.log('%c Success! QuizzQuestion deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuizzQuestionStore - apiDelete(quizzQuestionId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiDelete(quizzQuestionId) | ", error);
          return false;
        })
        return true;
    },
    apiGetAllQuizzQuestionsUser(userId : number) : IQuizzQuestion[] {
      QuizzQuestionApi.getAllQuizzQuestionsUser(userId)
      .then((response) => {
          if(response.status == 200){
            //this.quizzQuestions = [...this.quizzQuestions , ...response.data];
            let quizzQuestionsToAdd : IQuizzQuestion[] = response.data;
            quizzQuestionsToAdd.forEach(quizzQuestion => this.quizzQuestions.push(quizzQuestion));
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiGetAllQuizzQuestionsUser() | ", error);
        })
        return this.quizzQuestions;
    },
    apiGetAllQuizzQuestionsAdmin() : IQuizzQuestion[] {
      QuizzQuestionApi.getAllQuizzQuestionsAdmin()
      .then((response) => {
          if(response.status == 200){
            //this.quizzQuestions = [...this.quizzQuestions , ...response.data];
            let quizzQuestionsToAdd : IQuizzQuestion[] = response.data;
            quizzQuestionsToAdd.forEach(quizzQuestion => this.quizzQuestions.push(quizzQuestion));
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzQuestionStore - apiGetAllQuizzQuestionsAdmin() | ", error);
        })
        return this.quizzQuestions;
    }
  }
})
