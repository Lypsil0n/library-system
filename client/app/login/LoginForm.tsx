"use client"


import { useRouter } from "next/navigation";
import { login } from "@/lib/api/authentication"
import { toast } from 'react-toastify'

export default function LoginForm() {
    const router = useRouter();

    async function handleLogin(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault()

        const formData = new FormData(e.currentTarget);

        const res = await login(formData)

        if (res.error) {
            toast.error(res.error)
            return;
        }

        toast.success(res.message)
        router.push("/staff")
    }

    return (
    <form 
        onSubmit={handleLogin} 
        className="flex flex-col gap-4"
    >
        <input
            type="email"
            name="email"
            placeholder="E-post"
        />

        <input
            type="password"
            name="password"
            placeholder="Lösenord"
        />

        <button type="submit">
            Logga in
        </button>
    </form>
    )
}