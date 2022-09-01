import type { IAnswer, IQuestion } from "@/models/";

/**
 * Contains one question, four possible answers, stores the chosen answer, and has a key for navigation between GameQuestions during quizz play.
 */
export class GameQuestion {
    /**
     * Can be used as an access value (number) for GameQuestions inside QuizzDict. Also conveys the position of the respective GameQuestion inside QuizzDict. 
     */
    "key": number;
    "question": IQuestion;
    "answers": IAnswer[];
    "chosenAnswer"?: IAnswer;

    constructor(question: IQuestion, answers: IAnswer[]){
        this.key = 0; //never undifined
        this.question = question;
        this.answers = answers.sort(() => { return 0.5 - Math.random() });
    }

    /**
     * Checks if the question was correctly answered.
     * @returns True if answer is correct. False if its undefined or incorrect.
     */
    gotItRight(): boolean {
        return this.chosenAnswer?.rightAnswer == true ? true : false;
    }
    gotItRightDingbat(): string {
        return this.gotItRight() ? '\u{2705}' : '\u{274C}';
    }
    /**
    * Returns the right answer for this question. Used to display final scores.
    * @returns The right answer for this question.
    */
    getRightAnswer(): IAnswer {
        return this.answers.find(a => a.rightAnswer == true) as IAnswer
    }
}











