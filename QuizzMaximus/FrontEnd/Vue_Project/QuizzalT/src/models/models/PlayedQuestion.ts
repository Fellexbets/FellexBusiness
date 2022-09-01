export interface IPlayedQuestion {
    "playedQuestionId": number;
    "userId": number;
    "questionId": number;
    "gotItRight": boolean;
    "points": number;
    "dateAnswered"?: Date;
}

export class PlayedQuestion {
    constructor(data?: IPlayedQuestion){
        Object.assign(this, data);
    }
}