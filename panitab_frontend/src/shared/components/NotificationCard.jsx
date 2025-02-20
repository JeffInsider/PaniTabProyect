import { useState, useEffect } from "react";
import { FaTimes } from "react-icons/fa";
import { motion } from "framer-motion";
import { useNotificationStore } from "../store/notificationStore";

const notificationStyles = {
  success: "bg-green-600 text-white",
  error: "bg-red-600 text-white",
  info: "bg-gray-600 text-white",
};

export const NotificationCard = ({ message, type = "info" }) => {
  const [visible, setVisible] = useState(true);
  const { addNotification } = useNotificationStore(); // Agregar notificación al cerrar

  const handleClose = () => {
    setVisible(false);
    addNotification(message, type); // Guardar en el store al cerrar
  };

  if (!visible) return null;

  return (
    <motion.div
      initial={{ y: -50, opacity: 0 }}
      animate={{ y: 0, opacity: 1 }}
      exit={{ y: -50, opacity: 0 }}
      transition={{ duration: 0.3 }}
      className={`fixed top-16 inset-x-0 mx-auto w-[90%] max-w-xs sm:max-w-md md:max-w-lg p-4 rounded-lg shadow-lg flex items-center justify-between ${notificationStyles[type]}`}
    >
      <span className="flex-1">{message}</span>
      <button className="ml-4" onClick={handleClose}>
        <FaTimes />
      </button>
    </motion.div>
  );
};