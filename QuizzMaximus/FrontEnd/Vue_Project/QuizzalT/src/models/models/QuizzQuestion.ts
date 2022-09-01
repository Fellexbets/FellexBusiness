export interface IQuizzQuestion {
    "quizzId": number;
    "questionId": number;
}

export class QuizzQuestion {
    constructor(data?: IQuizzQuestion){
        Object.assign(this, data);
    }
}