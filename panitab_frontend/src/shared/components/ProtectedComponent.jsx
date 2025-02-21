import React, { useState } from "react";
import { useAuthStore } from "../../features/security/store"
import { NotificationCard } from "./NotificationCard";

export const ProtectedComponent = ({requiredRoles, children}) => {
    const [showMessage, setShowMessage] = useState(false);
    const [notification, setNotification] = useState(null);
    const roles = useAuthStore((state) => state.roles) ?? [];

    // Verificar si el usuario tiene algún rol requerido
    const hasPermission = roles.some((role) => requiredRoles.includes(role));

    const handleClick = (e) => {
        if (!hasPermission) {
            e.preventDefault();
            setShowMessage(true);
            setNotification({
                message: "Acceso no autorizado",
                type: "error",
            });
            setTimeout(() => {
                setShowMessage(false);
            }, 3000);      
        }
    };

    return (
        <div>
            {showMessage && (
                <NotificationCard message={notification.message} type={notification.type} />
            )}
            {children && React.cloneElement(children, { onClick: handleClick })}
        </div>
    );
};