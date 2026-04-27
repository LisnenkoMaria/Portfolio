import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getCompetitions } from '../api/footballApi';
import type { Competition } from '../types';
import { Loader } from '../components/Loader';
import { ErrorMessage } from '../components/ErrorMessage';

export function LeaguesPage() {
    const [leagues, setLeagues] = useState<Competition[]>([]);
    const [filteredLeagues, setFilteredLeagues] = useState<Competition[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 10;

    useEffect(() => {
        async function loadLeagues() {
            try {
                setLoading(true);
                const data = await getCompetitions();
                setLeagues(data);
                setFilteredLeagues(data);
                setError(null);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Неизвестная ошибка');
            } finally {
                setLoading(false);
            }
        }
        loadLeagues();
    }, []);

    useEffect(() => {
        const filtered = leagues.filter(league =>
            league.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredLeagues(filtered);
        setCurrentPage(1);
    }, [searchTerm, leagues]);

    const totalPages = Math.ceil(filteredLeagues.length / itemsPerPage);
    const startIndex = (currentPage - 1) * itemsPerPage;
    const paginatedLeagues = filteredLeagues.slice(startIndex, startIndex + itemsPerPage);

    if (loading) return <Loader />;
    if (error) return <ErrorMessage message={error} />;

    return (
        <div style={{ padding: '1rem', maxWidth: '1200px', margin: '0 auto' }}>
            <h1 style={{ textAlign: 'center', marginBottom: '1.5rem' }}>Футбольные лиги</h1>

            <input
                type="text"
                placeholder="🔍 Поиск лиги..."
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
                gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))',
                gap: '1rem',
            }}>
                {paginatedLeagues.map((league) => (
                    <Link
                        key={league.id}
                        to={`/league/${league.id}/calendar`}
                        style={{ textDecoration: 'none' }}
                    >
                        <div style={{
                            border: '1px solid #e0e0e0',
                            borderRadius: '12px',
                            padding: '1rem',
                            backgroundColor: 'white',
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
                            <h3 style={{ margin: '0 0 0.5rem 0', color: '#1a1a2e' }}>{league.name}</h3>
                            <p style={{ margin: 0, color: '#666' }}>Страна: {league.area?.name || '—'}</p>
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