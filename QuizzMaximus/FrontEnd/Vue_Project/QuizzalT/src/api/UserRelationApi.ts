import type { UserRelation } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/UserRelation/GetAll`);
    },
    getById(userId: number, relatedUserId: number){
        return axios.get(`${this.baseUrl}/UserRelation/GetById/${userId},${relatedUserId}`);
    },
    create(userRelation: UserRelation){
        return axios.post(`${this.baseUrl}/UserRelation/Create`, userRelation);
    },
    update(userRelation: UserRelation){
        return axios.put(`${this.baseUrl}/UserRelation/Update`, userRelation);
    },
    delete(userId: number, relatedUserId: number){
        return axios.delete(`${this.baseUrl}/UserRelation/Delete/${userId},${relatedUserId}`);
    },
    getAllRelationsUser(userId : number){
        return axios.get(`${this.baseUrl}/UserRelation/GetAllRelationsUser/${userId}`)
    }
}