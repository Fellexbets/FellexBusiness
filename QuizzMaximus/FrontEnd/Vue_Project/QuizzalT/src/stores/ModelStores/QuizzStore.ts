import { defineStore } from 'pinia';
import type { IQuestion, IQuizz, IQuizzQuestion } from '@/models/';
import { QuizzApi, QuizzQuestionApi } from "@/api/";
import { useAuthStore } from '../AuthStore';

export const useQuizzStore = defineStore({
  id: 'quizz',
  state: () => ({
    currentQuizz: {} as IQuizz,
    quizzes: [] as IQuizz[],
    filteredQuizzes:[] as IQuizz[]
  }),
  getters: {
    getCurrentQuizz: (state) => state.currentQuizz,
    getQuizzes: (state) => state.quizzes,
    getPublishedQuizzes: (state) => state.quizzes.filter(q => q.status == "Published")
  },
  actions: {
    // Store methods - gets and sets
    setCurrentQuizz(quizz: IQuizz): void {
      this.currentQuizz = quizz;
    },
    setQuizzes(quizzes: IQuizz[]): void {
      this.quizzes = quizzes
    },
    /**
     * Returns the stored quizz with the given id. 
     */
    getQuizzById(quizzId: number): IQuizz {
      return this.quizzes.find((u) => u.quizzId == quizzId) as IQuizz;
    },
    getRandomQuizzId(): number {
      const distinctQuizzesId = [...new Set(this.getPublishedQuizzes.map(q => q.quizzId))];  //make an array of existing QuizzId (only published ones)
      let randomIndex = Math.floor(Math.random() * distinctQuizzesId.length);                //make random index for the QuizzId array
      return distinctQuizzesId[randomIndex];                                                 //return a random value of the QuizzId array
    },
    /**
    * Returns a random quizz. 
    */
    getRandomQuizz(): IQuizz {
      return this.getQuizzById(this.getRandomQuizzId()) as IQuizz;
    },
    getQuizzesByUser(userId : number): IQuizz[] {
      return this.quizzes.filter(u => u.userId == userId);
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addQuizz(quizz: IQuizz): void {
      this.quizzes.push(quizz);
      this.currentQuizz = quizz;
    },
    updateQuizz(quizz: IQuizz): void {
      const index = this.quizzes.findIndex((u) => u.quizzId == quizz.quizzId);
      this.quizzes.splice(index, 1, quizz);
      this.currentQuizz = quizz;
    },
    deleteQuizz(quizzId: number): void {
      const index = this.quizzes.findIndex((u) => u.quizzId == quizzId);
      this.quizzes.splice(index, 1);
    },
    clearStore() {
      this.quizzes = [];
      this.filteredQuizzes = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IQuizz[] {
      QuizzApi.getAll()
        .then((response) => {
          let quizzesToAdd : IQuizz[] = response.data;
          quizzesToAdd.forEach(quizz => this.quizzes.push(quizz));
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzStore - apiGetAll() | ", error);
        })
      return this.quizzes;
    },
    apiGetById(quizzId: number): IQuizz {
      QuizzApi.getById(quizzId)
        .then((response) => {
          this.currentQuizz = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzStore - apiGetById(quizzId) | ", error);
        })
        return this.currentQuizz;
    },
    apiCreate(quizz: IQuizz): boolean {
      QuizzApi.create(quizz)
        .then((response) => {
          if (response.status == 200) {
            this.addQuizz(response.data);
            console.log('%c Success! Quizz created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuizzStore - apiCreate(quizz)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzStore - apiCreate(quizz) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(quizz: IQuizz): boolean {
      QuizzApi.update(quizz)
        .then((response) => {
          if (response.data == true) {
            this.updateQuizz(quizz);
            console.log('%c Success! Quizz updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuizzStore - apiUpdate(quizz)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzStore - apiUpdate(quizz) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(quizzId: number): boolean {
      QuizzApi.delete(quizzId)
        .then((response) => {
          if (response.data == true) {
            this.deleteQuizz(quizzId);
            console.log('%c Success! Quizz deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: QuizzStore - apiDelete(quizzId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuizzStore - apiDelete(quizzId) | ", error);
          return false;
        })
        return true;
    },
    apiGetAllQuizzesUser(userId: number) : IQuizz[]{
      QuizzApi.getAllQuizzesUser(userId)
      .then((response) => {
        let quizzesToAdd : IQuizz[] = response.data;//[...this.quizzes , ...response.data];
        quizzesToAdd.forEach(quizz => this.quizzes.push(quizz));
      })
      .catch((error) => {
        console.error("Error: API connection | Location: QuizzStore - apiGetAllQuizzesUser() | ", error);
      })
    return this.quizzes;
    },
    apiGetAllQuizzesAdmin() : IQuizz[]{
      QuizzApi.getAllQuizzesAdmin()
      .then((response) => {
        let quizzesToAdd : IQuizz[] = response.data;//[...this.quizzes , ...response.data];
        quizzesToAdd.forEach(quizz => this.quizzes.push(quizz));
      })
      .catch((error) => {
        console.error("Error: API connection | Location: QuizzStore - apiGetAllQuizzesAdmin() | ", error);
      })
    return this.quizzes;
    },
    // game methods

    //metodos João
    //generates 10 random numbers
    generateQuizzQuestions(questions : IQuestion[]): number[] {
      let questionIds : number[] = [];
      for(let i = 0; i < 10; ++i){
          let randomNumber = Math.ceil(Math.random() * questions.length);
          if(questionIds.indexOf(randomNumber) !== -1){
            --i;
          }
          else{
            questionIds[i] = randomNumber;
          }
        }
        return questionIds;
    },
    //generates random quizz
    createRandomQuizz(quizzQuestions: IQuizzQuestion[], questionIds: number[]) {
      let quizzQuestionsCompare: IQuizzQuestion[] = this.filterQuizzQuestions(quizzQuestions, questionIds);
      let quizzIdToFind: number = this.compareQuizzQuestions(quizzQuestionsCompare, questionIds);

      if (quizzIdToFind == 0) {
        let newQuizz: IQuizz = {
          quizzId: 0,
          userId: 1,
          quizzName: 'randomQuizz',
          status: 'Draft'
        }
        QuizzApi.create(newQuizz)
          .then((response) => {
            newQuizz.quizzId = response.data.quizzId;
            console.log(newQuizz)
            this.setCurrentQuizz(newQuizz as IQuizz);
            for (let i = 0; i < questionIds.length; ++i) {
              let quizzQuestionAdd: IQuizzQuestion = {
                quizzId: response.data.quizzId,
                questionId: questionIds[i]
              }
              QuizzQuestionApi.create(quizzQuestionAdd)
                .then((response) => {
                })
                .catch((error) => {
                  console.log(error);
                })
            }
          })
          .catch((error) => {
            console.error(error);
          })
        return newQuizz;
      } 
      else {
        let quizzes: IQuizz[] = this.getQuizzes;
        let quizzExists = quizzes.find(q => q.quizzId == quizzIdToFind);
        return quizzExists;
      }
    },
    //filters QuizzQuestions by QuestionId
    filterQuizzQuestions(quizzQuestions : IQuizzQuestion[], questionIds : number[]) {
      let quizzQuestionsCompare : IQuizzQuestion[] = [];
      for(let i = 0; i < questionIds.length; ++i) {
        let quizzQuestionsToAdd : IQuizzQuestion[] = quizzQuestions.filter(q => q.questionId == questionIds[i]);
        for(let j = 0; j < quizzQuestionsToAdd.length; ++j){
          quizzQuestionsCompare.push(quizzQuestionsToAdd[j]);
        }
      }
      return quizzQuestionsCompare;
    },
    //checks if a quizz with an array of QuizzQuestions already exists
    compareQuizzQuestions(quizzQuestions : IQuizzQuestion[], questionIds : number[]) {
      let count = 0;
      let quizzId = 0;
      for(let i = 0; i < quizzQuestions.length; ++i) {
          let quizzQuestionIds = quizzQuestions.filter(q => q.quizzId == quizzQuestions[i].quizzId);
          for(let j = 0; j < quizzQuestionIds.length; ++j) {
              if(quizzQuestionIds[j].questionId == questionIds[j] && quizzQuestionIds.length == questionIds.length) {
                quizzId = quizzQuestionIds[j].quizzId;
                count++;
              }
          }
          if (count == questionIds.length) {
              return quizzId;
          }
          else {
              count = 0;
              quizzId = 0;
          }
      }
      return quizzId;
    },
    //Other methods
    sortQuizzes(selectQuizz: any) {
      if (selectQuizz == 'Asc') {
        return this.filteredQuizzes.sort((a, b) => a.quizzId > b.quizzId ? 1 : -1);
      }
      else if (selectQuizz == 'Desc') {
        return this.filteredQuizzes.sort((a, b) => a.quizzId < b.quizzId ? 1 : -1);
      } else {
        return this.filteredQuizzes;
      }
    },
    searchQuizzes(term: any) { 
      if (!term .length) {
        return this.filteredQuizzes;
      } else {       
        return this.filteredQuizzes.filter(q => q.quizzName.toLowerCase().includes(term.toLowerCase()));
      }
    },
    //Returns filtered quizzes
    megaFilter(selectQuizz: any, term:any) { 
      this.filteredQuizzes = this.quizzes.filter(q => q.status != "Deleted" && q.userId == useAuthStore().user.userId);
      if(selectQuizz.length){
        this.filteredQuizzes = this.sortQuizzes(selectQuizz);
      }
      if(term){
        this.filteredQuizzes = this.searchQuizzes(term);
      }
      return this.filteredQuizzes;
    }
  }
})