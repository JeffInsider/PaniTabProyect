import { useEffect, useState } from "react";
import StatsWidget from "./StatsWidget";
import { FaBars } from "react-icons/fa";
import logo from "../../images/logo.png";


const SidebarHome = ({showSidebar}) => {
    return (
        <div 
        className={`${
            showSidebar ? "w-56" : "w-0"
        } bg-white shadow-lg text-white h-screen transition-all duration-300 ease-in-out 
        flex flex-col items-center overflow-hidden`}
        >
            {/* Título del Sidebar */}
            {showSidebar && (
                <div className="p-5 pt-8 flex flex-col h-full items-center justify-between">
                    <img
                        src={logo}
                        alt="Company Logo"
                        className="h-20 w-20 mb-6 "
                    />
                    <h2 className="font-bold text-lg text-[#5a3825] ml-4 mb-6">Alertas y Resumen</h2>
                    <nav className="flex-grow">
                        <ul>
                            <StatsWidget />
                        </ul>
                    </nav>
                </div>
            )}
        </div>
    );
};

export default SidebarHome;