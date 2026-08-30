import { getBookByIsbn } from "@/lib/api/books";

export default async function Book({
  params,
}: {
  params: Promise<{ isbn: string }>;
}) {
  const { isbn } = await params;

  const book = await getBookByIsbn(isbn)


  return (
    <div className="min-h-screen bg-gray-100 px-4 py-12">
        <p>{book.title}</p>
        <p>{book.author}</p>
    </div>
  )
}