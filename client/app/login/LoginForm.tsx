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
            className="flex flex-col gap-5"
            >
            <div>
                <label
                htmlFor="email"
                className="mb-2 block text-sm font-medium text-gray-700"
                >
                E-post
                </label>

                <input
                id="email"
                type="email"
                name="email"
                placeholder="namn@företag.se"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="password"
                className="mb-2 block text-sm font-medium text-gray-700"
                >
                Lösenord
                </label>

                <input
                id="password"
                type="password"
                name="password"
                placeholder="••••••••"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <button
                type="submit"
                className="mt-2 rounded-lg bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700 active:scale-[0.98]"
            >
                Logga in
            </button>
        </form>
    )
}