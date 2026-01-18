import { createBrowserRouter } from 'react-router-dom';
import Login from './pages/Login';
import Register from './pages/Register';
import Home from './pages/Home';
import Forbidden from './pages/Forbidden';
//import { RequireAuth } from './auth/RequireAuth';

export const router = createBrowserRouter([
    { path: '/', element: <Home /> },
    { path: '/login', element: <Login /> },
    { path: '/register', element: <Register /> },
    { path: '/forbidden', element: <Forbidden /> },
]);
