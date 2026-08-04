"use client"

import { useRouter } from "next/navigation";
import { useState } from "react";
import { login } from "@/lib/api/authentication";

export default function Login() {
    const [details, setDetails] = useState({
        email: "",
        password: ""
    })

    const router = useRouter()

    async function handleLogin() {
        const result = await login(details.email, details.password)

        if (result.status === 401) {
            alert("Ogiltigt användarnamn eller lösenord")
        }

        if (result.status === 200) {
            document.cookie = `userId=${result.data.userId}; path=/; max-age=86400`
            document.cookie = `token=${result.data.token}; path=/; max-age=86400`

            router.push("/staff")
        }
    }

    return(
        <div>
            <form onSubmit={handleLogin}>
                <input 
                    type="email"
                    placeholder="E-post"
                    value={details.email}
                    onChange={(e) =>
                        setDetails({
                            ...details,
                            email: e.target.value,
                        })
                    }
                />

                <input 
                    type="password"
                    placeholder="Lösenord"
                    value={details.password}
                    onChange={(e) =>
                        setDetails({
                            ...details,
                            password: e.target.value,
                        })
                    }
                />

                <input 
                    type="button" 
                    value="Logga in"
                    onClick={handleLogin}
                    />
            </form>
        </div>
    );
}