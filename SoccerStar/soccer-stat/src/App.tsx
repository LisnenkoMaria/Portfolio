import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { LeaguesPage } from './pages/LeaguesPage';
import { LeagueCalendarPage } from './pages/LeagueCalendarPage';
import { TeamsPage } from './pages/TeamsPage';
import { TeamCalendarPage } from './pages/TeamCalendarPage';
import './index.css';

function App() {
    return (
        <BrowserRouter>
            <div style={{ minHeight: '100vh', backgroundColor: '#f5f5f5' }}>
                <nav style={{
                    backgroundColor: '#1a1a2e',
                    padding: '1rem',
                    display: 'flex',
                    gap: '2rem',
                    justifyContent: 'center',
                }}>
                    <Link to="/" style={{ color: 'white', textDecoration: 'none', fontSize: '1.1rem' }}>
                        🏆 Лиги
                    </Link>
                    <Link to="/teams" style={{ color: 'white', textDecoration: 'none', fontSize: '1.1rem' }}>
                        ⚽ Команды
                    </Link>
                </nav>

                <div style={{ padding: '1rem' }}>
                    <Routes>
                        <Route path="/" element={<LeaguesPage />} />
                        <Route path="/teams" element={<TeamsPage />} />
                        <Route path="/league/:id/calendar" element={<LeagueCalendarPage />} />
                        <Route path="/team/:id/calendar" element={<TeamCalendarPage />} />
                    </Routes>
                </div>
            </div>
        </BrowserRouter>
    );
}

export default App;