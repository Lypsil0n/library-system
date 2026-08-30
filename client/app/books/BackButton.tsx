"use client"

import { useRouter } from "next/navigation";

export default function BackButton() {
    const router = useRouter();


    return (
        <button
            onClick={() => router.push("/")}
            className="mt-2 rounded-lg bg-red-600 px-8 py-4 font-semibold text-white transition hover:bg-red-700 active:scale-[0.98] cursor-pointer"
            >
            Tillbaka
        </button>
    )
}