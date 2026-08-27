import LibraryCardForm from "./LibraryCardForm";

export default function CreateLibraryCardPage() {
    return(
        <div className="min-h-screen flex items-center justify-center bg-gray-100 px-4">
            <div className="w-full max-w-md rounded-xl bg-white p-8 shadow-lg">
                <h1 className="text-3xl font-bold text-gray-800">
                Skapa nytt lånekort
                </h1>

                <LibraryCardForm />
            </div>
         </div>
    )
}