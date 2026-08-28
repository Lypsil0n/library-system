"use client"

import { useRouter } from "next/navigation";
import { createLibraryCard } from "@/lib/api/library_cards";
import { toast } from 'react-toastify'

export default function LibraryCardForm() {
    const router = useRouter();
    
    async function handleCreate(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault()

        const formData = new FormData(e.currentTarget);

        const res = await createLibraryCard(formData)

        if (res?.error) {
            toast.error(res.error)
            return
        }

        toast.success(res?.message)
        router.push(`/staff/library_card/${res?.id}`)
    }

    return (
        <form
            onSubmit={handleCreate}
            className="flex flex-col gap-5">
            <div>
                <label
                htmlFor="ssn"
                className="mb-1 mt-1 block text-sm font-medium text-gray-700"
                >
                Personnummer
                </label>

                <input
                id="ssn"
                type="ssn"
                name="ssn"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="firstName"
                className="mb-1 mt-1 block text-sm font-medium text-gray-700"
                >
                Förnamn
                </label>

                <input
                id="firstName"
                type="firstName"
                name="firstName"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="lastName"
                className="mb-1 mt-1 block text-sm font-medium text-gray-700"
                >
                Efternamn
                </label>

                <input
                id="lastName"
                type="lastName"
                name="lastName"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="email"
                className="mb-1 mt-1 block text-sm font-medium text-gray-700"
                >
                E-post
                </label>

                <input
                id="email"
                type="email"
                name="email"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="homeAddress"
                className="mb-1 mt-1 block text-sm font-medium text-gray-700"
                >
                Adress
                </label>

                <input
                id="homeAddress"
                type="homeAddress"
                name="homeAddress"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="postalCode"
                className="mb-1 mt-1 block text-sm font-medium text-gray-700"
                >
                Postnummer
                </label>

                <input
                id="postalCode"
                type="postalCode"
                name="postalCode"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="city"
                className="mb-1 mt-1 block text-sm font-medium text-gray-700"
                >
                Postort
                </label>

                <input
                id="city"
                type="city"
                name="city"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <div>
                <label
                htmlFor="pinCode"
                className="mb-2 mt-2 block text-sm font-medium text-gray-700"
                >
                Pinkod
                </label>

                <input
                id="pinCode"
                type="pinCode"
                name="pinCode"
                className="w-full rounded-lg border border-gray-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                />
            </div>

            <button
                type="submit"
                className="mb-1 mt-1 rounded-lg bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700 active:scale-[0.98]"
            >
                Skapa
            </button>
        </form>
    )
}