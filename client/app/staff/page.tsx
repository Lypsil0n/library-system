import { getUserData } from "@/lib/api/staff"
import { cookies } from "next/headers";
import { redirect } from "next/navigation";

export default async function StaffPage() {
    const cookieStore = await cookies();

    const userId = cookieStore.get("userId")?.value;
    const token = cookieStore.get("token")?.value;
    
    if (!userId || !token) {
        redirect("/login");
    }

    return(
        <div>
            <h1>Välkommen</h1>
        </div>
    )
}