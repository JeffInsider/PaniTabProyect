import { create } from "zustand";

export const useNotificationStore = create((set) => ({
    notifications: [],
    addNotification: (message, type) => set((state) => ({
        notifications: [...state.notifications, { id: Date.now(), message, type }]
    })),
    removeNotification: (id) => set((state) => ({
        notifications: state.notifications.filter((notif) => notif.id !== id)
    })),
    clearNotifications: () => set({ notifications: [] })
}));
