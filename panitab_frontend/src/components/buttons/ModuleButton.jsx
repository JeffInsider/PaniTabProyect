const ModuleButton = ({ title, description, icon: Icon, onClick }) => {
    return (
        <div
            className="bg-gradient-to-br from-white to-gray-100 shadow-xl p-6 rounded-2xl flex flex-col items-center text-center 
                hover:shadow-2xl hover:bg-white hover:scale-105 transition-transform cursor-pointer border border-gray-300"
            onClick={onClick}
        >
            <Icon className="text-5xl text-red-500 mb-4" />
            <h3 className="text-xl font-semibold text-gray-800 mb-2">{title}</h3>
            <p className="text-sm text-gray-600">{description}</p>
        </div>
    );
};

export default ModuleButton;