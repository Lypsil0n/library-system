import { getUserData } from "@/lib/api/staff"
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { logout } from "@/lib/api/authentication";

export default async function StaffPage() {
    const cookieStore = await cookies();
    const userId = cookieStore.get("userId")?.value;
    const token = cookieStore.get("token")?.value;
    

    if (!userId || !token) {
        redirect("/login");
    }

    const userData = (await getUserData(userId, token)).data;

    return(
        <div>
            <h1>Välkommen {userData.firstName} {userData.lastName} ({userData.username})</h1>

            <form action={logout}>
                <button type="submit">Logga ut</button>
            </form>
        </div>
    )
}