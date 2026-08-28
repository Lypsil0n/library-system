import { getAllBooks } from "@/lib/api/books"
import BookCard from "./BookCard"
import { Book } from "@/types/book"
import BackButton from "./BackButton"

export default async function BookPage() {
    const books = await getAllBooks()

    return (
        <div className="min-h-screen bg-gray-100 px-4 py-12">
            <BackButton/>
            <div className="mx-auto grid max-w-6xl grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
                {books.map((book: Book) => (
                    <BookCard key={book.id} book={book} />
                ))}
            </div>
        </div>
    )
}