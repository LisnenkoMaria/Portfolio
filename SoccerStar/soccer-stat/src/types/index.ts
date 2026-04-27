export interface Competition {
    id: number;
    name: string;
    code: string;
    type: string;
    emblem?: string;
    area: {
        name: string;
    };
}

export interface Team {
    id: number;
    name: string;
    shortName?: string;
    crest?: string;
    crestUrl?: string;
}

export interface Match {
    id: number;
    status: string;
    utcDate: string;
    homeTeam: {
        name: string;
    };
    awayTeam: {
        name: string;
    };
    score: {
        fullTime: {
            home: number | null;
            away: number | null;
        };
        extraTime: {
            home: number | null;
            away: number | null;
        };
        penalties: {
            home: number | null;
            away: number | null;
        };
    };
    competition?: {
        name: string;
    };
}

export interface MatchesResponse {
    matches: Match[];
    count: number;
}

export interface CompetitionsResponse {
    competitions: Competition[];
    count: number;
}

export interface TeamsResponse {
    teams: Team[];
    count: number;
}