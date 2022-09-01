export interface IUserRelation {
    "userId": number;
    "relatedUserId": number;
    "relationId": number;
    "dateCreated"?: Date;
}

export class UserRelation {
    constructor(data?: IUserRelation){
        Object.assign(this, data);
    }
}