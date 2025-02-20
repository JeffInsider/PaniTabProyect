import { FaBars, FaUserCircle } from "react-icons/fa";
import { MdLogout, MdMail, MdNotifications, MdPerson } from "react-icons/md";
import { useEffect, useRef, useState } from "react";
import { useAuthStore } from "../../features/security/store";
import { useNavigate } from "react-router-dom";
import { useNotificationStore } from "../store/notificationStore";

const Header = ({ showUserMenu, setShowUserMenu, setShowSidebar }) => {
  const userMenuRef = useRef();
  const logout = useAuthStore((state) => state.logout);
  const [showModal, setShowModal] = useState(false);
  const { user, init } = useAuthStore();
  const navigate = useNavigate();
  const { notifications, clearNotifications } = useNotificationStore();
  const [showNotifications, setShowNotifications] = useState(false);

  //llamar al init para obtener los datos del usuario
  useEffect(() => {
    init();
  }, [init]);

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
          <div className="relative">
            {/* Icono de notificaciones */}
            <MdNotifications
              className="text-2xl text-[#5a3825] cursor-pointer"
              onClick={() => {
                setShowNotifications(!showNotifications);
              }}
            />
            {notifications.length > 0 && !showNotifications && (
              <span className="absolute top-0 right-0 bg-gray-400 text-white text-xs px-1 rounded-full">
                {notifications.length}
              </span>
            )}

            {/* Menú desplegable de notificaciones */}
            {showNotifications && (
              <div className="absolute right-0 mt-2 w-64 bg-white shadow-lg rounded-lg p-4 border z-50">
                <h3 className="font-bold text-gray-700 mb-2 flex justify-between">
                  Notificaciones
                  <button
                    onClick={clearNotifications}
                    className="text-red-500 text-sm"
                  >
                    Limpiar
                  </button>
                </h3>
                {notifications.length === 0 ? (
                  <p className="text-gray-500 text-sm">No hay notificaciones</p>
                ) : (
                  notifications.map((notif) => (
                    <div
                      key={notif.id}
                      className={`p-2 rounded ${
                        notif.type === "error"
                          ? "bg-red-100 text-red-600"
                          : "bg-green-100 text-green-600"
                      }`}
                    >
                      {notif.message}
                    </div>
                  ))
                )}
              </div>
            )}
          </div>

          <MdMail className="text-2xl text-[#5a3825] cursor-pointer" />

          {/* Menú de usuario */}
          <div className="relative flex items-center space-x-2">
            <FaUserCircle
              className={`text-4xl text-gray-700 cursor-pointer ${
                showUserMenu ? "bg-[#e0e0e0] rounded-full p-2" : ""
              }`}
              onClick={() => setShowUserMenu(!showUserMenu)}
            />
            <span
              className={`text-gray-700 font-semibold cursor-pointer ${
                showUserMenu ? "text-[#bebebe]" : ""
              }`}
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
                  <p className="font-bold text-gray-800 mb-1">
                    {user?.fullName}
                  </p>
                  <p className="text-sm text-[#8a8a8a] mb-3">{user?.email}</p>
                  <p className="text-xs font-semibold text-blue-600 bg-blue-100 px-2 py-1 rounded-lg">
                    {useAuthStore.getState().roles[0] ?? "Sin rol"}
                  </p>
                </div>
                <div className="flex flex-col w-full">
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
            <h2 className="text-lg font-semibold text-gray-700 mb-4">
              ¿Estás seguro de que quieres cerrar sesión?
            </h2>
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
};

export default Header;
