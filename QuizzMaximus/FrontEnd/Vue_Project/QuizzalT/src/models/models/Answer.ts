export interface IAnswer {
    "answerId": number;
    "questionId": number;
    "answerText": string;
    "rightAnswer": boolean;
    "dateCreated"?: Date;
    "dateEdited"?: Date;
}

export class Answer {
    constructor(data?: IAnswer){
        Object.assign(this, data);
    }
}