import { FaBars, FaUserCircle } from "react-icons/fa";
import logo from "../images/logo.png";
import { HiOutlineLogout } from "react-icons/hi";
import { MdInbox, MdLogout, MdMail, MdNotifications, MdPerson, MdRedeem } from "react-icons/md";
import { useEffect, useRef } from "react";

const Header = ({ 
    title, 
    userName, 
    userEmail, 
    showSidebar, 
    showUserMenu, 
    setShowUserMenu,
    setShowSidebar,
}) => {
    const userMenuRef = useRef();

    //cerrar el menú de usuario al hacer click fuera de él
    useEffect(() => {
        const handleClickedOutside = (e) => {
            if (userMenuRef.current && !userMenuRef.current.contains(e.target)) {
                setShowUserMenu(false);
            }
        };
        document.addEventListener("mousedown", handleClickedOutside);
        return () => document.removeEventListener("mousedown", handleClickedOutside);
    }, [setShowUserMenu]);

    return (
        <header
            className="bg-[#f7f4f0] p-4 flex justify-between sm:mb-1"
        >
        
            <div className="flex items-center ">
                {/* Icono de menú */}
                <button
                    onClick={() => setShowSidebar((prev) => !prev)}
                    className="text-[#5a3825] text-2xl focus:outline-none"
                >
                    <FaBars />
                </button>
                {/* Logo y Título */}
                <div className={`flex items-center`}>
                    <h1 className="font-bold text-lg text-[#5a3825] ml-4">{title}</h1>
                </div>
            </div>

            {/* Notificaciones y usuario */}
            <div className="relative flex items-center space-x-6">
                {/* Notificaciones */}
                <MdNotifications className="text-2xl text-[#5a3825] cursor-pointer" />
                <MdMail className="text-2xl text-[#5a3825] cursor-pointer" />

                {/* Menú de usuario */}
                <div className="relative flex items-center space-x-2">
                    <FaUserCircle
                        className="text-4xl text-gray-700 cursor-pointer"
                        onClick={() => setShowUserMenu(!showUserMenu)}
                    />
                    <span
                        className="text-gray-700 font-semibold cursor-pointer"
                        onClick={() => setShowUserMenu(!showUserMenu)}
                    >
                        {userName}
                    </span>
                    {showUserMenu && (
                        <div
                            ref={userMenuRef}
                            className="absolute right-0 top-full mt-2 bg-white shadow-lg rounded-lg w-72 p-4 border z-50"
                        >
                            <div className="flex flex-col items-center">
                                <FaUserCircle className="text-8xl text-gray-700 mb-3" />
                                <p className="font-bold text-gray-800 mb-1">{userName}</p>
                                <p className="text-sm text-[#8a8a8a] mb-3">{userEmail}</p>
                            </div>
                            <div className="flex flex-col w-full">
                                <button className="flex items-center p-2 hover:bg-[#f7f4f0] text-[#5a3825]">
                                    <MdPerson className="mr-2 text-xl" /> Ver Perfil
                                </button>
                                <button className="flex items-center p-2 hover:bg-[#f7f4f0] text-red-600">
                                    <MdLogout className="mr-2 text-xl" /> Cerrar Sesión
                                </button>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </header>
    );
} 

export default Header;