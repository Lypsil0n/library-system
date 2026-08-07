import { getUserData } from "@/lib/api/staff"
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { isCurrentTokenValid, logout } from "@/lib/api/authentication";

export default async function StaffPage() {
    // const validToken = await isCurrentTokenValid();

    // if (!validToken || !validToken.valid) {
    //     redirect("/login");
    // }

    const userData = (await getUserData())?.data;

    if (!userData || userData.status === 401)
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

                <form action={logout}>
                <button
                    type="submit"
                    className="w-full rounded-lg bg-red-600 py-3 font-semibold text-white transition hover:bg-red-700 active:scale-[0.98]"
                    >
                    Logga ut
                </button>
                </form>
            </div>
        </div>
    )
}