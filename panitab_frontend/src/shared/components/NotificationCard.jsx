import { useState, useEffect } from "react";
import { FaTimes } from "react-icons/fa";
import { motion } from "framer-motion";
import { useNotificationStore } from "../store/notificationStore";

const notificationStyles = {
  success: "bg-green-100 border-l-4 border-green-500 text-green-700",
  error: "bg-red-100 border-l-4 border-red-500 text-red-700",
  info: "bg-blue-100 border-l-4 border-blue-500 text-blue-700",
};

export const NotificationCard = ({ message, type = "info" }) => {
  const [visible, setVisible] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setVisible(false);
    }, 3000);

    return () => clearTimeout(timer);
  }, [message, type]);

  if (!visible) return null;

  return (
    <motion.div
      initial={{ y: -50, opacity: 0 }}
      animate={{ y: 0, opacity: 1 }}
      exit={{ y: -50, opacity: 0 }}
      transition={{ duration: 0.3 }}
      className={`fixed top-16 inset-x-0 mx-auto w-[90%] max-w-xs sm:max-w-md md:max-w-lg p-4 rounded-lg shadow-md flex items-center justify-between ${notificationStyles[type]}`}
    >
      <span className="flex-1">{message}</span>
    </motion.div>
  );
};