import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useUserStore } from "../store/useUserStore";
import { createUser } from "../../../shared/actions/users/users.action";

export const useCreateUser = () => {
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState("");// El mensaje de confirmación
    const [isError, setIsError] = useState(false);// Para mostrar el mensaje de error
    const [showConfirmation, setShowConfirmation] = useState(false);// Para mostrar el mensaje de confirmación
    const navigate = useNavigate();
    const { addUserToStore } = useUserStore();
    const [notification, setNotification] = useState(null);

    const handleCreateUser = async (formValues) => {
        setLoading(true);
        setMessage("");// Limpiar el mensaje de confirmación
        setShowConfirmation(false);// Ocultar el mensaje de confirmación
        setNotification(null);// Limpiar la notificación

        // Aquí verificamos y ajustamos la estructura del formulario
        const formattedForm = {
            firstName: formValues.firstName,
            lastName: formValues.lastName,
            email: formValues.email,
            password: formValues.password,
            confirmPassword: formValues.confirmPassword,
            role: formValues.roles,// Asegúrate de que 'roles' sea una cadena, no un array
        };

        console.log("Formulario a enviar", formattedForm);
        const createdUser = await createUser(formattedForm);
        console.log("Usuario creado", createdUser);

        if (createdUser) {
            addUserToStore(createdUser);
            setMessage("Usuario creado exitosamente");
            setShowConfirmation(true);
            setNotification({ message: "Usuario creado exitosamente", type: "success" });
            //setLoading(false);
            setTimeout(() => navigate("/users"), 2000);
        } else {
            setMessage("Error al crear el usuario");
            setShowConfirmation(true);
            setNotification({ message: "Error al crear el usuario", type: "error" });
            //setLoading(false);
        }
        setLoading(false);
    };

    return { handleCreateUser, loading, message, showConfirmation, notification };
};