export async function getAllBooks() { 
    const res = await fetch(`${process.env.API_URL}/api/v1/get_all_books`, {
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (!res.ok) {
        console.log(res)
        throw new Error(`Failed to fetch`)
    }

    return res.json()
}

export async function getBookByIsbn(isbn: string) {
    const res = await fetch(`${process.env.API_URL}/api/v1/get_book_by_isbn/${isbn}`, {
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (!res.ok) {
        console.log(res)
        throw new Error(`Failed to fetch`)
    }

    return res.json()
}