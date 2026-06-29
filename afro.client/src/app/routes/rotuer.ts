import { createElement } from "react";
import { createBrowserRouter } from "react-router-dom";
import { LoginPage } from "../../features/auth/pages/LoginPage";
import { DashboardPage } from "../../features/dashboard/pages/DashboardPage";
import { CreateOwnerPage } from "../../features/owners/pages/CreateOwnerPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { DashboardLayout } from "../layouts/DashboardLayout";
import { OwnerListPage } from "@/features/owners/pages/OwnerListPage";

export const router = createBrowserRouter([
    {
        path: "/login", element: createElement(LoginPage),
    },
    {
        element: createElement(ProtectedRoute),
        children: [
            {
                element: createElement(DashboardLayout),
                children:[
                    { path: "/", element: createElement(DashboardPage) },
                    { path: "/owners", element: createElement(OwnerListPage) },
                    { path: "/owners/create", element: createElement(CreateOwnerPage) },
                ],
            },
        ],

    },
    
]);