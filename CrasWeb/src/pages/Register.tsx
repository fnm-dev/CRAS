import { useState } from "react"
import { useNavigate } from "react-router-dom"

import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Label } from "@/components/ui/label"

import { useTheme } from "@/theme/ThemeProvider"
import { Sun, Moon } from "lucide-react"

import { toast } from "sonner"

import { register as apiRegister } from "../api/auth"

export default function Register() {
    const [name, setName] = useState("")
    const [lastName, setLastName] = useState("")
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [confirmPassword, setConfirmPassword] = useState("")

    const fullName = `${name} ${lastName}`.trim()

    const navigate = useNavigate()
    const { toggle, theme } = useTheme()

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()

        if (password !== confirmPassword) {
            toast.error("Passwords do not match")
            return
        }

        try {
            await apiRegister({
                name,
                fullName,
                email,
                password,
            })

            toast.success("User created successfully")
            navigate("/login")
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
                        Create your account
                    </CardTitle>
                </CardHeader>

                <CardContent>
                    <form onSubmit={handleSubmit} className="flex flex-col gap-4">

                        <div className="grid grid-cols-2 gap-3">
                            <div className="space-y-1">
                                <Label className="text-xs text-muted-foreground">
                                    First name
                                </Label>
                                <Input
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    required
                                />
                            </div>

                            <div className="space-y-1">
                                <Label className="text-xs text-muted-foreground">
                                    Last name
                                </Label>
                                <Input
                                    value={lastName}
                                    onChange={(e) => setLastName(e.target.value)}
                                    required
                                />
                            </div>
                        </div>

                        <div className="space-y-1">
                            <Label className="text-xs text-muted-foreground">
                                Email
                            </Label>
                            <Input
                                type="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                            />
                        </div>

                        <div className="space-y-1">
                            <Label className="text-xs text-muted-foreground">
                                Password
                            </Label>
                            <Input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </div>

                        <div className="space-y-1">
                            <Label className="text-xs text-muted-foreground">
                                Confirm password
                            </Label>
                            <Input
                                type="password"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                required
                            />
                        </div>

                        <Button className="w-full mt-2" type="submit">
                            Register
                        </Button>

                        <Button
                            type="button"
                            variant="link"
                            className="text-sm"
                            onClick={() => navigate("/login")}
                        >
                            Already have an account? Sign in
                        </Button>

                    </form>
                </CardContent>
            </Card>
        </div>
    )
}
