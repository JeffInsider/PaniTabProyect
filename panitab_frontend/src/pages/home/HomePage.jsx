import { FaUserCircle, FaWarehouse, FaClipboardList, FaChartBar, FaIndustry, FaBox } from "react-icons/fa";
import Header from "../../components/Header";
import HeroSection from "../../components/HeroSection";
import ModuleButton from "../../components/buttons/ModuleButton";
import StatsWidget from "../../components/StatsWidget";
import Footer from "../../components/Footer";
import { useState } from "react";
import SiderBar from "../../components/Sidebar";

const HomePage = () => {
    const [showUserMenu, setShowUserMenu] = useState(false);
    const userName = "John Doe";
    const userEmail = "jon@";

    return (
        <div className="min-h-screen bg-gradient-to-b from-gray-100 to-gray-200">
            
            <div className="max-w-7xl mx-auto">
                <Header 
                    title="Pani Tab" 
                    userName={userName} 
                    userEmail={userEmail} 
                    showUserMenu={showUserMenu} 
                    setShowUserMenu={setShowUserMenu} 
                />

                <main className="p-8 space-y-10">
                    <HeroSection userName={userName} />

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
                            description="Visualización de gráficos e indicadores clave."
                            icon={FaChartBar}
                            onClick={() => console.log("Ir a Reportes")}
                        />
                    </section>

                    <StatsWidget />
                </main>

                
            </div>
            <Footer />
        </div>
    );
};

export default HomePage;