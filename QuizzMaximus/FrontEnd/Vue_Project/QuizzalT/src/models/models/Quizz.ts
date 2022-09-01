export interface IQuizz {
    "quizzId": number;
    "userId": number;
    "quizzName": string;
    "status": string;
    "dateCreated"?: Date;
    "dateEdited"?: Date;
}

export default class Quizz {
    constructor(data?: IQuizz){
        Object.assign(this, data);
    }
}