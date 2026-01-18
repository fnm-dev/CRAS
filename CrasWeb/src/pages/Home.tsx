import { Button } from "@/components/ui/button"
import { useNavigate } from "react-router-dom"
import { useTheme } from "@/theme/ThemeProvider"
import { Sun, Moon } from "lucide-react"

export default function Home() {
    const navigate = useNavigate()
    const { toggle, theme } = useTheme()

    return (
        <div className="fixed inset-0 flex flex-col items-center justify-center gap-6 bg-background">

            <img
                src={theme === "dark" ? "./white_logo.png" : "./dark_logo.png"}
                className="h-40"
            />

            <div className="flex gap-4">
                <Button onClick={() => navigate("/login")}>
                    Sign in
                </Button>

                <Button variant="outline" onClick={() => navigate("/register")}>
                    Register
                </Button>
            </div>

            <button
                onClick={toggle}
                className="absolute top-6 right-6 p-2 rounded-full border"
            >
                {theme === "dark" ? <Sun size={18} /> : <Moon size={18} />}
            </button>
        </div>
    )
}
