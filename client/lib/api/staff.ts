export async function getUserData(userId: string, token: string) {
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/v1/get_staff_by_id`, {
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