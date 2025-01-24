import StatCard from "./StatCard";

const StatsWidget = () => {
    return (
        <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            <StatCard title="Órdenes Activas" value="25" />
            <StatCard title="Materiales en Inventario" value="142" />
            <StatCard title="Producción de Hoy" value="350 unidades" />
        </section>
    );
};

export default StatsWidget;