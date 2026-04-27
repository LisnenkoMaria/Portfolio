export function formatLocalDate(utcDate: string): string {
    const date = new Date(utcDate);
    return date.toLocaleDateString('ru-RU', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
    });
}

export function formatLocalTime(utcDate: string): string {
    const date = new Date(utcDate);
    return date.toLocaleTimeString('ru-RU', {
        hour: '2-digit',
        minute: '2-digit',
    });
}