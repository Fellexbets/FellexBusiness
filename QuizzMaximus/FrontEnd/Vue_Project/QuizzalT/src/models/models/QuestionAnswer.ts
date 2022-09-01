import { QuestionApi, AnswerApi } from "@/api";
import type { IQuestion, IAnswer } from "@/models";
import { useAnswerStore, useQuestionStore } from "@/stores";

export class QuestionAnswer {
    "question": IQuestion;
    "answers": IAnswer[];

    constructor(question: IQuestion, answers : IAnswer[]){
        this.question = question;
        this.answers = answers;
    }
}

export function createQuestionAnswers(questionAnswer : QuestionAnswer){
  QuestionApi.create(questionAnswer.question)
      .then((response) => {
            if(response.status == 200){
                useQuestionStore().addQuestion(response.data);
                console.log('%c Success! Question created and added to store.', 'color: green;');
                questionAnswer.answers.forEach(answer => answer.questionId = response.data.questionId);
                AnswerApi.createAnswers(questionAnswer.answers)
                    .then((response) => {
                        if ((response.status) == 200) {
                            useAnswerStore().addSeveralAnswers(response.data);
                            console.log('%c Success! Answers created and added to store.', 'color: green;');
                        } else {
                          console.warn("Warning: not accepted by API | Location: QuestionAnswerModel - createAnswers(answer)");
                        }
                    })
            }
            else{
                console.warn("Warning: not accepted by API | Location: QuestionAnswerModel - apiCreate(question)");
            }
      })
      .catch((error) => {
        console.error("Error: API connection | Location: QuestionStore - apiCreate(question) | ", error);
      })
}

export function updateQuestionAnswers(questionAnswer : QuestionAnswer){
    QuestionApi.updateQuestion(questionAnswer.question)
        .then((response) => {
              if(response.status == 200){
                  useQuestionStore().updateQuestion(response.data);
                  console.log('%c Success! Question updated and added to store.', 'color: green;');
                  AnswerApi.updateAnswers(questionAnswer.answers)
                      .then((response) => {
                          if ((response.status) == 200) {
                              useAnswerStore().updateSeveralAnswers(questionAnswer.answers);
                              console.log('%c Success! Answers updated and added to store.', 'color: green;');
                          } else {
                            console.warn("Warning: not accepted by API | Location: QuestionAnswerModel - createAnswers(answer)");
                          }
                      })
              }
              else{
                  console.warn("Warning: not accepted by API | Location: QuestionAnswerModel - apiUpdate(Answers)");
              }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: QuestionAnswerModel - apiUpdate(question) | ", error);
        })
  }