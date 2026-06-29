import { Outlet } from "react-router-dom";

import { Footer } from "../../@/components/layout/Footer";
import { Sidebar } from "@/@/components/layout/Sidebar";
import { Header } from "@/@/components/layout/Header";

export function DashboardLayout(){
    return (
        <div className="min-h-screen flex bg-slate-50">
            <aside className="w-64 border-r">
                <Sidebar />
            </aside>

            <div className="flex-1 flex flex-col p-6">
                <Header />
            <main className="flex-1 p-6">
                <Outlet />
            </main>
            <Footer />
            </div>
         </div>
    )
}