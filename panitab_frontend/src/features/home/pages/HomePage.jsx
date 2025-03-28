import { FaUserCircle, FaWarehouse, FaClipboardList, FaChartBar, FaIndustry, FaBox } from "react-icons/fa";
import HeroSection from "../components/HeroSection";
import ModuleButton from "../components/buttons/ModuleButton";
import Header from "../../../shared/components/Header";
import { SidebarHome } from "../components/SidebarHome";
import { useLayout } from "../../../shared/hooks/useLayout";
import { useNavigate } from "react-router-dom";

const HomePage = () => {
    const { showSidebar, setShowSidebar, showUserMenu, setShowUserMenu } = useLayout();
    const navigate = useNavigate();

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
                    showSidebar={showSidebar}
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
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
                            onClick={() => navigate("/administration")}
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
                        
                    </section>
                </main>
                </div>
            </div>
        </div>
    );
};

export default HomePage;