import type { PlayedQuestion } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/PlayedQuestion/GetAll`);
    },
    getById(playedQuestionId: number){
        return axios.get(`${this.baseUrl}/PlayedQuestion/GetById/${playedQuestionId}`);
    },
    create(playedQuestion: PlayedQuestion){
        return axios.post(`${this.baseUrl}/PlayedQuestion/Create`, playedQuestion);
    },
    createMany(playedQuestions: PlayedQuestion[]){
        return axios.post(`${this.baseUrl}/PlayedQuestion/createPlayedQuestions`, playedQuestions);
    },
    update(playedQuestion: PlayedQuestion){
        return axios.put(`${this.baseUrl}/PlayedQuestion/Update`, playedQuestion);
    },
    delete(playedQuestionId: number){
        return axios.delete(`${this.baseUrl}/PlayedQuestion/Delete/${playedQuestionId}`);
    }

}