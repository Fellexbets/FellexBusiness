import type { Question } from "@/models";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/Question/GetAll`);
    },
    getById(questionId: number){
        return axios.get(`${this.baseUrl}/Question/GetById/${questionId}`);
    },
    create(question: Question){
        return axios.post(`${this.baseUrl}/Question/Create`, question);
    },
    update(question: Question){
        return axios.put(`${this.baseUrl}/Question/Update`, question);
    },
    updateQuestion(question: Question){
        return axios.put(`${this.baseUrl}/Question/UpdateQuestion`, question);
    },
    delete(questionId: number){
        return axios.delete(`${this.baseUrl}/Question/Delete/${questionId}`);
    },
    getAllQuestionsUser(userId: number){
        return axios.get(`${this.baseUrl}/Question/GetAllQuestionsUser/${userId}`)
    },
    getAllQuestionsBase(){
        return axios.get(`${this.baseUrl}/Question/GetAllQuestionsAdmin`)
    }
}