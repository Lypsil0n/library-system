"use client"

import { useRouter } from "next/navigation";

export default function LibraryCardButton() {
    const router = useRouter();


    return (
        <button
            onClick={() => router.push("/staff/library_card/create")}
            className="w-full mt-2 rounded-lg bg-blue-600 py-3 font-semibold text-white transition hover:bg-blue-700 active:scale-[0.98]"
            >
            Lånekortshantering
        </button>
    )
}