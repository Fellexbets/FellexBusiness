import type { IQuizzQuestion, QuizzQuestion } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/QuizzQuestion/GetAll`);
    },
    getById(quizzId: number, questionId: number){
        return axios.get(`${this.baseUrl}/QuizzQuestion/GetById/${quizzId},${questionId}`);
    },
    create(quizzQuestion: QuizzQuestion){
        return axios.post(`${this.baseUrl}/QuizzQuestion/Create`, quizzQuestion);
    },
    update(quizzQuestion: QuizzQuestion){
        return axios.put(`${this.baseUrl}/QuizzQuestion/Update`, quizzQuestion);
    },
    delete(quizzId: number, questionId: number){
        return axios.delete(`${this.baseUrl}/QuizzQuestion/Delete/${quizzId},${questionId}`);
    },
    createQuizzQuestions(quizzQuestions : IQuizzQuestion[]){
        return axios.post(`${this.baseUrl}/QuizzQuestion/CreateQuizzQuestions`, quizzQuestions);
    },
    getAllQuizzQuestionsUser(userId : number){
        return axios.get(`${this.baseUrl}/QuizzQuestion/GetAllQuizzQuestionsUser/${userId}`);
    },
    getAllQuizzQuestionsAdmin(){
        return axios.get(`${this.baseUrl}/QuizzQuestion/GetAllQuizzQuestionsAdmin`);
    }
}