import { getUserData } from "@/lib/api/staff"
import { redirect } from "next/navigation";
import { isCurrentTokenValid, logout } from "@/lib/api/authentication";
import LibraryCardButton from "./LibraryCardButton";

export default async function StaffPage() {
    const userData = (await getUserData())?.data;

    const isTokenValid = await isCurrentTokenValid()

    if (!userData || userData.status === 401 || !isTokenValid.valid)
        redirect("/login")

    return(
        <div className="min-h-screen flex items-center justify-center bg-gray-100 p-6">
            <div className="w-full max-w-md rounded-2xl bg-white p-8 shadow-lg">
                <h1 className="text-3xl font-bold text-gray-800">
                Välkommen!
                </h1>

                <p className="mt-2 text-lg text-gray-700">
                {userData.firstName} {userData.lastName}
                </p>

                <p className="text-sm text-gray-500">
                @{userData.username}
                </p>

                <div className="my-2 h-px bg-gray-200" />

                <p className="mb-6 text-gray-600">
                Du är nu inloggad som personal.
                </p>


                <LibraryCardButton/>

                <form action={logout}>
                <button
                    type="submit"
                    className="w-full mt-2 rounded-lg bg-red-600 py-3 font-semibold text-white transition hover:bg-red-700 active:scale-[0.98]"
                    >
                    Logga ut
                </button>
                </form>
            </div>
        </div>
    )
}