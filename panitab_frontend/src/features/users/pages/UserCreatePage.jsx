import { useState } from "react";
import { UserForm } from "../components/UserForm";
import { Loading } from "../../../shared/components/Loading";
import { SideBarUser } from "../components/SideBarUser";
import Header from "../../../shared/components/Header";
import { FaCheckCircle } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { useUserStore } from "../store/useUserStore";
import { createUser } from "../../../shared/actions/users/users.action";

export const UserCreatePage = () => {
    const [showSidebar, setShowSidebar] = useState(true);
    const [showUserMenu, setShowUserMenu] = useState(false);
    const [loading, setLoading] = useState(false);
    const [showConfirmation, setShowConfirmation] = useState(false); // Para mostrar el mensaje de confirmación
    const [message, setMessage] = useState(""); // El mensaje de confirmación
    const navigate = useNavigate();
    const {addUserToStore, isLoaging} = useUserStore();

    const handleCreateUser = async (formValues) => {
        setLoading(true);
    setMessage(""); // Limpiar el mensaje de confirmación
    setShowConfirmation(false); // Ocultar el mensaje de confirmación

    // Aquí verificamos y ajustamos la estructura del formulario
    const formattedForm = {
        firstName: formValues.firstName,
        lastName: formValues.lastName,
        email: formValues.email,
        password: formValues.password,
        confirmPassword: formValues.confirmPassword,
        role: formValues.roles,  // Asegúrate de que 'roles' sea una cadena, no un array
    };

    console.log("Formulario a enviar", formattedForm);

    const createdUser = await createUser(formattedForm);
    console.log("Usuario creado", createdUser);

    if (createdUser) {
        addUserToStore(createdUser);
        setMessage("Usuario creado exitosamente");
        setShowConfirmation(true);
        setLoading(false);
        setTimeout(() => {
            navigate("/users");
        }, 2000);
    } else {
        setMessage("Error al crear el usuario");
        setShowConfirmation(true);
        setLoading(false);
    }
    };

    const handleCancel = () => {
        navigate("/users");
    };

    if (loading) {
        return <Loading />;
    }
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
                    <UserForm user={[]} onSubmit={handleCreateUser} loading={loading} isCreate={true}/>
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
}