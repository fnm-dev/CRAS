import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { useAuth } from "../auth/AuthContext"
import { login as apiLogin } from "../api/auth"

import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"

import { useTheme } from "@/theme/ThemeProvider"
import { Sun, Moon } from "lucide-react"

import { toast } from "sonner"
import { Checkbox } from "@/components/ui/checkbox"
import { Label } from "@/components/ui/label"

export default function Login() {
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [rememberMe, setRememberMe] = useState(false)
    const [error, setError] = useState("")
    const { login } = useAuth()
    const navigate = useNavigate()
    const { toggle, theme } = useTheme()


    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError("")

        try {
            const token = await apiLogin(username, password, rememberMe)
            login(token)
            navigate("/")
        } catch (err: any) {
            toast.error(err.message ?? "Unexpected error")
        }
    }

    return (
        <div className="fixed inset-0 flex flex-col items-center justify-center bg-background gap-6">

            <img
                src={theme === "dark" ? "./white_logo.png" : "./dark_logo.png"}
                alt="Logo"
                className="h-36"
            />

            <button
                onClick={toggle}
                className="absolute top-6 right-6 p-2 rounded-full border"
            >
                {theme === "dark" ? <Sun size={18} /> : <Moon size={18} />}
            </button>

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

                        <div className="flex items-center gap-3 ml-2">
                            <Checkbox id="rememberMe"
                                checked={rememberMe}
                                onCheckedChange={(e) => setRememberMe(!!e)}
                            />
                            <Label htmlFor="rememberMe">Remember-me?</Label>
                        </div>

                        <div className="space-y-3">
                            <div className="relative flex items-center">
                                <div className="flex-grow border-t" />
                                <span className="mx-2 text-xs text-muted-foreground">or</span>
                                <div className="flex-grow border-t" />
                            </div>

                            <Button variant="outline" className="w-full flex items-center gap-3">
                                <img src="/google_logo.svg" className="h-5 w-5" />
                                Sign in with Google
                            </Button>

                            <Button variant="outline" className="w-full flex items-center gap-3">
                                <img src="/github_logo.svg" className="h-5 w-5" />
                                Sign in with GitHub
                            </Button>
                        </div>

                        <Button className="w-full" type="submit">
                            Sign in
                        </Button>

                        <Button
                            type="button"
                            variant="link"
                            className="text-sm"
                            onClick={() => navigate("/register")}
                        >
                            Don't have an account? Register
                        </Button>
                    </form>
                </CardContent>
            </Card>
        </div>
    )
}
