import { getLibraryCardById } from "@/lib/api/library_cards";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import BackButton from "../create/BackButton";
import { isCurrentTokenValid } from "@/lib/api/authentication";

export default async function LibraryCard({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  const isTokenValid = await isCurrentTokenValid()

  if (!isTokenValid.valid) {
      redirect("/login");
  }

  const libraryCard = await getLibraryCardById(id);


  return (
    <div className="rounded-2xl bg-white p-8 shadow-lg mx-auto max-w-xlg">
      <h1 className="mb-6 text-3xl font-bold text-gray-800">
        Lånekort
      </h1>

      <div className="grid gap-4 sm:grid-cols-2">
        <div>
          <p className="text-sm text-gray-500">Namn</p>
          <p className="font-medium text-gray-900">
            {libraryCard.firstName} {libraryCard.lastName}
          </p>
        </div>

        <div>
          <p className="text-sm text-gray-500">Personnummer</p>
          <p className="font-medium text-gray-900">
            {libraryCard.ssn}
          </p>
        </div>

        <div>
          <p className="text-sm text-gray-500">E-post</p>
          <p className="font-medium text-gray-900">
            {libraryCard.email}
          </p>
        </div>

        <div>
          <p className="text-sm text-gray-500">Lånekortsnummer</p>
          <p className="font-mono text-gray-900 break-all">
            {libraryCard.id}
          </p>
        </div>

        <div className="sm:col-span-2">
          <p className="text-sm text-gray-500">Adress</p>
          <p className="font-medium text-gray-900">
            {libraryCard.homeAddress}
          </p>
          <p className="text-gray-700">
            {libraryCard.postalCode} {libraryCard.city}
          </p>
        </div>

        <div>
          <p className="text-sm text-gray-500">Status</p>
          <span
            className={`inline-flex rounded-full px-3 py-1 text-sm font-medium ${
              libraryCard.active
                ? "bg-green-100 text-green-700"
                : "bg-red-100 text-red-700"
            }`}
          >
            {libraryCard.active ? "Aktivt" : "Inaktivt"}
          </span>
        </div>

        <div>
          <p className="text-sm text-gray-500">Aktiva lån</p>
          <p className="font-medium text-gray-900">
            {libraryCard.currentLoanAmount}
          </p>
        </div>

        <div>
          <p className="text-sm text-gray-500">PIN-kod</p>
          <p className="font-mono text-gray-900">
            {libraryCard.pinCode}
          </p>
        </div>
      </div>
      <BackButton/>
    </div>
  )
}