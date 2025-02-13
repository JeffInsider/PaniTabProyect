import { FaBars, FaUserCircle } from "react-icons/fa";
import { MdLogout, MdMail, MdNotifications, MdPerson } from "react-icons/md";
import { useEffect, useRef, useState } from "react";
import { useAuthStore } from "../../features/security/store";
import { useNavigate } from "react-router-dom";

const Header = ({ showUserMenu, setShowUserMenu,setShowSidebar,}) => {
    const userMenuRef = useRef();
    const logout = useAuthStore((state) => state.logout);
    const [showModal, setShowModal] = useState(false);
    const {user, init} = useAuthStore();
    const navigate = useNavigate();

    //llamar al init para obtener los datos del usuario
    useEffect(() => {
        init();
    }, [init]);
    
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

    const openModal = () => {
        setShowModal(true);
    };

    const closeModal = () => {
        setShowModal(false);
    };

    const confirmLogout = () => {
        logout();
        closeModal();
    };
    return (
        <>
            <header className="bg-[#f7f4f0] p-4 flex justify-between sm:mb-1">
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
                        <h1 className="font-bold text-lg text-[#5a3825] ml-4">PaniTab</h1>
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
                            {user?.fullName.split(" ")[0] ?? "Usuario"}
                        </span>
                        {showUserMenu && (
                            <div
                                ref={userMenuRef}
                                className="absolute right-0 top-full mt-2 bg-white shadow-lg rounded-lg w-72 p-4 border z-50"
                            >
                                <div className="flex flex-col items-center">
                                    <FaUserCircle className="text-8xl text-gray-700 mb-3" />
                                    <p className="font-bold text-gray-800 mb-1">{user?.fullName}</p>
                                    <p className="text-sm text-[#8a8a8a] mb-3">{user?.email}</p>
                                    <p className="text-xs font-semibold text-blue-600 bg-blue-100 px-2 py-1 rounded-lg">
                                        {useAuthStore.getState().roles[0] ?? "Sin rol"}
                                    </p>
                                </div>
                                <div className="flex flex-col w-full">
                                    {/*<button 
                                        className="flex items-center p-2 hover:bg-[#f7f4f0] text-[#5a3825]"
                                        onClick={() => navigate("/users/profile")}
                                    >
                                        <MdPerson className="mr-2 text-xl" /> Ver Perfil
                                    </button>*/}
                                    <button 
                                        className="flex items-center p-2 hover:bg-[#f7f4f0] text-red-600"
                                        onClick={openModal}
                                    >
                                        <MdLogout className="mr-2 text-xl" /> Cerrar Sesión
                                    </button>
                                </div>
                            </div>
                        )}
                    </div>
                </div>
            </header>

            {/* Modal de Confirmación */}
            {showModal && (
                <div className="fixed inset-0 bg-gray-500 bg-opacity-75 flex justify-center items-center z-50">
                    <div className="bg-white p-6 rounded-lg shadow-lg w-96">
                        <h2 className="text-lg font-semibold text-gray-700 mb-4">¿Estás seguro de que quieres cerrar sesión?</h2>
                        <div className="flex justify-between">
                            <button
                                onClick={closeModal}
                                className="bg-gray-200 px-4 py-2 rounded-lg text-gray-700 hover:bg-gray-300"
                            >
                                Cancelar
                            </button>
                            <button
                                onClick={confirmLogout}
                                className="bg-red-600 text-white px-4 py-2 rounded-lg hover:bg-red-700"
                            >
                                Sí, cerrar sesión
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </>
    );
} 

export default Header;