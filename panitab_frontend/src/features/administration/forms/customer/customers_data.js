import * as Yup from "yup";

export const customerInitValues = {
    identityNumber: "",
    firstName: "",
    lastName: "",
    phone: "",
    balance: "",
    isActive: false,
};

export const customerValidationSchema = (isCreate) => {
    return Yup.object({
        identityNumber: Yup.string()
            .required("El número de identidad es requerido")
            .matches(/^\d+$/, "El número de identidad debe ser numérico"),
        firstName: Yup.string().required("El nombre es requerido"),
        lastName: Yup.string().required("El apellido es requerido"),
        phone: Yup.string()
            .required("El teléfono es requerido")
            .matches(/^\d+$/, "El teléfono debe ser numérico"),
        //solo validar balance en actualización
        ...(!isCreate && {
            balance: Yup.number()
                .required("El balance es requerido")
                .typeError("El balance debe ser un número"),
        }),
    });
}