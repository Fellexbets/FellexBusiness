export interface IRelation {
    "relationId": number;
    "relationName": string;
}

export class Relation {
    constructor(data?: IRelation){
        Object.assign(this, data);
    }
}