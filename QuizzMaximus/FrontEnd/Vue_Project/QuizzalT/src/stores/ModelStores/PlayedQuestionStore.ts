import { defineStore } from 'pinia'
import type { IPlayedQuestion } from '@/models/'
import { PlayedQuestionApi } from "@/api/";

export const usePlayedQuestionStore = defineStore({
  id: 'playedQuestion',
  state: () => ({
    currentPlayedQuestion: {} as IPlayedQuestion,
    playedQuestions: [] as IPlayedQuestion[]
  }),
  getters: {
    getCurrentPlayedQuestion: (state) => state.currentPlayedQuestion,
    getPlayedQuestions: (state) => state.playedQuestions,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentPlayedQuestion(playedQuestion: IPlayedQuestion): void {
      this.currentPlayedQuestion = playedQuestion;
    },
    setPlayedQuestions(playedQuestions: IPlayedQuestion[]): void {
      this.playedQuestions = playedQuestions
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addPlayedQuestion(playedQuestion: IPlayedQuestion): void {
      this.playedQuestions.push(playedQuestion);
      this.currentPlayedQuestion = playedQuestion;
    },
    addManyPlayedQuestions(playedQuestions: IPlayedQuestion[]): void {
      playedQuestions.forEach(pQ => this.playedQuestions.push(pQ));
    },
    updatePlayedQuestion(playedQuestion: IPlayedQuestion): void {
      const index = this.playedQuestions.findIndex((u) => u.playedQuestionId == playedQuestion.playedQuestionId);
      this.playedQuestions.splice(index, 1, playedQuestion);
      this.currentPlayedQuestion = playedQuestion;
    },
    deletePlayedQuestion(playedQuestionId: number): void {
      const index = this.playedQuestions.findIndex((u) => u.playedQuestionId == playedQuestionId);
      this.playedQuestions.splice(index, 1);
    },
    clearStore() {
      this.playedQuestions = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IPlayedQuestion[] {
      PlayedQuestionApi.getAll()
        .then((response) => {
          this.playedQuestions = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuestionStore - apiGetAll() | ", error);
        })
      return this.playedQuestions;
    },
    apiGetById(playedQuestionId: number): IPlayedQuestion {
      PlayedQuestionApi.getById(playedQuestionId)
        .then((response) => {
          this.currentPlayedQuestion = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuestionStore - apiGetById(playedQuestionId) | ", error);
        })
        return this.currentPlayedQuestion;
    },
    apiCreate(playedQuestion: IPlayedQuestion): boolean {
      PlayedQuestionApi.create(playedQuestion)
        .then((response) => {
          if (response.status == 200) {
            this.addPlayedQuestion(response.data);
            console.log('%c Success! PlayedQuestion created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: PlayedQuestionStore - apiCreate(playedQuestion)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuestionStore - apiCreate(playedQuestion) | ", error);
          return false;
        })
        return true;
    },
    apiCreateMany(playedQuestions: IPlayedQuestion[]): boolean {
      PlayedQuestionApi.createMany(playedQuestions)
      .then((response) => {
        if (response.status == 200) {
          this.addManyPlayedQuestions(response.data);
          console.log('%c Success! PlayedQuestions created and added to store.', 'color: green;');
          return true;
        }
        else {
          console.warn("Warning: not accepted by API | Location: PlayedQuestionStore - apiCreateMany(playedQuestions)");
          return false;
        }
      })
      .catch((error) => {
        console.error("Error: API connection | Location: PlayedQuestionStore - apiCreateMany(playedQuestions) | ", error);
        return false;
      })
      return true;
    },
    apiUpdate(playedQuestion: IPlayedQuestion): boolean {
      PlayedQuestionApi.update(playedQuestion)
        .then((response) => {
          if (response.data == true) {
            this.updatePlayedQuestion(playedQuestion);
            console.log('%c Success! PlayedQuestion updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: PlayedQuestionStore - apiUpdate(playedQuestion)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuestionStore - apiUpdate(playedQuestion) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(playedQuestionId: number): boolean {
      PlayedQuestionApi.delete(playedQuestionId)
        .then((response) => {
          if (response.data == true) {
            this.deletePlayedQuestion(playedQuestionId);
            console.log('%c Success! PlayedQuestion deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: PlayedQuestionStore - apiDelete(playedQuestionId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuestionStore - apiDelete(playedQuestionId) | ", error);
          return false;
        })
        return true;
    }

  }
})
