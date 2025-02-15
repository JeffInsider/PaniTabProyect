import { useLocation, useNavigate } from "react-router-dom";
import logo from "../../images/logo.png";
import { FaHome, FaUser, FaUsers } from "react-icons/fa";

export const SideBar = ({title, options = [], showSidebar, isHome, children}) => {
    const navigate = useNavigate();
    const location = useLocation();

    return (
        <div 
            className={`${
                showSidebar ? "w-56" : "w-0"
            } bg-white shadow-lg h-screen transition-all duration-300 ease-in-out 
            flex flex-col items-center overflow-hidden`}
        >
            {showSidebar && (
                <div className="p-5 pt-8 flex flex-col h-full items-center">
                    <img
                        src={logo}
                        alt="Company Logo"
                        className="h-20 w-20 mb-6"
                    />
                    <h2 className="font-bold text-lg text-[#5a3825] mb-6">{title}</h2>

                    {children && <div className="mb-4 w-full">{children}</div>}

                    {/* Renderizar opciones dinámicamente */}
                    {options.length > 0 && (
                        <div className="flex flex-col w-full">
                            {options.map(({ label, icon: Icon, path }, index) => (
                                <button
                                    key={index}
                                    className={`flex items-center gap-3 w-48 p-4 mt-2 text-[#5a3825]
                                    ${location.pathname === path ? 'bg-[#f7f4f0]' : 'hover:bg-[#f7f4f0]'}
                                    rounded-lg transition duration-300`}
                                    onClick={() => navigate(path)}
                                >
                                    <Icon className="text-xl" />
                                    <span className="text-md font-semibold">{label}</span>
                                </button>
                            ))}
                        </div>
                    )}
                    {/* Pie de página */}
                    <div className="mt-auto w-full ">
                        {isHome ? (
                            <button
                                className="flex items-center gap-3 w-full p-3 mt-6 text-[#5a3825] hover:bg-[#f7f4f0] rounded-lg transition duration-300"
                                onClick={() => navigate("/users")}
                            >
                                <FaUsers className="text-xl" />
                                <span className="text-md  font-semibold">Ver Usuarios</span>
                            </button>
                        ) : (
                            <button
                                className="flex items-center gap-3 w-full p-3 mt-6 text-[#5a3825] hover:bg-[#f7f4f0] rounded-lg transition duration-300"
                                onClick={() => navigate("/")}
                            >
                                <FaHome className="text-xl" />
                                <span className="text-md font-semibold">Regresar</span>
                            </button>
                        )}
                    </div>
                </div>
                
            )}
        </div>
    );
}