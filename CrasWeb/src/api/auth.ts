import { api } from './axios';
import axios from 'axios';

export async function login(username: string, password: string, rememberMe: boolean = false) {
    try {
        const { data } = await api.post('/auth/login', { username, password }, { params: { rememberMe } });
        return data.token;
    } catch (err) {
        if (axios.isAxiosError(err) && err.response) {
            throw err.response.data; 
        }
        throw { message: "Unable to connect to the server" };
    }
}
