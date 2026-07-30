import { getLibraryCardById } from "@/lib/api/library_cards";

export default async function LibraryCard({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  const libraryCard = await getLibraryCardById(id);

  return (
    <div>
        <p>
            <b>Namn</b>: {libraryCard.firstName} {libraryCard.lastName}
        </p>
        <p>
            <b>E-post</b>: {libraryCard.email}
        </p>
        <p>
            <b>Adress</b>: {libraryCard.homeAddress}, {libraryCard.postalCode} {libraryCard.city}
        </p>
    </div>
  )
}