import { FaUserCircle, FaWarehouse, FaClipboardList, FaChartBar, FaIndustry, FaBox } from "react-icons/fa";
import HeroSection from "../components/HeroSection";
import ModuleButton from "../components/buttons/ModuleButton";
import { useState } from "react";
import SidebarHome from "../components/SidebarHome";
import Header from "../../../shared/components/Header";

const HomePage = () => {
    const [showSidebar, setShowSidebar] = useState(true);
    const [showUserMenu, setShowUserMenu] = useState(false);

    return (
        <div className="flex h-screen">
            {/* Sidebar */}
            <SidebarHome showSidebar={showSidebar}/>

            
            {/* Contenedor principal */}
            <div
                className="flex-1 flex flex-col"
            >
                {/* Header */}
                <Header
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
                    showSidebar={showSidebar}
                />

                {/* Contenido Principal */}
                <div className="container overflow-auto mx-auto p-2 pt-3 md:p-3 lg:p-4 flex-1">
                <main
                    className="flex-1 space-y-10 transition-all duration-300"
                >
                    <HeroSection/>

                    <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 justify-items-center">
                        <ModuleButton
                            title="Administración"
                            description="Gestión de órdenes de compra y clientes."
                            icon={FaClipboardList}
                            onClick={() => console.log("Ir a Administración")}
                        />
                        <ModuleButton
                            title="Control de Bodega"
                            description="Control y registro de inventarios y movimientos."
                            icon={FaWarehouse}
                            onClick={() => console.log("Ir a Control de Bodega")}
                        />
                        <ModuleButton
                            title="Producción"
                            description="Monitoreo y registro de los procesos de producción."
                            icon={FaIndustry}
                            onClick={() => console.log("Ir a Producción")}
                        />
                        <ModuleButton
                            title="Inventario"
                            description="Registro y gestión de materiales."
                            icon={FaBox}
                            onClick={() => console.log("Ir a Inventario")}
                        />
                        <ModuleButton
                            title="Reportes"
                            description="Visualización de gráficos e indicadores clave."
                            icon={FaChartBar}
                            onClick={() => console.log("Ir a Reportes")}
                        />
                        <ModuleButton
                            title="Usuarios"
                            description="Gestión de roles y accesos de usuario."
                            icon={FaUserCircle}
                            onClick={() => console.log("Ir a Usuarios")}
                        />
                    </section>
                </main>
                </div>
            </div>
        </div>
    );
};

export default HomePage;