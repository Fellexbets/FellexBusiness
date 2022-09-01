import type { Quizz } from "@/models";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/Quizz/GetAll`);
    },
    getById(quizzId: number){
        return axios.get(`${this.baseUrl}/Quizz/GetById/${quizzId}`);
    },
    create(quizz: Quizz){
        return axios.post(`${this.baseUrl}/Quizz/Create`, quizz);
    },
    update(quizz: Quizz){
        return axios.put(`${this.baseUrl}/Quizz/Update`, quizz);
    },
    delete(quizzId: number){
        return axios.delete(`${this.baseUrl}/Quizz/Delete/${quizzId}`);
    },
    getAllQuizzesUser(userId: number){
        return axios.get(`${this.baseUrl}/Quizz/GetAllQuizzesUser/${userId}`)
    },
    getAllQuizzesAdmin(){
        return axios.get(`${this.baseUrl}/Quizz/GetAllQuizzesAdmin`)
    }
}