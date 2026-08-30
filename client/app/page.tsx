"use client"

import { useRouter } from "next/navigation";

export default function Home() {
  const router = useRouter()

  function handleLogin() {
    router.push("/login")
  }

  function handleBooks() {
    router.push("/books")
  }

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-100 px-4">
      <div className="w-full max-w-md rounded-2xl bg-white p-8 text-center shadow-lg">
        <h1 className="text-3xl font-bold text-gray-900">
          Bibliotek
        </h1>

        <div className="my-2 h-px bg-gray-200" />
        
        <button
          onClick={handleBooks}
          className="w-full rounded-lg mt-2 bg-blue-400 py-3 font-semibold text-white transition hover:bg-blue-500 active:scale-[0.98] cursor-pointer"
        >
          Sök efter böcker
        </button>

        <button
          onClick={handleLogin}
          className="w-full rounded-lg mt-2 bg-blue-600 py-3 font-semibold text-white transition hover:bg-blue-700 active:scale-[0.98] cursor-pointer"
        >
          Logga in som personal
        </button>
      </div>
    </div>
  );
}
