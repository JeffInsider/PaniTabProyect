import { useEffect, useState } from "react";
import Header from "../../../shared/components/Header";
import { SideBarUser } from "../components/SideBarUser";
import { UserTable } from "../components/UserTable";
import { useUserStore } from "../store/useUserStore";
import { Loading } from "../../../shared/components/Loading";

export const UserListPage = () => {
    const [showSidebar, setShowSidebar] = useState(true);
    const [showUserMenu, setShowUserMenu] = useState(false);
    const { users, isLoading, loadUsers } = useUserStore();

    useEffect(() => {
        loadUsers();
    }, [loadUsers]);

    //console.log(users);

    if (isLoading) {
        return <Loading />;
    }

    //cargar usuarios
    return (
        <div className="flex h-screen bg-gray-50">
            <SideBarUser showSidebar={showSidebar} />

            <div className="flex-1 flex flex-col">
                {/* Header */}
                <Header
                    showSidebar={showSidebar}
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
                />

                {/* Contenido Principal */}
                <div className="container mx-auto p-6 pt-3 bg-gray-50 flex-1">
                    <main className="space-y-10">
                        <section className="text-2xl font-semibold text-gray-800 mb-4">
                            <h1 className="mt-2 mb-5">Gestión de Usuarios</h1>
                            {/* Tabla de Usuarios */}
                            <UserTable users={users}/>
                        </section>
                    </main>
                </div>
            </div>
        </div>
    );
};