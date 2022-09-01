import type { Relation } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/Relation/GetAll`);
    },
    getById(themeId: number){
        return axios.get(`${this.baseUrl}/Relation/GetById/${themeId}`);
    },
    create(theme: Relation){
        return axios.post(`${this.baseUrl}/Relation/Create`, theme);
    },
    update(theme: Relation){
        return axios.put(`${this.baseUrl}/Relation/Update`, theme);
    },
    delete(themeId: number){
        return axios.delete(`${this.baseUrl}/Relation/Delete/${themeId}`);
    }
}