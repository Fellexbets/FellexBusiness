import type { Answer } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/Answer/GetAll`);
    },
    getById(answerId: number){
        return axios.get(`${this.baseUrl}/Answer/GetById/${answerId}`);
    },
    create(answer: Answer){
        return axios.post(`${this.baseUrl}/Answer/Create`, answer);
    },
    update(answer: Answer){
        return axios.put(`${this.baseUrl}/Answer/Update`, answer);
    },
    delete(answerId: number){
        return axios.delete(`${this.baseUrl}/Answer/Delete/${answerId}`);
    },
    createAnswers(answers : Answer[]){
        return axios.post(`${this.baseUrl}/Answer/CreateAnswers`, answers);
    },
    updateAnswers(answers : Answer[]){
        return axios.put(`${this.baseUrl}/Answer/UpdateAnswers`, answers);
    },
    getAllAnswersUser(userId: number){
        return axios.get(`${this.baseUrl}/Answer/GetAnswersByUser/${userId}`);
    },
    getAllAnswersBase(){
        return axios.get(`${this.baseUrl}/Answer/GetAllAnswersAdmin`);
    }
}