import { useEffect, useState } from "react";
import StatsWidget from "./StatsWidget";
import { FaBars, FaUsers } from "react-icons/fa";
import logo from "../../../images/logo.png";
import { useNavigate } from "react-router-dom";


const SidebarHome = ({showSidebar}) => {
    const navigate = useNavigate();
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

                    <div className="flex flex-col w-full">
                        <button
                            className="flex items-center gap-3 w-full p-3 mt-6 text-[#5a3825] 
                            hover:bg-[#f7f4f0] rounded-lg transition duration-300"
                            onClick={() => navigate("/users")}
                        >
                            <FaUsers className="text-xl" />
                            <span className="text-md font-semibold">Usuarios</span>

                        </button>
                    </div>
                </div>
            )}
        </div>
    );
};

export default SidebarHome;