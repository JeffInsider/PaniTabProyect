import { useEffect, useState } from "react"

// este archivo es para manejar bien lo del localstorage
const useLocalStorage = (key, defaultValue) => {
    // se crea un estado con el valor que se obtiene del localstorage
    const [value, setValue] = useState(() => {
        let currentValue;

        try {
            currentValue = JSON.parse(
                localStorage.getItem(key) || String(defaultValue)
            );
        } catch (error) {
            currentValue = defaultValue;
            console.error(error);
        }

        return currentValue;
    });

    // se actualiza el valor del localstorage cada vez que cambia el estado
    useEffect(() => {
        localStorage.setItem(key, JSON.stringify(value));
    }, [value, key]);

    return [value, setValue];
}

export default useLocalStorage;