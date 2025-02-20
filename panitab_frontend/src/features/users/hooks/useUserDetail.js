import { useNavigate, useParams } from "react-router-dom";
import { useUserStore } from "../store/useUserStore";
import { useEffect, useState } from "react";
import { disableUser, enableUser, getUserById, updateUser } from "../../../shared/actions/users/users.action";

export const useUserDetail = () => {
    const { userId } = useParams(); // Obtener el ID del usuario de los parámetros de la URL
    const navigate = useNavigate();
    const { users, updateUserInStore, addUserToStore } = useUserStore();
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);//controlar el estado de carga
    const [notification, setNotification] = useState(null);
    // Cargar el usuario al cargar la página, por si se pierde al recargar
    useEffect(() => {
        const fetchUser = async () => {
            let selectedUser = users.find((u) => u.id === userId);
            //si se borra el usuario del store, se vuelve a buscar
            if (!selectedUser) {
                //se trae la funcion de action.user
                selectedUser = await getUserById(userId);// Si no está en el store, buscar en API
                if (selectedUser) addUserToStore(selectedUser);
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
        setNotification(null);

        console.log("Formulario a enviar", formValues);

        //se envia el formulario pero se agrega el id del usuario
        //aqui se hace el llamado al action updateUser

        const updatedUser = await updateUser(userId, {
            id: userId, // Incluye el ID del usuario por que asi lo requiere la API
            firstName: formValues.firstName,
            lastName: formValues.lastName,
            email: formValues.email,
            role: formValues.roles,
        });

        if (updatedUser) {
            // Actualizar el usuario en el store y en el estado
            updateUserInStore(updatedUser);
            setUser(updatedUser);
            setNotification({ message: `Usuario ${updateUser.firstName} actualizado correctamente`, type: "success" });
            // Redirigir a la lista de usuarios
            setTimeout(() => navigate("/users"), 2000);
        } else {
            setNotification({ message: "Error al actualizar el usuario", type: "error" });
        }
        setLoading(false);
    };

    // Función para cambiar el estado de activo/inactivo del usuario
    const handleUpdateStatus = async (userId, isActive) => {
        setLoading(true);
        console.log(isActive);
        setNotification(null);

        // Llamar a la API para cambiar el estado
        const updatedUser = isActive 
            ? await disableUser(userId) 
            : await enableUser(userId);

        if (updatedUser) {
            // Actualizar el usuario en el store y en el estado
            updateUserInStore(updatedUser);
            setUser(updatedUser);
            setNotification({ message: `Usuario ${user.firstName} ${isActive ? "desactivado" : "activado"} correctamente`, type: "success" });
       } else {
            setNotification({ message: `Error al ${isActive ? "desactivar" : "activar"} el usuario`, type: "error" });
        }
        setLoading(false);
    };

    return { user, loading, handleUpdateUser, handleUpdateStatus, notification };
};