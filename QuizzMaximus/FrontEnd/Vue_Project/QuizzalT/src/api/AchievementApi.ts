import type { Achievement } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/Achievement/GetAll`);
    },
    getById(themeId: number, userId: number){
        return axios.get(`${this.baseUrl}/Achievement/GetById/${themeId},${userId}`);
    },
    create(achievement: Achievement){
        return axios.post(`${this.baseUrl}/Achievement/Create`, achievement);
    },
    update(achievement: Achievement){
        return axios.put(`${this.baseUrl}/Achievement/Update`, achievement);
    },
    delete(themeId: number, userId: number){
        return axios.delete(`${this.baseUrl}/Achievement/Delete/${themeId},${userId}`);
    },
    getAllAchievementsUser(userId: number){
        return axios.get(`${this.baseUrl}/Achievement/GetAllAchievementsUser/${userId}`)
    }
}