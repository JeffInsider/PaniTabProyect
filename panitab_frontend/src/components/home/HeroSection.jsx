const HeroSection = ({ userName }) => {
    return (
        <section className="bg-gradient-to-r from-[#f7f4f0] via-[#f0eae2] to-[#e0d6cc] py-6 px-8 shadow-lg rounded-xl text-center">
            <h2 className="text-4xl font-bold text-[#5a3825] mb-4">
                Bienvenido, {userName}!
            </h2>
            <p className="text-gray-700 text-lg mb-6">
                Gestión integral para tu empresa. Accede a tus módulos principales y controla todo en un solo lugar.
            </p>
            
        </section>
    );
};

export default HeroSection;