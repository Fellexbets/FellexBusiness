import { defineStore } from 'pinia'
import type { IPlayedQuizz } from '@/models/'
import { PlayedQuizzApi } from "@/api/";

export const usePlayedQuizzStore = defineStore({
  id: 'playedQuizz',
  state: () => ({
    currentPlayedQuizz: {} as IPlayedQuizz,
    playedQuizzes: [] as IPlayedQuizz[]
  }),
  getters: {
    getCurrentPlayedQuizz: (state) => state.currentPlayedQuizz,
    getPlayedQuizzes: (state) => state.playedQuizzes,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentPlayedQuizz(playedQuizz: IPlayedQuizz): void {
      this.currentPlayedQuizz = playedQuizz;
    },
    setPlayedQuizzes(playedQuizzes: IPlayedQuizz[]): void {
      this.playedQuizzes = playedQuizzes
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addPlayedQuizz(playedQuizz: IPlayedQuizz): void {
      this.playedQuizzes.push(playedQuizz);
      this.currentPlayedQuizz = playedQuizz;
    },
    updatePlayedQuizz(playedQuizz: IPlayedQuizz): void {
      const index = this.playedQuizzes.findIndex((u) => u.playedQuizzId == playedQuizz.playedQuizzId);
      this.playedQuizzes.splice(index, 1, playedQuizz);
      this.currentPlayedQuizz = playedQuizz;
    },
    deletePlayedQuizz(playedQuizzId: number): void {
      const index = this.playedQuizzes.findIndex((u) => u.playedQuizzId == playedQuizzId);
      this.playedQuizzes.splice(index, 1);
    },
    clearStore() {
      this.playedQuizzes = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IPlayedQuizz[] {
      PlayedQuizzApi.getAll()
        .then((response) => {
          this.playedQuizzes = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuizzStore - apiGetAll() | ", error);
        })
      return this.playedQuizzes;
    },
    apiGetById(playedQuizzId: number): IPlayedQuizz {
      PlayedQuizzApi.getById(playedQuizzId)
        .then((response) => {
          this.currentPlayedQuizz = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuizzStore - apiGetById(playedQuizzId) | ", error);
        })
        return this.currentPlayedQuizz;
    },
    apiCreate(playedQuizz: IPlayedQuizz): boolean {
      PlayedQuizzApi.create(playedQuizz)
        .then((response) => {
          if (response.status == 200) {
            this.addPlayedQuizz(response.data);
            console.log('%c Success! PlayedQuizz created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: PlayedQuizzStore - apiCreate(playedQuizz)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuizzStore - apiCreate(playedQuizz) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(playedQuizz: IPlayedQuizz): boolean {
      PlayedQuizzApi.update(playedQuizz)
        .then((response) => {
          if (response.data == true) {
            this.updatePlayedQuizz(playedQuizz);
            console.log('%c Success! PlayedQuizz updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: PlayedQuizzStore - apiUpdate(playedQuizz)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuizzStore - apiUpdate(playedQuizz) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(playedQuizzId: number): boolean {
      PlayedQuizzApi.delete(playedQuizzId)
        .then((response) => {
          if (response.data == true) {
            this.deletePlayedQuizz(playedQuizzId);
            console.log('%c Success! PlayedQuizz deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: PlayedQuizzStore - apiDelete(playedQuizzId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: PlayedQuizzStore - apiDelete(playedQuizzId) | ", error);
          return false;
        })
        return true;
    }

  }
})
