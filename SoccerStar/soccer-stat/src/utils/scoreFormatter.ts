import type { Match } from '../types';

export function formatScore(match: Match): string {
    if (!match || !match.score) {
        return '-:-';
    }

    const home = match.score.fullTime?.home;
    const away = match.score.fullTime?.away;

    if (home !== null && home !== undefined && away !== null && away !== undefined) {
        return home + ':' + away;
    }

    return '-:-';
}