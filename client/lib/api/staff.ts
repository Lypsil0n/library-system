import { cookies } from "next/headers";

export async function getUserData() {
    const cookieStore = await cookies();
    const userId = cookieStore.get("userId")?.value;
    const token = cookieStore.get("token")?.value;

    if (!userId || !token) 
        throw new Error("Missing authentication cookies");

    const res = await fetch(`${process.env.API_URL}/api/v1/get_staff_by_id`, {
        headers: {
            'Content-Type': 'application/json',
            'userId': userId,
            'token': token
        }
    })

    const data = await res.json();

    return {
        status: res.status,
        data,
    }
}