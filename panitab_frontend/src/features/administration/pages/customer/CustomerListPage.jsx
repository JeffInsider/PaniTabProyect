import { useEffect } from "react";
import { useLayout } from "../../../../shared/hooks/useLayout";
import {useCustomerStore} from "../../store/customer/useCustomerStore";
import { SideBarAdministration } from "../../components/SidebarAdministration";
import Header from "../../../../shared/components/Header";
import { ProtectedComponent } from "../../../../shared/components/ProtectedComponent";
import { Link } from "react-router-dom";
import { CustomerTable } from "../../components/customer/CustomerTable";
import { rolesListConstant } from "../../../../shared/constants/roles-list.constants";
import { Loading } from "../../../../shared/components/Loading";

export const CustomerListPage = () => {
    const { showSidebar, setShowSidebar, showUserMenu, setShowUserMenu } = useLayout();
    const { customers, isLoading, loadCustomers } = useCustomerStore();

    useEffect(() => {
        loadCustomers();
    }
    , [loadCustomers]);

    if (isLoading) {
        return <Loading />;
    }

    return (
        <div className="flex h-screen bg-gray-50">
            <SideBarAdministration showSidebar={showSidebar} />

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
                                <h1 className="mt-2 mb-5 text-2xl">Gestión de Clientes</h1>
                                <ProtectedComponent requiredRoles={[rolesListConstant.ADMIN, rolesListConstant.OFFICE]}>
                                    <Link to="/administration/customers/create" className="bg-gray-500 text-white font-semibold px-6 py-2 rounded-lg shadow-md hover:bg-gray-600">
                                        Crear Cliente
                                    </Link>
                                </ProtectedComponent>
                            </div>
                            <CustomerTable customers={customers} />
                        </section>
                    </main>
                </div>
            </div>
        </div>
    );
};