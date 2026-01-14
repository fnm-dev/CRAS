import { useState } from 'react';
import { login as apiLogin } from '../api/auth';
import { useAuth } from '../auth/AuthContext';
import { useNavigate } from 'react-router-dom';

export default function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const { login } = useAuth();
    const navigate = useNavigate();

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError('');

        try {
            const token = await apiLogin(username, password);
            login(token);
            navigate('/');
        } catch (err: any) {
            setError(err.message ?? 'Erro inesperado');
        }
    }


    return (
        <div className="h-screen flex items-center justify-center">
            <form onSubmit={handleSubmit} className="w-96 space-y-4">
                <label>Email:</label>
                <input
                    className="border p-2 w-full"
                    placeholder="Email"
                    value={username}
                    onChange={e => setUsername(e.target.value)}
                />
                <label>Password:</label>
                <input
                    type="password"
                    className="border p-2 w-full"
                    placeholder="Password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                />
                {error && <p>{error}</p>}
                <button className="bg-blue-600 text-white p-2 w-full">
                    Sign-in
                </button>
            </form>
        </div>
    );
}
