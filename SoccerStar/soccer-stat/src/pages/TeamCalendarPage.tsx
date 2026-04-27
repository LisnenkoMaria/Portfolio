import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { getMatchesByTeam } from '../api/footballApi';
import type { Match } from '../types';
import { Loader } from '../components/Loader';
import { ErrorMessage } from '../components/ErrorMessage';
import { MatchCard } from '../components/MatchCard';

export function TeamCalendarPage() {
    const { id } = useParams<{ id: string }>();
    const [matches, setMatches] = useState<Match[]>([]);
    const [filteredMatches, setFilteredMatches] = useState<Match[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [dateFrom, setDateFrom] = useState('');
    const [dateTo, setDateTo] = useState('');
    const [teamName, setTeamName] = useState('');

    useEffect(() => {
        async function loadMatches() {
            try {
                setLoading(true);
                setError(null);

                const from = dateFrom ? dateFrom.split('.').reverse().join('-') : '';
                const to = dateTo ? dateTo.split('.').reverse().join('-') : '';

                const data = await getMatchesByTeam(
                    Number(id),
                    from || undefined,
                    to || undefined
                );
                setMatches(data);
                setFilteredMatches(data);

                if (data.length > 0) {
                    setTeamName(data[0].homeTeam.name);
                }
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Не удалось загрузить матчи');
            } finally {
                setLoading(false);
            }
        }

        if (id) {
            loadMatches();
        }
    }, [id, dateFrom, dateTo]);

    const handleFilter = () => {
        let filtered = [...matches];

        if (dateFrom) {
            const fromDate = new Date(dateFrom.split('.').reverse().join('-'));
            filtered = filtered.filter(m => new Date(m.utcDate) >= fromDate);
        }
        if (dateTo) {
            const toDate = new Date(dateTo.split('.').reverse().join('-'));
            filtered = filtered.filter(m => new Date(m.utcDate) <= toDate);
        }

        setFilteredMatches(filtered);
    };

    const resetFilter = () => {
        setDateFrom('');
        setDateTo('');
        setFilteredMatches(matches);
    };

    if (loading) return <Loader />;
    if (error) return <ErrorMessage message={error} />;

    return (
        <div style={{ maxWidth: '1200px', margin: '0 auto', padding: '1rem' }}>
            <div style={{ marginBottom: '1rem' }}>
                <Link to="/teams" style={{ color: '#1a1a2e', textDecoration: 'none' }}>
                    ← Назад к командам
                </Link>
            </div>

            <h1 style={{ marginBottom: '0.5rem' }}>{teamName || 'Календарь команды'}</h1>

            <div style={{
                display: 'flex',
                gap: '1rem',
                flexWrap: 'wrap',
                marginBottom: '2rem',
                padding: '1rem',
                backgroundColor: '#f5f5f5',
                borderRadius: '8px',
                alignItems: 'flex-end',
            }}>
                <div>
                    <div style={{ marginBottom: '0.25rem', fontSize: '0.9rem' }}>Дата с:</div>
                    <input
                        type="text"
                        placeholder="ДД.ММ.ГГГГ"
                        value={dateFrom}
                        onChange={(e) => setDateFrom(e.target.value)}
                        style={{ padding: '0.5rem', borderRadius: '4px', border: '1px solid #ddd' }}
                    />
                </div>
                <div>
                    <div style={{ marginBottom: '0.25rem', fontSize: '0.9rem' }}>Дата по:</div>
                    <input
                        type="text"
                        placeholder="ДД.ММ.ГГГГ"
                        value={dateTo}
                        onChange={(e) => setDateTo(e.target.value)}
                        style={{ padding: '0.5rem', borderRadius: '4px', border: '1px solid #ddd' }}
                    />
                </div>
                <button
                    onClick={handleFilter}
                    style={{
                        padding: '0.5rem 1rem',
                        backgroundColor: '#1a1a2e',
                        color: 'white',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                    }}
                >
                    Применить
                </button>
                <button
                    onClick={resetFilter}
                    style={{
                        padding: '0.5rem 1rem',
                        backgroundColor: '#666',
                        color: 'white',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                    }}
                >
                    Сбросить
                </button>
            </div>

            {filteredMatches.length === 0 ? (
                <p style={{ textAlign: 'center', color: '#666' }}>Нет матчей за выбранный период</p>
            ) : (
                filteredMatches.map(match => <MatchCard key={match.id} match={match} />)
            )}
        </div>
    );
}