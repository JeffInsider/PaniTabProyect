import { useNavigate, useParams } from "react-router-dom";
import { useUserStore } from "../store/useUserStore";
import { useEffect, useState } from "react";
import { Loading } from "../../../shared/components/Loading";
import { useFormik } from "formik";
import { userInitValues, userValidationSchema } from "../forms/users_data";
import { disableUser, enableUser, getUserById, updateUser } from "../../../shared/actions/users/users.action";
import { UserForm } from "../components/UserForm";
import { SideBarUser } from "../components/SideBarUser";
import Header from "../../../shared/components/Header";
import { UserActiveStatus } from "../components/UserActiveStatus";
import { FaCheckCircle } from "react-icons/fa";

export const UserDetailPage = () => {
    const [showSidebar, setShowSidebar] = useState(true);
    const [showUserMenu, setShowUserMenu] = useState(false);
    const { userId } = useParams();
    const {users, updateUserInStore, addUserToStore} = useUserStore();
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true); //controlar el estado de carga
    const [showConfirmation, setShowConfirmation] = useState(false); // Para mostrar el mensaje de confirmación
    const [message, setMessage] = useState(""); // El mensaje de confirmación
    const navigate = useNavigate();

    // Cargar el usuario al cargar la página
    useEffect(() => {
        const fetchUser = async () => {
            let selectedUser = users.find((u) => u.id === userId);

            //si se borra el usuario del store, se vuelve a buscar
            if (!selectedUser) {
                selectedUser = await getUserById(userId); // Si no está en el store, buscar en API
                console.log("Se busco el usuario");
                if (selectedUser) {
                    addUserToStore(selectedUser); // Guardarlo en el store
                    console.log("Se agrego el usuario al store");
                }
            }

            //aquí se actualiza el estado del usuario
            setUser(selectedUser);
            setLoading(false);
        };

        fetchUser();
    }, [userId, users, addUserToStore]);

    // Función para actualizar el usuario
    const handleUpdateUser = async (formValues) => {
        setLoading(true);
        setMessage(""); // Limpiar el mensaje de confirmación
        setShowConfirmation(false); // Ocultar el mensaje de confirmación

        console.log("Formulario a enviar", formValues);

        //se envia el formulario pero se agrega el id del usuario
        //aqui se hace el llamado al action updateUser
        const updatedUser = await updateUser(userId, {
            id: userId, // Incluye el ID del usuario
            firstName: formValues.firstName,
            lastName: formValues.lastName,
            email: formValues.email,
            role: formValues.roles, // Asegúrate de que 'roles' sea un string
        });
    
        if (updatedUser) {
            // Actualizar el usuario en el store y en el estado
            updateUserInStore(updatedUser);
            setUser(updatedUser);

            setLoading(false);
            setShowConfirmation(true);
            setMessage("Usuario actualizado correctamente");

            // Redirigir a la lista de usuarios
            setTimeout(() => {
                navigate("/users");
            }, 2000);
        }else
        {
            setLoading(false);
            setMessage("Error al actualizar el usuario");
        }
    };

    // Función para cambiar el estado de activo/inactivo
    const handleUpdateStatus = async (userId, isActive) => {
        setLoading(true);
        setMessage(""); // Limpiar el mensaje de confirmación
        setShowConfirmation(false); // Ocultar el mensaje de confirmación

        // Llamar a la API para cambiar el estado
        const updatedUser = isActive
            ? await disableUser(userId)
            : await enableUser(userId);

        if (updatedUser) {
            // Actualizar el usuario en el store y en el estado
            updateUserInStore(updatedUser);
            setUser(updatedUser);

            setLoading(false);
            setShowConfirmation(true);
            setMessage(`Usuario ${isActive ? "desactivado" : "activado"} correctamente`);
        } else {
            setLoading(false);
            setMessage(`Error al ${isActive ? "desactivar" : "activar"} el usuario`);
        }
    };

    const handleCancel = () => {
        navigate("/users");
    };

    if (!user) {
        return <Loading />;
    };

    return (
        <div className="flex h-screen">
            {showConfirmation && (
                <div className="absolute top-10 left-1/2 transform -translate-x-1/2 z-50 max-w-md w-full">
                    <div className="bg-green-500 text-white text-center py-3 px-6 rounded-lg shadow-lg flex items-center justify-center space-x-2">
                        <FaCheckCircle className="w-5 h-5" />
                        <span>{message}</span>
                    </div>
                </div>
            )}
            <SideBarUser showSidebar={showSidebar} />
            <div className="flex-1 flex flex-col">
                <Header
                    showSidebar={showSidebar}
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
                />
                <div className="p-6">
                    <h2 className="text-2xl font-bold mb-4">Actualizar Usuario</h2>
                    <UserForm user={user} onSubmit={handleUpdateUser} loading={loading} isCreate={false}/>
                    <UserActiveStatus
                        isActive={user.isActive}
                        onToggleStatus={() => handleUpdateStatus(user.id, user.isActive)}
                    />
                </div>
                <div className="p-6 flex justify-end">
                    <button
                        onClick={handleCancel}
                        className="bg-gray-500 text-white font-semibold px-6 py-2 rounded-lg shadow-md hover:bg-gray-600"
                    >
                        Volver
                    </button>
                </div>
            </div>
        </div>
    );
};