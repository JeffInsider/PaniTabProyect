export const MetricCard = ({ title, value, icon }) => {
    return (
        <div className="p-4 bg-white shadow-lg rounded-lg flex items-center space-x-4">
            <div className="text-4xl">{icon}</div>
            <div>
                <h3 className="text-lg font-semibold">{title}</h3>
                <p className="text-xl font-bold">{value}</p>
            </div>
        </div>
    );
};