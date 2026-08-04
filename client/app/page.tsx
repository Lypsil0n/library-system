"use client"

import { redirect } from "next/navigation"

export default function Home() {
  function handleLogin() {
    redirect("/login")
  }

  return (
    <div>
        <h1>Bibliotek</h1>


        <button className="login-button" onClick={handleLogin}>Logga in</button>
    </div>
  );
}
