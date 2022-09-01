import { defineStore, mapStores } from 'pinia'
import type { IAchievement } from '@/models/'
import { AchievementApi } from "@/api/";
import { useAuthStore } from '../AuthStore';

export const useAchievementStore = defineStore({
  id: 'achievement',
  state: () => ({
    currentAchievement: {} as IAchievement,
    achievements: [] as IAchievement[]
  }),
  getters: {
    getCurrentAchievement: (state) => state.currentAchievement,
    getAchievements: (state) => state.achievements,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentAchievement(achievement: IAchievement): void {
      this.currentAchievement = achievement;
    },
    setAchievements(achievements: IAchievement[]): void {
      this.achievements = achievements
    },
    getAchievementsOfUser(userId: number): IAchievement[] {
      return this.achievements.filter(achiev => achiev.userId == userId);
    },
    getAchievementsOfTheme(themeId: number): IAchievement[] {
      return this.achievements.filter(achiev => achiev.themeId == themeId);
    },
    getThemeTopUserId(themeId: number): number {
      const themeAchiev = this.getAchievementsOfTheme(themeId);
      let tempAchiev = 0;
      let topUser = 0;
      themeAchiev.forEach(achiev => {
        if (tempAchiev < achiev.gainedPoints) {
          tempAchiev = achiev.gainedPoints;
          topUser = achiev.userId;
        }
      });
      return topUser;
    },
    getGoldTrophies(): number {
      let trofeus = useAchievementStore().getAchievementsOfUser(useAuthStore().user.userId)
      let contagem = 0;
      trofeus.forEach((trofeu) => {
        if (trofeu.gainedPoints >= 500) {  /* 8 agora mas mudar quando ajustarmos a pontuação necessaria pra ouro */
          contagem++
        }
      })
      return contagem;
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addAchievement(achievement: IAchievement): void {
      this.achievements.push(achievement);
      this.currentAchievement = achievement;
    },
    updateAchievement(achievement: IAchievement): void {
      const index = this.achievements.findIndex((u) => u.themeId == achievement.themeId && u.userId == achievement.userId);
      this.achievements.splice(index, 1, achievement);
      this.currentAchievement = achievement;
    },
    deleteAchievement(themeId: number, userId: number): void {
      const index = this.achievements.findIndex((u) => u.themeId == themeId && u.userId == userId);
      this.achievements.splice(index, 1);
    },
    clearStore() {
      this.achievements = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IAchievement[] {
      AchievementApi.getAll()
        .then((response) => {
          this.achievements = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AchievementStore - apiGetAll() | ", error);
        })
      return this.achievements;
    },
    apiGetById(themeId: number, userId: number ): IAchievement {
      AchievementApi.getById(themeId, userId)
        .then((response) => {
          this.currentAchievement = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AchievementStore - apiGetById(achievementId) | ", error);
        })
        return this.currentAchievement;
    },
    apiCreate(achievement: IAchievement): boolean {
      AchievementApi.create(achievement)
        .then((response) => {
          if (response.status == 200) {
            this.addAchievement(response.data);
            console.log('%c Success! Achievement created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: AchievementStore - apiCreate(achievement)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AchievementStore - apiCreate(achievement) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(achievement: IAchievement): boolean {
      AchievementApi.update(achievement)
        .then((response) => {
          if (response.data == true) {
            this.updateAchievement(achievement);
            console.log('%c Success! Achievement updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: AchievementStore - apiUpdate(achievement)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AchievementStore - apiUpdate(achievement) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(themeId: number, userId: number): boolean {
      AchievementApi.delete(themeId, userId)
        .then((response) => {
          if (response.data == true) {
            this.deleteAchievement(themeId, userId);
            console.log('%c Success! Achievement deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: AchievementStore - apiDelete(achievementId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: AchievementStore - apiDelete(achievementId) | ", error);
          return false;
        })
        return true;
    },
    apiGetAllAchievementsUser(userId: number): IAchievement[]{
      AchievementApi.getAllAchievementsUser(userId)
      .then((response) => {
        this.achievements = [...this.achievements , ...response.data];
      })
      .catch((error) => {
        console.error("Error: API connection | Location: AchievementStore - apiGetAllAchievementsUser() | ", error);
      })
    return this.achievements;
    }

  }
})

const levelPointsInterval = 10000;



