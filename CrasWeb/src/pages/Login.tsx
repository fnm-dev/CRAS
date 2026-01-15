import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { useAuth } from "../auth/AuthContext"
import { login as apiLogin } from "../api/auth"

import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"

export default function Login() {
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [error, setError] = useState("")
    const { login } = useAuth()
    const navigate = useNavigate()

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError("")

        try {
            const token = await apiLogin(username, password)
            login(token)
            navigate("/")
        } catch (err: any) {
            setError(err.message ?? "Erro inesperado")
        }
    }

    return (
        <div className="fixed inset-0 flex flex-col items-center justify-center bg-background gap-6">

            <img
                src="/logo_solo.png"
                alt="Logo"
                className="h-16"
            />

            <Card className="w-full max-w-sm">
                <CardHeader>
                    <CardTitle className="text-center text-xl md:text-2xl">
                        Sign in to CrasWeb
                    </CardTitle>
                </CardHeader>

                <CardContent>
                    <form onSubmit={handleSubmit} className="flex flex-col gap-4">
                        <Input
                            placeholder="Email"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />

                        <Input
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />

                        {error && (
                            <p className="text-sm text-red-500 text-center">
                                {error}
                            </p>
                        )}

                        <Button className="w-full" type="submit">
                            Sign in
                        </Button>
                    </form>
                </CardContent>
            </Card>
        </div>
    )
}
