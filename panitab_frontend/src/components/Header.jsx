import { FaUserCircle } from "react-icons/fa";
import logo from "../images/logo.png";
import { HiOutlineLogout } from "react-icons/hi";

const Header = ({ title, userName, userEmail, showUserMenu, setShowUserMenu }) => {
    return (
        <header className="bg-gray-700 text-white p-6 flex justify-between items-center shadow-md relative">
            <div className="flex items-center">
                <img 
                    src="../images/logo.png" 
                    alt="Company Logo" 
                    className="h-14 w-14 mr-3 rounded-full border-2 border-red-500"
                />
            </div>
            <h1 className="text-2xl font-bold text-center flex-grow tracking-wider">{title}</h1>
            <div className="relative">
                <div 
                    className="flex items-center gap-3 cursor-pointer" 
                    onClick={() => setShowUserMenu(!showUserMenu)}
                >
                    <FaUserCircle className="text-4xl text-red-500" />
                </div>
                {showUserMenu && (
                    <div className="absolute right-0 mt-2 bg-white shadow-lg rounded-lg w-64 p-4 border">
                        <p className="font-bold text-gray-800 mb-1">{userName}</p>
                        <p className="text-sm text-gray-600 mb-3">{userEmail}</p>
                        <button className="text-blue-500 text-sm mb-2">Ver Perfil</button>
                        <button className="text-red-500 text-sm">Cerrar Sesión</button>
                    </div>
                )}
            </div>
        </header>
    );
} 

export default Header;