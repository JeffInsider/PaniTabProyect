import { useEffect, useState } from "react";
import Header from "../../../shared/components/Header";
import { SideBarUser } from "../components/SideBarUser";
import { UserTable } from "../components/UserTable";
import { useUserStore } from "../store/useUserStore";
import { Loading } from "../../../shared/components/Loading";
import { Link } from "react-router-dom";
import { ProtectedComponent } from "../../../shared/components/ProtectedComponent";

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
                    <main className="flex-1 space-y-10 transition-all duration-300">
                        <section className=" font-semibold text-gray-800 mb-4">
                            <div className="flex justify-between items-center">
                                <h1 className="mt-2 mb-5 text-2xl">Gestión de Usuarios</h1>
                                <ProtectedComponent requiredRoles={["ADMIN"]}>
                                    <Link to="/users/create" className="bg-gray-500 text-white font-semibold px-6 py-2 rounded-lg shadow-md hover:bg-gray-600">
                                        Crear Usuario
                                    </Link>
                                </ProtectedComponent>
                            {/* Tabla de Usuarios */}
                            
                            </div>
                            <UserTable users={users} />
                        </section>
                    </main>
                </div>
            </div>
        </div>
    );
};