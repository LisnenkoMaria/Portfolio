export function mapStatusToRussian(status: string): string {
    const statusMap: Record<string, string> = {
        SCHEDULED: 'Запланирован',
        LIVE: 'В прямом эфире',
        IN_PLAY: 'В игре',
        PAUSED: 'Пауза',
        FINISHED: 'Завершен',
        POSTPONED: 'Отложен',
        SUSPENDED: 'Приостановлен',
        CANCELED: 'Отменен'
    };
    return statusMap[status] || status;
}