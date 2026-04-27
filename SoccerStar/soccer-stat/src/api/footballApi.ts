import type { Competition, CompetitionsResponse, MatchesResponse, Team, TeamsResponse, Match } from '../types';

const BASE_URL = '/api';

async function fetchFromAPI<T>(endpoint: string): Promise<T> {
    try {
        const response = await fetch(`${BASE_URL}${endpoint}`);

        if (!response.ok) {
            throw new Error(`API error: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Fetch error:', error);
        throw error;
    }
}

export async function getCompetitions(): Promise<Competition[]> {
    const data = await fetchFromAPI<CompetitionsResponse>('/competitions');
    return data.competitions;
}

export async function getMatchesByCompetition(
    competitionId: number,
    dateFrom?: string,
    dateTo?: string
): Promise<Match[]> {
    let url = `/competitions/${competitionId}/matches`;

    if (dateFrom && dateTo) {
        url += `?dateFrom=${dateFrom}&dateTo=${dateTo}`;
    }

    const data = await fetchFromAPI<MatchesResponse>(url);
    return data.matches;
}

export async function getTeams(): Promise<Team[]> {
    const data = await fetchFromAPI<TeamsResponse>('/teams?limit=100');
    return data.teams;
}

export async function getMatchesByTeam(
    teamId: number,
    dateFrom?: string,
    dateTo?: string
): Promise<Match[]> {
    let url = `/teams/${teamId}/matches`;

    if (dateFrom && dateTo) {
        url += `?dateFrom=${dateFrom}&dateTo=${dateTo}`;
    }

    const data = await fetchFromAPI<MatchesResponse>(url);
    return data.matches;
}