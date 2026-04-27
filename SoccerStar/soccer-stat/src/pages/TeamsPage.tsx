import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getTeams } from '../api/footballApi';
import type { Team } from '../types';
import { Loader } from '../components/Loader';
import { ErrorMessage } from '../components/ErrorMessage';

export function TeamsPage() {
    const [teams, setTeams] = useState<Team[]>([]);
    const [filteredTeams, setFilteredTeams] = useState<Team[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 12;

    useEffect(() => {
        async function loadTeams() {
            try {
                setLoading(true);
                const data = await getTeams();
                setTeams(data);
                setFilteredTeams(data);
                setError(null);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Неизвестная ошибка');
            } finally {
                setLoading(false);
            }
        }
        loadTeams();
    }, []);

    useEffect(() => {
        const filtered = teams.filter(team =>
            team.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredTeams(filtered);
        setCurrentPage(1);
    }, [searchTerm, teams]);

    const totalPages = Math.ceil(filteredTeams.length / itemsPerPage);
    const startIndex = (currentPage - 1) * itemsPerPage;
    const paginatedTeams = filteredTeams.slice(startIndex, startIndex + itemsPerPage);

    if (loading) return <Loader />;
    if (error) return <ErrorMessage message={error} />;

    return (
        <div style={{ padding: '1rem', maxWidth: '1200px', margin: '0 auto' }}>
            <h1 style={{ textAlign: 'center', marginBottom: '1.5rem' }}>Футбольные команды</h1>

            <input
                type="text"
                placeholder="🔍 Поиск команды..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                style={{
                    width: '100%',
                    padding: '0.75rem',
                    fontSize: '1rem',
                    border: '1px solid #ddd',
                    borderRadius: '8px',
                    marginBottom: '1.5rem',
                    boxSizing: 'border-box',
                }}
            />

            <div style={{
                display: 'grid',
                gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))',
                gap: '1rem',
            }}>
                {paginatedTeams.map((team) => (
                    <Link
                        key={team.id}
                        to={`/team/${team.id}/calendar`}
                        style={{ textDecoration: 'none' }}
                    >
                        <div style={{
                            border: '1px solid #e0e0e0',
                            borderRadius: '12px',
                            padding: '1rem',
                            backgroundColor: 'white',
                            textAlign: 'center',
                            transition: 'transform 0.2s, box-shadow 0.2s',
                            cursor: 'pointer',
                        }}
                            onMouseEnter={(e) => {
                                e.currentTarget.style.transform = 'translateY(-4px)';
                                e.currentTarget.style.boxShadow = '0 4px 12px rgba(0,0,0,0.1)';
                            }}
                            onMouseLeave={(e) => {
                                e.currentTarget.style.transform = 'translateY(0)';
                                e.currentTarget.style.boxShadow = 'none';
                            }}>
                            {team.crest || team.crestUrl ? (
                                <img
                                    src={team.crest || team.crestUrl}
                                    alt={team.name}
                                    style={{ width: '60px', height: '60px', objectFit: 'contain', marginBottom: '0.5rem' }}
                                />
                            ) : (
                                <div style={{ fontSize: '2rem', marginBottom: '0.5rem' }}>⚽</div>
                            )}
                            <h3 style={{ margin: '0', fontSize: '1rem', color: '#1a1a2e' }}>{team.name}</h3>
                        </div>
                    </Link>
                ))}
            </div>

            {totalPages > 1 && (
                <div style={{
                    display: 'flex',
                    justifyContent: 'center',
                    gap: '0.5rem',
                    marginTop: '2rem',
                    flexWrap: 'wrap',
                }}>
                    <button
                        onClick={() => setCurrentPage(p => Math.max(1, p - 1))}
                        disabled={currentPage === 1}
                        style={{
                            padding: '0.5rem 1rem',
                            borderRadius: '6px',
                            border: '1px solid #ddd',
                            backgroundColor: currentPage === 1 ? '#f0f0f0' : 'white',
                            cursor: currentPage === 1 ? 'not-allowed' : 'pointer',
                        }}
                    >
                        ← Назад
                    </button>
                    <span style={{ padding: '0.5rem 1rem' }}>
                        Страница {currentPage} из {totalPages}
                    </span>
                    <button
                        onClick={() => setCurrentPage(p => Math.min(totalPages, p + 1))}
                        disabled={currentPage === totalPages}
                        style={{
                            padding: '0.5rem 1rem',
                            borderRadius: '6px',
                            border: '1px solid #ddd',
                            backgroundColor: currentPage === totalPages ? '#f0f0f0' : 'white',
                            cursor: currentPage === totalPages ? 'not-allowed' : 'pointer',
                        }}
                    >
                        Вперед →
                    </button>
                </div>
            )}
        </div>
    );
}