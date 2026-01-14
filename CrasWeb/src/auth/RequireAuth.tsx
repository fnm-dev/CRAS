import { Navigate } from 'react-router-dom';
import { useAuth } from './AuthContext';

export function RequireAuth({ roles, children }: any) {
    const { user } = useAuth();

    if (!user) return <Navigate to="/login" />;

    const userRoles = Array.isArray(user.role)
        ? user.role
        : user.role
            ? [user.role]
            : [];

    if (roles && !roles.some((r: string) => userRoles.includes(r))) {
        return <Navigate to="/forbidden" />;
    }

    return children;
}
