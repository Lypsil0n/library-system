import { login } from "@/lib/api/authentication";

export default function LoginPage() {
    return (
        <div>
            <h1>Logga in som personal</h1>

            <form action={login}>

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
        </div>
    );
}