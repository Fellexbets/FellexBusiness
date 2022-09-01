import type { User } from "@/models/";
import axios from "axios";

export default{
    baseUrl: 'https://localhost:44331',

    getAll(){
        return axios.get(`${this.baseUrl}/User/GetAll`);
    },
    getById(userId: number){
        return axios.get(`${this.baseUrl}/User/GetById/${userId}`);
    },
    create(user: User){
        return axios.post(`${this.baseUrl}/User/Create`, user);
    },
    update(user: User){
        return axios.put(`${this.baseUrl}/User/Update`, user);
    },
    delete(userId: number){
        return axios.delete(`${this.baseUrl}/User/Delete/${userId}`);
    },
    authenticate(user: User){
        return axios.post(`${this.baseUrl}/User/Authenticate`, user);
    },
    getAllDummyUsers(){
        return axios.get(`${this.baseUrl}/User/GetAllDummyUsers`);
    },
    getFilteredDummyUsers(userIds: number[]){
        return axios.post(`${this.baseUrl}/User/GetFilteredDummyUsers`, userIds);
    },
    updateUser(user: User){
        return axios.put(`${this.baseUrl}/User/UpdateUser`, user);
    },
}