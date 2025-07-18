import { panitabApi } from "../../../../config/api/panitabApi";


//metodo para obtemer la lista de customers
export const getCustomersList = async () => {
    try {
        const response = await panitabApi.get("/customers");
        //console.log("response", response.data.data);
        return response.data.data; //retorna la data de los customers
    } catch (error) {
        console.error("Error al obtener la lista de customers", error);
        return []; //retorna un array vacio si hay error
    }
}

//metodo para obtener un customer por id
export const getCustomerById = async (customerId) => {
    try {
        const response = await panitabApi.get(`/customers/${customerId}`);
        return response.data.data; //retorna la data del customer
    } catch (error) {
        console.error("Error al obtener el customer por id", error);
        return null; //retorna null si hay error
    }
}

//metodo para crear un customer
export const createCustomer = async (form) => {
    try {
        const response = await panitabApi.post("/customers", form);
        return response.data.data; //retorna la data del customer creado
    } catch (error) {
        console.error("Error al crear el customer", error);
        return null; //retorna null si hay error
    }
}

//metodo para actualizar un customer
export const updateCustomer = async (customerId, form) => {
    try {
        const response = await panitabApi.put(`/customers/${customerId}`, form);
        return response.data.data; //retorna la data del customer actualizado
    } catch (error) {
        console.error("Error al actualizar el customer", error);
        return null; //retorna null si hay error
    }
}

//metodo para desactivar un customer
export const disableCustomer = async (customerId) => {
    try {
        const response = await panitabApi.put(`/customers/disable/${customerId}`);
        return response.data.data; //retorna la data del customer desactivado
    } catch (error) {
        console.error("Error al desactivar el customer", error);
        return null; //retorna null si hay error
    }
}

//metodo para activar un customer
export const enableCustomer = async (customerId) => {
    try {
        const response = await panitabApi.put(`/customers/enable/${customerId}`);
        return response.data.data; //retorna la data del customer activado
    } catch (error) {
        console.error("Error al activar el customer", error);
        return null; //retorna null si hay error
    }
}