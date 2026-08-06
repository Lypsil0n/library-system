import LoginForm from "./LoginForm";

export default function LoginPage() {
    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100 px-4">
        <div className="w-full max-w-md rounded-xl bg-white p-8 shadow-lg">
            <h1 className="mb-6 text-center text-3xl font-bold text-gray-800">
            Logga in som personal
            </h1>

            <LoginForm />
        </div>
        </div>
    );
}