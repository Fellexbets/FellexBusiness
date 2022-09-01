export interface IAchievement {
    "themeId": number;
    "userId": number;
    "gainedPoints": number;
}

export class Achievement {
    constructor(data?: IAchievement){
        Object.assign(this, data);
    }
}

