import { api } from './axios';
import axios from 'axios';

export async function login(username: string, password: string) {
    try {
        const { data } = await api.post('/auth/login', { username, password });
        return data.token;
    } catch (err) {
        if (axios.isAxiosError(err) && err.response) {
            throw err.response.data; 
        }
        throw { message: 'Erro de conexão' };
    }
}
