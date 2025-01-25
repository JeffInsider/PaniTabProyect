import { AiOutlineDashboard } from "react-icons/ai";
import { BsGraphUp } from "react-icons/bs";
import { FaWarehouse } from "react-icons/fa";
import { FiSettings } from "react-icons/fi";
import { GiFactory } from "react-icons/gi";
import { MdInventory2, MdPointOfSale } from "react-icons/md";
import { NavLink } from "react-router-dom";

const SiderBar = () => {
    return (
        <aside className="w-64 bg-gray-800 text-white h-screen felx flex-col"> 
            <div className="p-4 flex items-center justify-center">
                <a href="" className="block">
                    <img 
                        src="/path-to-logo.jpg" 
                        alt="Logo" 
                        className="w-16 h-16 rounded-full" />
                </a>
            </div>

            <nav className="flex-1 overflow-y-auto">
                <ul className="space-y-2">
                    <li>
                        <NavLink
                            to="/"
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-4 py-3 hover:bg-gray-700 rounded-lg ${
                                  isActive ? "bg-gray-700" : ""
                                }`
                            }  
                        >
                            <AiOutlineDashboard />
                            <span className="text-sm">Inicio</span>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            to="/about"
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-4 py-3 hover:bg-gray-700 rounded-lg ${
                                  isActive ? "bg-gray-700" : ""
                                }`
                            }  
                        >
                            <MdPointOfSale />
                            <span className="text-sm">Administracion</span>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            to="/contact"
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-4 py-3 hover:bg-gray-700 rounded-lg ${
                                  isActive ? "bg-gray-700" : ""
                                }`
                            }  
                        >
                            <FaWarehouse />
                            <span className="text-sm">Control de bodega</span>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            to="/contact"
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-4 py-3 hover:bg-gray-700 rounded-lg ${
                                  isActive ? "bg-gray-700" : ""
                                }`
                            }  
                        >
                            <GiFactory />
                            <span className="text-sm">Produccion</span>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            to="/contact"
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-4 py-3 hover:bg-gray-700 rounded-lg ${
                                  isActive ? "bg-gray-700" : ""
                                }`
                            }  
                        >
                            <MdInventory2 />
                            <span className="text-sm">Inventario</span>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            to="/contact"
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-4 py-3 hover:bg-gray-700 rounded-lg ${
                                  isActive ? "bg-gray-700" : ""
                                }`
                            }  
                        >
                            <BsGraphUp />
                            <span className="text-sm">Reportes</span>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            to="/contact"
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-4 py-3 hover:bg-gray-700 rounded-lg ${
                                  isActive ? "bg-gray-700" : ""
                                }`
                            }  
                        >
                            <FiSettings />
                            <span className="text-sm">Configuracion</span>
                        </NavLink>
                    </li>
                </ul>
            </nav>
        </aside>
    );
    }

export default SiderBar;