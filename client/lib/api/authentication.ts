"use server";
import { ok } from "assert";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";

export async function login(formData: FormData) {
    const email = formData.get("email") as string;
    const password = formData.get("password") as string;

    const cookieStore = await cookies();
    
    const res = await fetch(`${process.env.API_URL}/api/v1/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            email,
            password,
        }),
    });

    if (res.status === 401) {
        return {
            success: false,
            error: "Invalid email or password"
        }
    }

    if (!res.ok) {
        return {
            success: false,
            error: "Something went wrong"
        }
    }

    const json = await res.json();

    cookieStore.set("userId", json.data.userId)
    cookieStore.set("token", json.data.token)

    return {
        success: true,
        message: "Login successful"
    }
}

export async function logout() {
    const cookieStore = await cookies();

    const userId = cookieStore.get("userId")?.value

    await fetch(`${process.env.API_URL}/api/v1/logout`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            userId: userId,
        })
    })

    cookieStore.delete("userId");
    cookieStore.delete("token");

    redirect("/login")
}