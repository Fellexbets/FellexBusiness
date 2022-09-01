import { defineStore } from 'pinia'
import { useAchievementStore, useAnswerStore, usePlayedQuestionStore, usePlayedQuizzStore, useQuestionStore, useQuizzStore, useQuizzQuestionStore, useRelationStore, useThemeStore, useUserStore, useUserRelationStore } from "@/stores";
import type { IAchievement, IAnswer, IPlayedQuestion, IPlayedQuizz, IQuestion, IQuizz, IQuizzQuestion, IRelation, ITheme, IUser, IUserRelation } from '@/models';

/**
 * Store that combines the data of other stores to give filtered information.
 */
export const useCombFilterStore = defineStore({
  id: 'combFilter',
  actions: {
    // QUIZZ related - in situations where only quizzId is available
    /**
     * Calculate the average difficulty of the questions of the given quizz.
     * @returns The average difficulty (number) rounded to the nearest hundredth.
     */
    getQuizzDifficultyAverage(quizzId: number): number {
      const quizzQuestionsIds = useQuizzQuestionStore().getQuestionIdsOfQuizzId(quizzId);
      const questions = useQuestionStore().getQuestionsByIds(quizzQuestionsIds);
      let difficultySum = 0;
      questions.forEach(q => difficultySum += q.difficulty);
      return Math.round((difficultySum / questions.length) * 100) / 100;
    },
    /**
     * Find the theme of the given quizz. If there is more than one theme, returns "Misc".
     * @returns The theme of the quizz. "Misc" if there is more than one.
     */
    getQuizzTheme(quizzId: number): string {
      const quizzQuestionsIds = useQuizzQuestionStore().getQuestionIdsOfQuizzId(quizzId);
      const questions : IQuestion[] = useQuestionStore().getQuestionsByIds(quizzQuestionsIds);
      let quizzTheme = questions[0].themeId;
      questions.forEach(q => q.themeId != quizzTheme ? quizzTheme = -1 : "")
      return quizzTheme == -1 ? "Misc" : useThemeStore().getThemeName(quizzTheme);
    },
    /**
     * Sum all the points of the questions of the given quizz.
     * @returns The sum of all the points (number).
     */
    getQuizzTotalPoints(quizzId: number): number {
      const quizzQuestionsIds = useQuizzQuestionStore().getQuestionIdsOfQuizzId(quizzId);
      const questions = useQuestionStore().getQuestionsByIds(quizzQuestionsIds);
      let totalPoints = 0;
      questions.forEach(q => totalPoints += (q.difficulty * 10));
      return totalPoints;
    },
  }
})



