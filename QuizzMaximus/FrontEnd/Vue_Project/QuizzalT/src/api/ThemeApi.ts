import type { Theme } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/Theme/GetAll`);
    },
    getById(themeId: number){
        return axios.get(`${this.baseUrl}/Theme/GetById/${themeId}`);
    },
    create(theme: Theme){
        return axios.post(`${this.baseUrl}/Theme/Create`, theme);
    },
    update(theme: Theme){
        return axios.put(`${this.baseUrl}/Theme/Update`, theme);
    },
    delete(themeId: number){
        return axios.delete(`${this.baseUrl}/Theme/Delete/${themeId}`);
    }
}