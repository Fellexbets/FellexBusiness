import type { PlayedQuizz } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/PlayedQuizz/GetAll`);
    },
    getById(playedQuizzId: number){
        return axios.get(`${this.baseUrl}/PlayedQuizz/GetById/${playedQuizzId}`);
    },
    create(playedQuizz: PlayedQuizz){
        return axios.post(`${this.baseUrl}/PlayedQuizz/Create`, playedQuizz);
    },
    update(playedQuizz: PlayedQuizz){
        return axios.put(`${this.baseUrl}/PlayedQuizz/Update`, playedQuizz);
    },
    delete(playedQuizzId: number){
        return axios.delete(`${this.baseUrl}/PlayedQuizz/Delete/${playedQuizzId}`);
    }
}