import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import ToggleForm from './pages/ToggleForm';
import { useState } from 'react';

function App() {
    const [darkMode, setDarkMode] = useState(false);

    const toggleDarkMode = () => {
        setDarkMode(!darkMode);
        document.documentElement.classList.toggle('dark', !darkMode);
    };

    return (
        <Router>
            <div className={`min-h-screen ${darkMode ? 'dark bg-gray-900 text-white' : 'bg-gray-100 text-gray-900'}`}>
                <header className="p-4 shadow-md flex justify-between items-center">
                    <h1 className="text-xl font-bold">Switch</h1>
                    <button
                        onClick={toggleDarkMode}
                        className="bg-primary text-white px-4 py-2 rounded"
                    >
                        {darkMode ? 'Light Mode' : 'Dark Mode'}
                    </button>
                </header>
                <main className="p-6">
                    <Routes>
                        <Route path="/" element={<Dashboard />} />
                        <Route path="/toggle/new" element={<ToggleForm />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
}

export default App;
