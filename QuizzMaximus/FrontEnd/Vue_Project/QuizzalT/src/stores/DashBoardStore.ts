import { defineStore } from 'pinia';
import { UsersListed, type IAchievement, type ITheme, type IUser } from '@/models';
import { useAchievementStore, useUserStore, useThemeStore } from '@/stores/';

/**
 * Dashboard manager.
 */
export const useDashboardStore = defineStore({
  id: 'dashboard',
  state: () => ({
    usersListed: {} as UsersListed,

    pointsInterval: 1000,
  }),
  getters: {
    getUsersListed:    (state) => state.usersListed as UsersListed,
    
    getThemes:         (state) => useThemeStore().getThemes as ITheme[],
    getThemesNotEmpty: (state) => state.usersListed.getThemesWithAchievements() as ITheme[],

    getAchievements:   (state) => useAchievementStore().getAchievements as IAchievement[],
    getUsers:          (state) => useUserStore().getUsers as IUser[],
    },
  actions: {
    startStore(users: IUser[]): void {
      this.usersListed = new UsersListed(users);  
    },

  }
})


