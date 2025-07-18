import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useCustomerStore } from "../../store/customer/useCustomerStore";
import { createCustomer } from "../../../../shared/actions/administration/customers/customers.action";

export const useCreateCustomer = () => {
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState(""); // El mensaje de confirmación
    const [isError, setIsError] = useState(false); // Para mostrar el mensaje de error
    const [showConfirmation, setShowConfirmation] = useState(false); // Para mostrar el mensaje de confirmación

    const navigate = useNavigate();
    const {addCustomerToStore} = useCustomerStore(); // Para agregar el cliente al store
    const [notification, setNotification] = useState(null); // Para mostrar notificaciones

    //crear el customer
    const handleCreateCustomer = async (formValues) => {
        setLoading(true);
        setMessage(""); // Limpiar el mensaje de confirmación
        setShowConfirmation(false); // Ocultar el mensaje de confirmación
        setNotification(null); // Limpiar la notificación

        // Aquí verificamos y ajustamos la estructura del formulario
        const formattedForm = {
            identityNumber: formValues.identityNumber,
            firstName: formValues.firstName,
            lastName: formValues.lastName,
            phone: formValues.phone,
            //balance: formValues.balance,
        };

        console.log("Formulario a enviar", formattedForm);
        const createdCustomer = await createCustomer(formattedForm);
        console.log("Cliente creado", createdCustomer);

        if (createdCustomer) {
            addCustomerToStore(createdCustomer);
            setMessage("Cliente creado exitosamente");
            setShowConfirmation(true);
            setNotification({ message: "Cliente creado exitosamente", type: "success" });
            setTimeout(() => navigate("/customers"), 2000);
        } else {
            setMessage("Error al crear el cliente");
            setShowConfirmation(true);
            setNotification({ message: "Error al crear el cliente", type: "error" });
        }
        setLoading(false);
    };
    return { handleCreateCustomer, loading, message, showConfirmation, notification };
}