import LibraryCardForm from "./LibraryCardForm";
import BackButton from "./BackButton";
import { isCurrentTokenValid } from "@/lib/api/authentication";
import { redirect } from "next/navigation";

export default async function CreateLibraryCardPage() {
    const isTokenValid = await isCurrentTokenValid()

    if (!isTokenValid.valid) {
        redirect("/login")
    }
    return(
        <div className="min-h-screen flex items-center justify-center bg-gray-100 px-4">
            <div className="w-full max-w-md rounded-xl bg-white p-8 shadow-lg">
                <h1 className="text-3xl font-bold text-gray-800">
                Skapa nytt lånekort
                </h1>

                <LibraryCardForm />
                <BackButton/>
            </div>
         </div>
    )
}