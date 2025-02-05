import * as Yup from "yup";

export const loginInitValues = {
    email: "",
    password: "",
};

export const loginValidationSchema = Yup.object({
    email: Yup.string()
        .required("El correo electronico es requerido")
        .email("Email invalido"),
    password: Yup.string()
        .required("La contraseña es requerida"),
});