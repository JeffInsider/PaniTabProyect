const HeroSection = ({ userName }) => {
    return (
        <section className="bg-white shadow-lg p-8 rounded-xl text-center">
            <h2 className="text-3xl font-bold text-gray-800 mb-4">Bienvenido, {userName}!</h2>
            <p className="text-gray-600">Gestión integral para tu empresa. Accede a tus módulos principales y controla todo en un solo lugar.</p>
        </section>
    );
};

export default HeroSection;