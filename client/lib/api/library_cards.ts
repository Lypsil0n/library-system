import { notFound, unauthorized } from "next/navigation"

export async function getLibraryCardById(id: string, userId: string, token: string) {
    const res = await fetch(`${process.env.API_URL}/api/v1/get_library_card_by_id/${id}`, {
        headers: {
            'Content-Type': 'application/json',
            'userId': userId,
            'token': token
        }
    })

    if (res.status === 404) {
        notFound();
    }

    if (res.status === 401) {
        
    }

    if (!res.ok) {
        console.log(res)
        throw new Error(`Failed to fetch`)
    }

    return res.json()
}

export async function getAllLibraryCards() {
    const res = await fetch(`${process.env.API_URL}/api/v1/get_all_library_cards`)

    if (!res.ok) {
        throw new Error(`Failed to fetch`)
    }

    

    return res.json()
}

