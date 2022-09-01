export interface IUser {
    "userId": number;
    "username": string;
    "email": string;
    "role": string;
    "dateCreated"?: Date;
    "password": string;
    "photoString": string;
    "photo": [];
}

export class User {
    constructor(data?: IUser) {
        Object.assign(this, data);
    }
}

