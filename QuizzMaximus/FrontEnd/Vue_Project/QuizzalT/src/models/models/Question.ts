export interface IQuestion {
    "questionId": number;
    "userId": number;
    "themeId": number;
    "questionName": string;
    "questionText": string;
    "difficulty": number;
    "status": string;
    "questionImage": [];
    "questionImageString": string,
    "dateCreated"?: Date;
    "dateEdited"?: Date;
}

export default class Question {
    constructor(data?: IQuestion){
        Object.assign(this, data);
    }
}