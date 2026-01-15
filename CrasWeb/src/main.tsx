import React from 'react';
import ReactDOM from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';
import { router } from './routes';
import { AuthProvider } from './auth/AuthContext';
import './index.css';
import { ThemeProvider } from './theme/ThemeProvider';
import { Toaster } from "sonner"

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <ThemeProvider>
                <AuthProvider>
                    <RouterProvider router={router} />
                    <Toaster richColors position="top-right" />
                </AuthProvider>
        </ThemeProvider>  
    </React.StrictMode>
);
