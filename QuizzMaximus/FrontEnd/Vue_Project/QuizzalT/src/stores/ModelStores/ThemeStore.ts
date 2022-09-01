import { defineStore } from 'pinia'
import type { ITheme } from '@/models/'
import { ThemeApi } from "@/api/";

export const useThemeStore = defineStore({
  id: 'theme',
  state: () => ({
    currentTheme: {} as ITheme,
    themes: [] as ITheme[]
  }),
  getters: {
    getCurrentTheme: (state) => state.currentTheme,
    getThemes: (state) => state.themes,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentTheme(theme: ITheme): void {
      this.currentTheme = theme;
    },
    setThemes(themes: ITheme[]): void {
      this.themes = themes
    },
    getThemeName(themeId: number): string {
      return this.themes.find(t => t.themeId == themeId)?.themeName as string;
    },
    getThemeId(themeName : string) : number{
      return this.themes.find(t => t.themeName == themeName)?.themeId as number;
    },
    getThemeNames() : string[]{
      let themeNames : string[] = [];
      this.themes.forEach(theme => {
        themeNames.push(theme.themeName);
      })
      return themeNames;
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addTheme(theme: ITheme): void {
      this.themes.push(theme);
      this.currentTheme = theme;
    },
    updateTheme(theme: ITheme): void {
      const index = this.themes.findIndex((u) => u.themeId == theme.themeId);
      this.themes.splice(index, 1, theme);
      this.currentTheme = theme;
    },
    deleteTheme(themeId: number): void {
      const index = this.themes.findIndex((u) => u.themeId == themeId);
      this.themes.splice(index, 1);
    },
    clearStore() {
      this.themes = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): ITheme[] {
      ThemeApi.getAll()
        .then((response) => {
          this.themes = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: ThemeStore - apiGetAll() | ", error);
        })
      return this.themes;
    },
    apiGetById(themeId: number): ITheme {
      ThemeApi.getById(themeId)
        .then((response) => {
          this.currentTheme = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: ThemeStore - apiGetById(themeId) | ", error);
        })
      return this.currentTheme;
    },
    apiCreate(theme: ITheme): boolean {
      ThemeApi.create(theme)
        .then((response) => {
          if (response.status == 200) {
            this.addTheme(response.data);
            console.log('%c Success! Theme created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: ThemeStore - apiCreate(theme)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: ThemeStore - apiCreate(theme) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(theme: ITheme): boolean {
      ThemeApi.update(theme)
        .then((response) => {
          if (response.data == true) {
            this.updateTheme(theme);
            console.log('%c Success! Theme updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: ThemeStore - apiUpdate(theme)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: ThemeStore - apiUpdate(theme) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(themeId: number): boolean {
      ThemeApi.delete(themeId)
        .then((response) => {
          if (response.data == true) {
            this.deleteTheme(themeId);
            console.log('%c Success! Theme deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: ThemeStore - apiDelete(themeId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: ThemeStore - apiDelete(themeId) | ", error);
          return false;
        })
        return true;
    }

  }
})
