"use server"

import { cookies } from "next/headers";

export async function getLibraryCardById(id: string, userId: string, token: string) {
    const res = await fetch(`${process.env.API_URL}/api/v1/get_library_card_by_id/${id}`, {
        headers: {
            'Content-Type': 'application/json',
            'userId': userId,
            'token': token
        }
    })


    if (!res.ok) {
        console.log(res)
        throw new Error(`Failed to fetch`)
    }

    return res.json()
}

export async function createLibraryCard(formData: FormData) {
    const cookieStore = await cookies();
    const userId = cookieStore.get("userId")?.value;
    const token = cookieStore.get("token")?.value;

    if (!userId || !token) 
        return;

    const libraryCard = Object.fromEntries(formData.entries());

    console.log(libraryCard)
        
    const res = await fetch(`${process.env.API_URL}/api/v1/create_library_card`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'userId': userId,
            'token': token
        },
        body: JSON.stringify(libraryCard),
    })

    if (!res.ok) {
        let details;
        try {
            details = await res.json();
        } catch {
            details = await res.text();
        }

        return {
            success: false,
            error: "Something went wrong: missing information",
            details
        }
    }

    const json = await res.json();

    return {
        success: true,
        id: json.id,
        message: "Library card created successfully"
    }
}


export async function getAllLibraryCards() {
    const res = await fetch(`${process.env.API_URL}/api/v1/get_all_library_cards`)

    if (!res.ok) {
        throw new Error(`Failed to fetch`)
    }

    return res.json()
}

