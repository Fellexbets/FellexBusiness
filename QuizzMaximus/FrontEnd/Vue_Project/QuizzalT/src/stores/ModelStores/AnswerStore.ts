import { defineStore } from 'pinia'
import type { IAnswer } from '@/models/'
import { AnswerApi } from "@/api/";

export const useAnswerStore = defineStore({
  id: 'answer',
  state: () => ({
    currentAnswer: {} as IAnswer,
    answers: [] as IAnswer[]
  }),
  getters: {
    getCurrentAnswer: (state) => state.currentAnswer,
    getAnswers: (state) => state.answers,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentAnswer(answer: IAnswer): void {
      this.currentAnswer = answer;
    },
    setAnswers(answers: IAnswer[]): void {
      this.answers = answers
    },
    /**
     * Get all the answers of the question with given id. 
     */
    getAllAnswersOfQuestion(questionId: number): IAnswer[] {
      return this.answers.filter(a => a.questionId == questionId) as IAnswer[];
    },
     /**
     * Get all the answers of the questions with given ids.
     */
    getAllAnswersOfManyQuestions(questionsIds: number[]): IAnswer[]{
      const answersToReturn = [] as IAnswer[];
      questionsIds.forEach(questionId => 
        this.answers.filter(a => a.questionId == questionId).forEach(ans => answersToReturn.push(ans)));
      //alternative: this.answers.forEach(a => questionsIds.find(questionId => questionId == a.questionId) ? answersToReturn.push(a) : "") 
      return answersToReturn;
    },
    /**
    * Get only the right answer of the question with given id.
    */
    getOneRightAnswerOfQuestion(questionId: number): IAnswer {
      return this.getAllAnswersOfQuestion(questionId).find(a => a.rightAnswer == true) as IAnswer;
    },
    /**
    * Get all the wrong answers of the question with given id.
    */
    getAllWrongAnswersOfQuestion(questionId: number): IAnswer[] {
      return this.getAllAnswersOfQuestion(questionId).filter(a => a.rightAnswer == false) as IAnswer[];
    },
    /**
    * Get three random wrong answers of the question with given id.
    */
    getThreeRandomWrongAnswersOfQuestion(questionId: number): IAnswer[] {
      return this.getAllWrongAnswersOfQuestion(questionId)
                 .sort(function(){ return 0.5 - Math.random()})
                 .slice(0, 3) as IAnswer[];
    },
    /**
    * Get all four questions of the question with given id - one right answer and three wrong in a random sort.
    */
    getFourFinalAnswersOfQuestion(questionId: number): IAnswer[] {
      return [...new Set([this.getOneRightAnswerOfQuestion(questionId), ...this.getThreeRandomWrongAnswersOfQuestion(questionId)])]
              .sort(function(){ return 0.5 - Math.random()});
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addAnswer(answer: IAnswer): void {
      this.answers.push(answer);
      this.currentAnswer = answer;
    },
    addSeveralAnswers(answers : IAnswer[]){
      console.log(answers)
      answers.forEach(answer => this.answers.push(answer));
    },
    updateAnswer(answer: IAnswer): void {
      const index = this.answers.findIndex((u) => u.answerId == answer.answerId);
      this.answers.splice(index, 1, answer);
      this.currentAnswer = answer;
    },
    updateSeveralAnswers(answers: IAnswer[]): void{
      answers.forEach(answer => {
          let index = this.answers.findIndex(q => q.answerId == answer.answerId);
          this.answers.splice(index, 1, answer);
      });
    },
    deleteAnswer(answerId: number): void {
      const index = this.answers.findIndex((u) => u.answerId == answerId);
      this.answers.splice(index, 1);
    }, 
    clearStore() {
      this.answers = [];
    },
    // API methods - sync with store on success only | Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IAnswer[] {
      AnswerApi.getAll()
        .then((response) => {
          this.answers = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AnswerStore - apiGetAll() | ", error);
        })
      return this.answers;
    },
    apiGetById(answerId: number): IAnswer {
      AnswerApi.getById(answerId)
        .then((response) => {
          this.currentAnswer = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AnswerStore - apiGetById(answerId) | ", error);
        })
        return this.currentAnswer;
    },
    apiGetAllAnswersUser(userId : number) {
      AnswerApi.getAllAnswersUser(userId)
        .then((response) => {
          if(response.status == 200){
            let answers : IAnswer[] = response.data;
            answers.forEach(answer => this.answers.push(answer));
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AnswerStore - apiGetAllAnswersUser(answerId) | ", error);
            return false;
        })
    },
    apiCreate(answer: IAnswer): boolean {
      AnswerApi.create(answer)
        .then((response) => {
          if (response.status == 200) {
            this.addAnswer(response.data);
            console.log('%c Success! Answer created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: AnswerStore - apiCreate(answer)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AnswerStore - apiCreate(answer) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(answer: IAnswer): boolean {
      AnswerApi.update(answer)
        .then((response) => {
          if (response.data == true) {
            this.updateAnswer(answer);
            console.log('%c Success! Answer updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: AnswerStore - apiUpdate(answer)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AnswerStore - apiUpdate(answer) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(answerId: number): boolean {
      AnswerApi.delete(answerId)
        .then((response) => {
          if (response.data == true) {
            this.deleteAnswer(answerId);
            console.log('%c Success! Answer deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: AnswerStore - apiDelete(answerId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AnswerStore - apiDelete(answerId) | ", error);
          return false;
        })
        return true;
    },
    apiGetAllAnswersBase(){
      AnswerApi.getAllAnswersBase()
      .then((response) => {
        if(response.status == 200){
          let answers : IAnswer[] = response.data;
          answers.forEach(answer => this.answers.push(answer));
        }
      })
      .catch((error) => {
        console.error("Error: API connection | Location: AnswerStore - apiGetAllAnswersBase() | ", error);
      })
    }
  }
})

