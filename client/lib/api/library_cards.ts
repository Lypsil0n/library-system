export async function getLibraryCardById(id: string) {
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/v1/get_library_card_by_id/${id}`)

    if (!res.ok) {
        throw new Error(`Failed to fetch`)
    }

    return res.json()
}

export async function getAllLibraryCards() {
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/v1/get_all_library_cards`)

    if (!res.ok) {
        throw new Error(`Failed to fetch`)
    }

    return res.json()
}

