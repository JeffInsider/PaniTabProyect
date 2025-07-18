import {create} from "zustand";
import {getCustomersList} from "../../../../shared/actions/administration/customers/customers.action";

export const useCustomerStore = create((set) => ({
    customers: [],
    isLoading: false,
    setCustomers: (customers) => set({ customers }),
    setIsLoading: (isLoading) => set({ isLoading }),

    // Funcion para cargar los vendedores
    loadCustomers: async () => {
        set({ isLoading: true });
        const customers = await getCustomersList(); // Call the function getCustomersList
        // se usa new Map para eliminar duplicados
        set({ customers: [...new Map(customers.map((customer) => [customer.id, customer])).values()], isLoading: false });
    },

    // Function to add a customer to the store
    addCustomerToStore: (customer) =>
        set((state) => ({
            customers: state.customers.some((c) => c.id === customer.id) ? state.customers : [...state.customers, customer],
        })),

    // Function to update a customer in the store
    updateCustomerInStore: (updateCustomer) =>
        set((state) => ({
            customers: state.customers.map((customer) =>
                customer.id === updateCustomer.id ? updateCustomer : customer
            ),
        }))
}));