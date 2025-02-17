import { panitabApi } from "../../../config/api/panitabApi"

//metodo para obtener la lista de usuarios
export const getUsersList = async () => {
    try {
        const response = await panitabApi.get("/users");
        return response.data.data; //retorna la data de los usuarios
    } catch (error) {
        console.error("Error al obtener la lista de usuarios", error);
        return [];
    }
}

//metodo para obtener un usuario por id
export const getUserById = async (userId) => {
    try {
        const response = await panitabApi.get(`/users/${userId}`);
        return response.data.data; //retorna la data del usuario
    } catch (error) {
        console.error("Error al obtener el usuario por id", error);
        return null;
    }
}

//metodo para crear un usuario
export const createUser = async (form) => {
    try {
        const response = await panitabApi.post("/users", form);
        return response.data.data; //retorna la data del usuario creado
    } catch (error) {
        console.error("Error al crear el usuario", error);
        return null;
    }
}

//metodo para actualizar un usuario
export const updateUser = async (userId, form) => {
    try {
        const response = await panitabApi.put(`/users/${userId}`, form);
        return response.data.data; //retorna la data del usuario actualizado
    } catch (error) {
        console.error("Error al actualizar el usuario", error);
        return null;
    }
}

//metodo para desactivar un usuario
export const disableUser = async (userId) => {
    try {
        const response = await panitabApi.put(`/users/disable/${userId}`);
        return response.data.data; //retorna la data del usuario desactivado
    } catch (error) {
        console.error("Error al desactivar el usuario", error);
        return null;
    }
}

//metodo para activar un usuario
export const enableUser = async (userId) => {
    try {
        const response = await panitabApi.put(`/users/enable/${userId}`);
        return response.data.data; //retorna la data del usuario activado
    } catch (error) {
        console.error("Error al activar el usuario", error);
        return null;
    }
}
