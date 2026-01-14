import { createContext, useContext, useState } from 'react';
import { jwtDecode } from 'jwt-decode';

type User = {
    sub: string;
    role: string[];
};

type AuthContextType = {
    user: User | null;
    login: (token: string) => void;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType>(null!);

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [user, setUser] = useState<User | null>(null);

    function login(token: string) {
        localStorage.setItem('token', token);
        setUser(jwtDecode(token));
    }

    function logout() {
        localStorage.removeItem('token');
        setUser(null);
    }

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export const useAuth = () => useContext(AuthContext);
