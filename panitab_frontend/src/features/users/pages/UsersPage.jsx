import { useState } from "react";
import { SideBarUser } from "../components/SideBarUser";
import Header from "../../../shared/components/Header";

export const UsersPage = () => {
    const [showSidebar, setShowSidebar] = useState(true);
    const [showUserMenu, setShowUserMenu] = useState(false);
    const [users, setUsers] = useState([]);

    //cargar usuarios
    return (
        <div className="flex h-screen">
            {/* Sidebar de Usuarios */}
            <SideBarUser showSidebar={showSidebar} />

            {/* Contenedor principal */}
            <div className="flex-1 flex flex-col">
                {/* Header */}
                <Header
                    showSidebar={showSidebar}
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
                />

                {/* Contenido Principal */}
                <div className="container overflow-auto mx-auto p-2 pt-3 md:p-3 lg:p-4 flex-1">
                    <main className="flex-1 space-y-10">
                        {/* Título de la página */}
                        <section className="text-2xl font-semibold text-gray-800 mb-4">
                            <h1>Gestión de Usuarios</h1>
                        </section>

                        {/* Lista de usuarios */}

                    </main>
                </div>
            </div>
        </div>
    );
}