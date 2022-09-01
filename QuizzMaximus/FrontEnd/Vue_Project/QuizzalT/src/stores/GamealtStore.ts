import { defineStore } from 'pinia';
import { type IAnswer, type IQuestion, QuizzDict } from "@/models";
import { useQuizzStore, useThemeStore } from "@/stores";

/**
 * Store that manages the QuizzalT game. Outputs data directly to view.
 */
export const useGamealtStore = defineStore("gamealt", {
  state: () => ({
    quizzDict: {} as QuizzDict,

    currentQuestion: {} as IQuestion,
    currentQuestionIndex: {} as number,
    currentAnswers: [] as IAnswer[],

    gameTimedOngoing: false,
    gameNormalOngoing: false,
    displayScore: false,

  }),
  getters: {
    getQuizzDict:                (state) => state.quizzDict,
    getQuizz:                    (state) => state.quizzDict.quizz,
    
    getNumRightAnswers:          (state) => state.quizzDict.countRightAnswers(),
    getNumWrongAnswers:          (state) => state.quizzDict.countWrongAnswers(),
    getNumNotAnswered:           (state) => state.quizzDict.length - (state.quizzDict.countRightAnswers() + state.quizzDict.countWrongAnswers()),
    getPointsObtained:           (state) => state.quizzDict.countPointsObtained(),
    getPointsWronged:            (state) => state.quizzDict.countPointsWronged(),
    getPointsNotAnswered:        (state) => state.quizzDict.totalPoints - (state.quizzDict.countPointsObtained() + state.quizzDict.countPointsWronged()),
    
    getPercentRightAnswers:      (state) => (Math.round((state.quizzDict.countRightAnswers() / state.quizzDict.length) * 100)),
    getPercentWrongedAnswers:    (state) => (Math.round((state.quizzDict.countWrongAnswers() / state.quizzDict.length) * 100)),
    getPercentNotAnswered:       (state) => (Math.round(((state.quizzDict.length - (state.quizzDict.countRightAnswers() + state.quizzDict.countWrongAnswers())) / state.quizzDict.length) * 100)),
    getPercentPointsObtained:    (state) => (Math.round((state.quizzDict.countPointsObtained() / state.quizzDict.totalPoints) * 100)),
    getPercentPointsWronged:     (state) => (Math.round((state.quizzDict.countPointsWronged() / state.quizzDict.totalPoints) * 100)),
    getPercentPointsNotAnswered: (state) => (Math.round(((state.quizzDict.totalPoints - (state.quizzDict.countPointsObtained() + state.quizzDict.countPointsWronged())) / state.quizzDict.totalPoints) * 100))
  },
  actions: {
    // MAIN METHODS
    playRandomQuizz(mode: string): void {
      this.quizzDict = new QuizzDict(useQuizzStore().getRandomQuizz());
      if(mode == 'timed'){
        this.startTimedCurrentQuizz();
      } else {
        this.startNormalCurrentQuizz();
      }
    },
    playChosenQuizz(quizzId: number, mode: string): void {
      this.quizzDict = new QuizzDict(useQuizzStore().getQuizzById(quizzId));
      if(mode == 'timed'){
        this.startTimedCurrentQuizz();
      } else {
        this.startNormalCurrentQuizz();
      }
    },
    startTimedCurrentQuizz(): void {
      this.setCurrent(0);
      this.gameTimedOngoing = true;
    },
    startNormalCurrentQuizz(): void {
      this.setCurrent(0);
      this.gameNormalOngoing = true;
    },
    submitGameTimed(): void {
      if(this.quizzDict.submitPlayedGame()) {
        this.gameTimedOngoing = false;
        this.displayScore = true;
      }
      else {
        this.gameTimedOngoing = false;
      }
    },
    submitGameNormal(): void {
      if(this.quizzDict.submitPlayedGame()) {
        this.gameNormalOngoing = false;
        this.displayScore = true;
      }
      else {
        this.gameNormalOngoing = false;
      }
    },
    // Question set and nav
    /**
    * Sets all the current data for display (question key + question + 4 answers)
    */
    setCurrent(key: number): void {
      this.currentQuestionIndex = key;
      this.currentQuestion = this.quizzDict.getQuestionByKey(this.currentQuestionIndex);
      this.currentAnswers = this.quizzDict.getQuestionAnswersByKey(this.currentQuestionIndex);
    },
    nextQuestion(): void {
      this.setCurrent(this.currentQuestionIndex == (this.quizzDict.length - 1) ? 0 : ++this.currentQuestionIndex);
    },
    prevQuestion(): void {
      this.setCurrent(this.currentQuestionIndex == 0 ? (this.quizzDict.length - 1) : --this.currentQuestionIndex)
    },
    getCurrentQuestionTheme(){
      return useThemeStore().getThemeName(this.currentQuestion.themeId);
    },
    // TOGGLES
    /**
    * Toggles the whole game on and off. Used to end the game.
    */
    toggleTimedGameOngoing(): void {
      this.gameTimedOngoing = this.gameTimedOngoing ? false : true;
    },
    toggleNormalGameOngoing(): void {
      this.gameNormalOngoing = this.gameNormalOngoing ? false : true;
    },
    /**
    * Toggles scores view on and off. Used on start/end game.
    */
    toggleGameScores(): void {
      this.displayScore = this.displayScore ? false : true;
    },
    // "dynamic CSS"
    /**
     * For View.vue use in :style "display". Retuns "yellow" if answer was chosen, "green" if not. 
     */
    cssAnswerColorByChoice(answer: IAnswer): string {
      return this.quizzDict.checkIfItIsThisQuestionChosenAnswer(answer) ? "#039974" : "green";
    }
  }
})
