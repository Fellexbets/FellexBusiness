import { useAchievementStore, useAnswerStore, useAuthStore, useQuestionStore, useQuizzQuestionStore, usePlayedQuestionStore, usePlayedQuizzStore } from "@/stores";
import type { IAchievement, IAnswer, IPlayedQuestion, IPlayedQuizz, IQuestion, IQuizz } from "@/models/";
import { GameQuestion } from "@/models/";

/**
* Stores all the information during a game of quizz. Stores several GameQuestions, each containing a question with four answers.
*/
export class QuizzDict {
  "quizz": IQuizz;
  /**
  * Each GameQuestion stores a question with four answers with a key. Also stores user chosen answer.
  */
  "gameQuestions": GameQuestion[];
  /**
  * Total number of questions of this quizz.
  */
  "length": number;
  /**
  * Total number of points that can be gained with this quizz.
  */
  "totalPoints": number;
  /**
  * The average difficulty of this quizz game.
  */
  "difficultyAvg": number;

  constructor(quizz: IQuizz) {
    this.quizz = quizz;
    this.gameQuestions = this.sortQuestionsAndGiveKey(
      this.populateGameQuestions(
        useQuizzQuestionStore().getQuestionIdsOfQuizzId(this.quizz.quizzId)));  //go get the questionIds, populate it and sort the questions
    this.totalPoints = this.setTotalPoints();
    this.length = this.gameQuestions.length;
    this.difficultyAvg = this.setDifficultyAvg();
  }

  // QUIZZ
  /**
  * If any answer was chosen, submits the played Quizz and Questions to the Db as PlayedQuizz and PlayedQuestions and sets the Achievements obtained.
  * @returns If any answer was chosen, the game is submited and returns true. False if no answer was chosen.
  */
  submitPlayedGame(): boolean {
    if (this.checkIfThereIsAnyQuestionAnswered()) {
      usePlayedQuestionStore().apiCreateMany(this.createQuizzPlayedQuestions());
      usePlayedQuizzStore().apiCreate(this.newPlayedQuizz());
      this.updateAchievementsForEndGame();
      return true;
    }
    return false;
  }

  // QUESTIONS
  /**
  * Returns a GameQuestions question with its key.
  */
  getQuestionByKey(key: number): IQuestion {
    return this.gameQuestions.find(element => element.key == key)?.question as IQuestion;
  }
  /**
  * Returns a GameQuestions question answers with its key.
  */
  getQuestionAnswersByKey(key: number): IAnswer[] {
    return this.gameQuestions.find(element => element.key == key)?.answers as IAnswer[];
  }

  // ANSWERS
  /**
  * Inserts and overrides the given answer into the proper gameQuestion.
  */
  insertChosenAnswer(answer: IAnswer): void {
    this.gameQuestions.forEach(gQ => gQ.question.questionId == answer.questionId ? gQ.chosenAnswer = answer : "");
  }
  /**
  * Verifies if the user answered any question. Used on game submit. 
  * @returns True if any question was answered, false if not.
  */
  checkIfThereIsAnyQuestionAnswered(): boolean {
    let check = false;
    this.gameQuestions.forEach(gQ => gQ.chosenAnswer != undefined ? check = true : "")
    return check;
  }
  /**
  * Checks if this answer was the chosen answer for the question with same id. 
  * Used for css color change of answered question in view.
  * @returns True if it was chosen answer, false if not.
  */
  checkIfItIsThisQuestionChosenAnswer(answer: IAnswer): boolean {
    return this.gameQuestions.find(gQ => gQ.question.questionId == answer.questionId)?.chosenAnswer?.answerId == answer.answerId;
  }
  /**
  * Checks if the question with the given key was answered or not.
  * @returns True if was answered, false if not.
  */
  checkByKeyIfQuestionWasAnswered(key: number): boolean {
    return this.gameQuestions.find(gQ => gQ.key == key)?.chosenAnswer != undefined ? true : false;
  }
  /**
   * Checks if the question with the given Id was answered or not.
   * @returns True if was answered, false if not.
   */
  checkByIdIfQuestionWasAnswered(questionId: number): boolean {
    return this.gameQuestions.find(gQ => gQ.question.questionId == questionId)?.chosenAnswer != undefined ? true : false;
  }
  /**
  * Counts how many questions were answered.
  * @returns Number of answered questions.
  */
  countAnsweredQuestions(): number {
    let answeredQuestions = 0;
    this.gameQuestions.forEach(gQ => gQ.chosenAnswer != undefined ? answeredQuestions++ : 0);
    return answeredQuestions;
  }
  /**
  * Counts how many questions were correctly answered.
  * @returns Number of correct answers.
  */
  countRightAnswers(): number {
    let rightAnswersCounter = 0;
    this.gameQuestions.forEach(gQ => gQ.gotItRight() ? rightAnswersCounter++ : "");
    return rightAnswersCounter;
  }
  /**
  * Counts how many questions were incorrectly answered.
  * @returns Number of incorrect answers.
  */
  countWrongAnswers(): number {
    let wrongAnswersCounter = 0;
    this.gameQuestions.forEach(gQ => gQ.chosenAnswer != undefined && !gQ.gotItRight() ? wrongAnswersCounter++ : "");
    return wrongAnswersCounter;
  }
  /**
  * Counts how many points were achieved with right answers.
  * @returns Total number of points gained.
  */
  countPointsObtained(): number {
    let pointsObtained = 0;
    this.gameQuestions.forEach(gQ => gQ.gotItRight() ? pointsObtained += (gQ.question.difficulty * 10) : "");
    return pointsObtained;
  }
  /**
  * Counts how many points were lost to wrong answers.
  * @returns Total number of points lost to wrong answers.
  */
  countPointsWronged(): number {
    let pointsWronged = 0;
    this.gameQuestions.forEach(gQ => gQ.chosenAnswer != undefined && !gQ.gotItRight() ? pointsWronged += (gQ.question.difficulty * 10) : "");
    return pointsWronged;
  }
  /**
  * Erases all chosen answers by the user.
  */
  clearAllChosenAnswers(): void {
    this.gameQuestions.forEach(gQ => gQ.chosenAnswer = undefined);
  }

  // INNER WORKINGS ------------------
  // QUIZZDICT CONSTRUCTOR
  /**
   * Used in setQuizz: creates a set of GameQuestions and returns them.
   * @param questionsIds The questionIds used to populate each GameQuestion.
   * @returns An array of GameQuestion to populate the quizz.
   */
  private populateGameQuestions(questionsIds: number[]) {
    let gameQuestions = [] as GameQuestion[];  // clears gamequestions, may be used as a reset
    for (let questionIndex = 0; questionIndex < questionsIds.length; questionIndex++) {   // and populate
      let thisQuestion = useQuestionStore().getQuestionById(questionsIds[questionIndex]);
      let thisAnswers = useAnswerStore().getFourFinalAnswersOfQuestion(thisQuestion.questionId);
      gameQuestions.push(new GameQuestion(thisQuestion, thisAnswers)); //key is given later after sort
    }
    return gameQuestions;
  }
  /**
   * Used in setQuizz: sorts given GameQuestions and attributes each a key.
   * @returns An array of sorted GameQuestions with key.
   */
  private sortQuestionsAndGiveKey(gameQuestions: GameQuestion[]) {
    gameQuestions.sort(function () { return 0.5 - Math.random() });
    for (let questionIndex = 0; questionIndex < gameQuestions.length; questionIndex++) {
      gameQuestions[questionIndex].key = questionIndex;
    }
    return gameQuestions;
  }
  /**
   * Used in constructor: sets the total number of points that can be gained with this quizz.
   * @returns Returns the total number of points.
   */
  private setTotalPoints() {
    let totalPoints = 0;
    this.gameQuestions.forEach(qQ => totalPoints += (qQ.question.difficulty * 10));
    return totalPoints;
  }
  /**
  * Used in constructor: sets the average difficulty of the quizz game.
  * @returns Returns the average difficulty.
  */
  private setDifficultyAvg(): number {
    let difficultySum = 0;
    this.gameQuestions.forEach(gQ => difficultySum += gQ.question.difficulty)
    return difficultySum / this.length;
  }
  
  // on submit -> make PLAYEDQUESTION / PLAYEDQUIZZ
  /**
  * Creates and returns a new IPlayedQuestion with the given answer - user's chosen answer. 
  */
  private newPlayedQuestion(answer: IAnswer): IPlayedQuestion {
    let pointsMade = 0;
    answer.rightAnswer ? pointsMade = (this.gameQuestions.find(gQ => gQ.question.questionId == answer.questionId) as GameQuestion)?.question.difficulty * 10 : "";

    let playedQuestion: IPlayedQuestion = {
      playedQuestionId: 0,
      userId: useAuthStore().user.userId,
      questionId: answer.questionId,
      gotItRight: answer.rightAnswer,
      points: pointsMade
    }
    return playedQuestion;
  }
  /**
  * Creates and returns an array of IPlayedQuestions with the user's chosen answers for this Quizz.
  */
  private createQuizzPlayedQuestions(): IPlayedQuestion[] {
    const playedQuestions = [] as IPlayedQuestion[];
    this.gameQuestions.forEach(gQ => gQ.chosenAnswer != undefined ? playedQuestions.push(this.newPlayedQuestion(gQ.chosenAnswer)) : "");
    return playedQuestions;
  }
  /**
  * Creates and returns a new IPlayedQuizz with the user's chosen answers for this Quizz.
  */
  private newPlayedQuizz(): IPlayedQuizz {
    let playedQuizz: IPlayedQuizz = {
      playedQuizzId: 0,
      userId: useAuthStore().user.userId,
      quizzId: this.quizz.quizzId,
      totalPoints: this.countPointsObtained()
    }
    return playedQuizz;
  }
  
  // on submit -> update ACHIEVEMENTS
  /**
   * Makes a jagged array with the points and theme obtained per answered question. 
   * @returns A number[][] array: [[themeId, points], [themeId, points], ...]
   */
  private makePointsPerThemeArray(): number[][] {
    const themeDict = [[]] as number[][]; // [[themeId, points], [themeId, points], ...]
    this.gameQuestions.forEach(gQ => gQ.chosenAnswer?.rightAnswer == true ? themeDict.push([gQ.question.themeId, (gQ.question.difficulty * 10)]) : "");
    return themeDict; 
  }
  /**
   * Receives the PointsPerThemeArray and agregates the points per themeId.
   * @returns A refined number[][] array: [[themeId1, totalPoints], [themeId2, totalPoints], ...]
   */
  private refinePointsPerThemeArray(pointsPerTheme: number[][]): number[][] {
    const themeDictToRefine = [[]] as number[][];

    // loops through the pointsPerTheme[][] 
    for(let pointsPerThemeIndex = 1; pointsPerThemeIndex < pointsPerTheme.length; pointsPerThemeIndex++) {

      let thisThemeId = pointsPerTheme[pointsPerThemeIndex][0]; //for legibility
      let thisPoints = pointsPerTheme[pointsPerThemeIndex][1]; //for legibility

      // see if this themeId is already stored in themeDictToRefine
      let previousRefinedThemePtIndex = themeDictToRefine.findIndex(prvTh=> prvTh[0] == thisThemeId);

      // if already exists 
      if (previousRefinedThemePtIndex != -1 ) {
        //sum the points
        let sumedPoints = (thisPoints + themeDictToRefine[previousRefinedThemePtIndex][1]);
        // and update
        themeDictToRefine.splice(previousRefinedThemePtIndex, 1, [thisThemeId, sumedPoints]);
      } 
      // if not, insert it
      else {
        themeDictToRefine.push(pointsPerTheme[pointsPerThemeIndex])
      }
    }
    return themeDictToRefine
  }
  /**
   * On game submit, check the obtained points and update the user's achievements.
   */
  private updateAchievementsForEndGame(): void {
    const themeDictTemp = this.makePointsPerThemeArray();
    const themeDict = this.refinePointsPerThemeArray(themeDictTemp);

    for (let themeDictIndex = 1; themeDictIndex < themeDict.length; themeDictIndex++) {
      let thisTheme = themeDict[themeDictIndex][0]; //for legibility
      let thisPoints = themeDict[themeDictIndex][1]; //for legibility

      // find if the user already has any points in this theme
      let userAchiev = useAchievementStore().achievements.find(achiev => 
                           achiev.userId == useAuthStore().user.userId && achiev.themeId == thisTheme);

      // if yes
      if (userAchiev != undefined) {
        let userAchievUpdated: IAchievement = {
          themeId: thisTheme,
          userId: useAuthStore().user.userId,
          gainedPoints: (userAchiev.gainedPoints + thisPoints)
        };
        useAchievementStore().apiUpdate(userAchievUpdated);
      }
      // if not
      else {
        let thisAchievement: IAchievement = {
          themeId: thisTheme,
          userId: useAuthStore().user.userId,
          gainedPoints: thisPoints
        };
        useAchievementStore().apiCreate(thisAchievement);
      }


    }
  }

}











