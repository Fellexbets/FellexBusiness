export interface IPlayedQuizz {
    "playedQuizzId": number;
    "userId": number;
    "quizzId": number;
    "totalPoints": number;
    "dateAnswered"?: Date;
}

export class PlayedQuizz {
    constructor(data?: IPlayedQuizz){
        Object.assign(this, data);
    }
}