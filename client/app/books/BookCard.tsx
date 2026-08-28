import type { Book } from "@/types/book";

export default function BookCard({ book }: { book: Book }) {
    return (
        <div className="flex h-full flex-col overflow-hidden rounded-xl bg-white shadow-lg">
            <div className="h-2 bg-gradient-to-r from-amber-700 via-amber-600 to-amber-700" />

            <div className="flex flex-1 flex-col p-8">
                <div className="mb-4 flex items-center justify-between text-xs font-medium uppercase tracking-widest text-gray-400">
                    <span>{book.lang}</span>
                    <span>{book.pages} sidor</span>
                </div>

                <h1 className="mb-2 line-clamp-2 text-3xl font-bold leading-tight text-gray-800">
                    {book.title}
                </h1>
                <p className="mb-6 line-clamp-1 text-lg text-gray-500">{book.author}</p>

                <div className="flex-1" />

                <div className="border-t border-gray-100 pt-4 flex items-center justify-between text-sm text-gray-500">
                    <span className="line-clamp-1">{book.publisher}</span>
                    <span className="shrink-0">{book.releaseDate?.slice(0, 4)}</span>
                </div>

                <div className="mt-6 flex items-center justify-between rounded-lg bg-gray-50 px-4 py-2">
                    <span className="text-sm text-gray-500">ISBN {book.isbn}</span>
                    <span
                        className={`text-sm font-semibold ${
                            book.amountAvailable > 0 ? "text-emerald-600" : "text-red-500"
                        }`}
                    >
                        {book.amountAvailable > 0
                            ? `${book.amountAvailable} Tillgänglig`
                            : "Ej tillgänglig"}
                    </span>
                </div>
            </div>
        </div>
    );
}