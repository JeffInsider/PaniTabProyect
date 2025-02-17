import { create } from "zustand";
import { getUsersList } from "../../../shared/actions/users/users.action";

export const useUserStore = create((set) => ({
    users: [],
    isLoading: false,
    setUsers: (users) => set({ users }),
    setIsLoading: (isLoading) => set({ isLoading }),

    //funcion para cargar los usuarios
    loadUsers: async () => {
        set({ isLoading: true });
        const users = await getUsersList();//llamamos a la funcion getUsersList
        //se usa new Map para eliminar duplicados
        set({ users:[...new Map(users.map((user) => [user.id, user])).values()], isLoading: false });
    },

    //funcion para agregar un usuario al store
    addUserToStore: (user) => 
        set((state) => ({ 
            users: state.users.some((u) => u.id === user.id) ? state.users : [...state.users, user],
        })),

    //funcion para actualizar un usuario en el store
    updateUserInStore: (updateUser) => 
        set((state) => ({
            users: state.users.map((user) => 
                user.id === updateUser.id ? updateUser : user
        ),
    }))
}));