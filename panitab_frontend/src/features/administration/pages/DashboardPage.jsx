import Header from "../../../shared/components/Header";
import { useLayout } from "../../../shared/hooks/useLayout";
import { SideBarAdministration } from "../components/SidebarAdministration";
import { MetricCard } from "../components/MetricCard";
import {QuickAccessCard} from "../components/QuickAccessCard";
import { FaCog, FaMoneyBillWave, FaUser, FaUserShield, FaUsers } from "react-icons/fa";

export const DashboardPage = () => {
    const { showSidebar, setShowSidebar, showUserMenu, setShowUserMenu } = useLayout();

    return (
        <div className="flex h-screen">
            <SideBarAdministration showSidebar={showSidebar} />

            {/* Contenido Principal */}
            <div className="flex-1 flex flex-col">
                {/* Header */}
                <Header
                    showSidebar={showSidebar}
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
                />

                {/* Contenido Principal */}
                <div className="container mx-auto p-4 flex-1 overflow-auto">
                    <h1 className="text-2xl font-bold mb-6">Dashboard de Administración</h1>
                    
                    {/* Métricas */}
                    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
                        <MetricCard title="Total Clientes" value="1,250" icon={<FaUser className='text-blue-500' />} />
                        <MetricCard title="Clientes Activos" value="1,100" icon={<FaUserShield className='text-green-500' />} />
                        <MetricCard title="Saldo Total" value="$25,000" icon={<FaMoneyBillWave className='text-yellow-500' />} />
                        <MetricCard title="Configuraciones" value="3" icon={<FaCog className='text-gray-500' />} />
                    </div>

                    {/* Secciones Rápidas */}
                    <h2 className="text-xl font-semibold mt-8 mb-4">Accesos Rápidos</h2>
                    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                        <QuickAccessCard title="Clientes" description="Gestiona clientes registrados" icon={FaUsers} />
                        <QuickAccessCard title="Usuarios" description="Administración de usuarios y roles" icon={FaUserShield} />
                        <QuickAccessCard title="Configuraciones" description="Ajustes generales del sistema" icon={FaCog} />
                    </div>
                </div>
            </div>
        </div>
    );
};