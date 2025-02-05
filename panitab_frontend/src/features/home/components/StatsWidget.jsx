import StatCard from "./StatCard";

const StatsWidget = () => {
    return (
        <section className="flex flex-col space-y-4">
            <StatCard title="Órdenes Activas" value="25" />
            <StatCard title="Materiales en Inventario" value="142" />
            <StatCard title="Producción de Hoy" value="350 unidades" />
        </section>
    );
};

export default StatsWidget;